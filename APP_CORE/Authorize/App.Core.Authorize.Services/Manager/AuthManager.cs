using Authorize.Services.Services.Models;
using App.Core.Common;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using SD.LLBLGen.Pro.ORMSupportClasses;
using Microsoft.AspNetCore.Http;
using static App.Core.Common.Constants;
using Authorize.Services.Models;
using Authorize.Services.Manager.Models;
using App.Qtht.Dal.DatabaseSpecific;
using App.Qtht.Dal.Linq;
using App.Qtht.Dal.EntityClasses;
using App.Qtht.Dal.HelperClasses;
using App.Qtht.Dal;

namespace Authorize.Services.Services.Manager
{
    public interface IAuthManager
    {
        ApiResponse<AuthenticateResponse> Authenticate(AuthenticateRequest model, string ipAddress, bool? loginSso = false);
        ApiResponse<AuthenticateResponse> AuthenHome(AuthenticateRequest model, string ipAddress);

        ApiResponse<AuthenticateResponse> AuthenHomeFeedback(AuthenticateRequest model, string ipAddress);
        ApiResponse<AuthenticateResponse> AuthenticateMobile(AuthenticateRequest model, string ipAddress);
        ApiResponse<RegisterResponse> Register(RegisterRequest model, string ipAddress);

        AuthenticateResponse RefreshToken(string token, string ipAddress, string tag);
        BaseAuthenticateResponse RefreshTokenBase(string token, string ipAddress, string tag);
        bool RevokeToken(string token, string ipAddress);
        IEnumerable<UserModel> GetAll();
        UserModel GetById(int id);
        Task<string> GetTokenSSO(SsoModel model);
        ApiResponse<BaseAuthenticateResponse> BaseAuthenticate(AuthenticateRequest model, string ipAddress);
        ApiResponse<BaseAuthenticateResponse> BaseAuthenticate_Fuse(AuthenticateRequest model, string ipAddress);
    }

    public class AuthManager : IAuthManager
    {
        private readonly IOptionsSnapshot<AppSettingModel> _appSettings;
        private readonly ICurrentContext _currentContext;


        public AuthManager(IOptionsSnapshot<AppSettingModel> appSettings, ICurrentContext currentContext)
        {
            _appSettings = appSettings;
            _currentContext = currentContext;
        }

        public async Task<string> GetTokenSSO(SsoModel model)
        {
            return await Task.Run(() =>
            {
                try
                {
                    var data = Authenticate(new AuthenticateRequest()
                    {
                        DinhDanhApp = model.AppName,
                        Username = model.UserName
                    }, null, true);
                    return data.Data.JwtToken;
                }
                catch (Exception)
                {
                    return null;
                }
            });
        }

        public ApiResponse<AuthenticateResponse> Authenticate(AuthenticateRequest model, string ipAddress, bool? loginSso = false)
        {
            using (DataAccessAdapter adapter = new DataAccessAdapterFactory().CreateAdapter())
            {
                UnitOfWork2 unitOfWork2 = new UnitOfWork2();
                var metaData = new LinqMetaData(adapter);
                string password = MD5Hash(_appSettings.Value.Authorize.ClientSecret + model.Password);
                SysuserEntity user = metaData.Sysuser.FirstOrDefault(c => c.Username == model.Username && c.Password == password);
                if (user == null)
                {
                    if (loginSso == true)
                    {
                        user = metaData.Sysuser.FirstOrDefault(c => c.Username == model.Username);
                        if (user == null)
                        {
                            return ApiResponse<AuthenticateResponse>.Generate(GeneralCode.NotFound, "Login SSO fail.");
                        }
                    }
                    else
                    {

                        return ApiResponse<AuthenticateResponse>.Generate(GeneralCode.NotFound, "Tài khoản hoặc mật khẩu không đúng");
                    }
                }
                // user.Log<SysuserEntity>(userId: user.Userid, userName: user.Username, actionName: UserAction.LogIn, comment: "Đăng nhập hệ thống", IpClient: ipAddress);
                int? appId = model.AppId;
                if (appId == null)
                {
                    appId = (int)metaData.Sysapp.First(c => c.Dinhdanhapp == model.DinhDanhApp).Appid;
                }
                else if (user.Trangthai != 1)
                {
                    return ApiResponse<AuthenticateResponse>.Generate(GeneralCode.Error, "Tài khoản chưa được kích hoạt.");
                }
                //var groupOfUser = metaData.Sysusergroup.Where(c => c.Userid == user.Userid && c.Sysgroup.Appid == model.AppId).FirstOrDefault();
                //get menu, function from menugroup
                string strAppId = appId.ToString();
                //get menu, function from menugroup
                List<MenuOfUserModel> lstMenu = (from c in metaData.Sysgroupmenu
                                                 join p in metaData.Sysmenu on c.Menuid equals p.Menuid
                                                 join t in metaData.Sysusergroup on c.Groupid equals t.Groupid
                                                 where p.State != null && t.Userid == user.Userid && p.Appid == strAppId
                                                 && c.Function != null
                                                 select new MenuOfUserModel
                                                 {
                                                     State = p.State,
                                                     Function = c.Function
                                                 }).ToList();
                //get menu, function from menuuser
                List<MenuOfUserModel> lstMenu_user = (from c in metaData.Sysusermenu
                                                      join p in metaData.Sysmenu on c.Menuid equals p.Menuid
                                                      where c.Userid == user.Userid && p.State != null && p.Appid == strAppId
                                                      && c.Function != null
                                                      select new MenuOfUserModel
                                                      {
                                                          State = p.State,
                                                          Function = c.Function,
                                                      }).ToList();

                foreach (MenuOfUserModel item in lstMenu_user)
                {
                    lstMenu.Add(item);
                }

                if (lstMenu.Count == 0)
                {
                    return ApiResponse<AuthenticateResponse>.Generate(GeneralCode.Error, "Tài khoản không có quyền truy cập App.");
                }

                var userModelConvert = new UserModel()
                {
                    Id = (int)user.Userid,
                    Username = user.Username,
                    FullName = user.Fullname,
                    Password = password,
                    AppId = appId,
                    Tag = model.Tag,
                    UnitCode = user.Unitcode,
                    UserType = user.Loaiuser,
                    UnitName = metaData.Sysunit.First(c => c.Unitcode == user.Unitcode).Tendonvi,
                    lstMenu = lstMenu,
                    RefreshTokens = new List<RefreshToken>()
                };
                // authentication successful so generate jwt and refresh tokens
                var refreshToken = generateRefreshToken(ipAddress);
                RefreshToken jwtToken = generateJwtToken(userModelConvert, ipAddress, refreshToken.Token);
                unitOfWork2.AddDeleteEntitiesDirectlyCall(nameof(SysRefreshtokenEntity), new RelationPredicateBucket(SysRefreshtokenFields.Expires < DateTime.Now));
                unitOfWork2.AddForSave(jwtToken, true);

                unitOfWork2.CommitOrder = new List<UnitOfWorkBlockType>() {
                    UnitOfWorkBlockType.Deletes,
                    UnitOfWorkBlockType.DeletesPerformedDirectly,
                    UnitOfWorkBlockType.Inserts };

                unitOfWork2.Commit(adapter, true);

                return new AuthenticateResponse(userModelConvert, jwtToken.GenerateToken, refreshToken.Token);
            }
        }

        public ApiResponse<AuthenticateResponse> AuthenticateMobile(AuthenticateRequest model, string ipAddress)
        {
            using (DataAccessAdapter adapter = new DataAccessAdapterFactory().CreateAdapter())
            {
                UnitOfWork2 unitOfWork2 = new UnitOfWork2();
                var metaData = new LinqMetaData(adapter);
                string password = MD5Hash(_appSettings.Value.Authorize.ClientSecret + model.Password);
                SysuserEntity user = metaData.Sysuser.FirstOrDefault(c => c.Username == model.Username && c.Password == password);
                //var groupOfUser = metaData.Sysusergroup.Where(c => c.Userid == user.Userid && c.Sysgroup.Appid == model.AppId).FirstOrDefault();
                //if (groupOfUser == null)
                //{
                //    return ApiResponse<AuthenticateResponse>.Generate(GeneralCode.Error, "Tài khoản không có quyền truy cập App.");
                //}
                if (user.Trangthai != 1)
                {
                    return ApiResponse<AuthenticateResponse>.Generate(GeneralCode.Error, "Tài khoản chưa được kích hoạt.");
                }

                var userModelConvert = new UserModel()
                {
                    Id = (int)user.Userid,
                    Username = user.Username,
                    FullName = user.Fullname,
                    Password = password,
                    AppId = model.AppId,
                    Tag = model.Tag,
                    UnitCode = user.Unitcode,
                    UserType = user.Loaiuser,
                    RefreshTokens = new List<RefreshToken>()
                };

                if (!String.IsNullOrEmpty(userModelConvert.UnitCode))
                {
                    userModelConvert.UnitName = metaData.Sysunit.First(c => c.Unitcode == user.Unitcode).Tendonvi;
                }

                // authentication successful so generate jwt and refresh tokens
                var refreshToken = generateRefreshToken(ipAddress);
                RefreshToken jwtToken = generateJwtTokenHome(userModelConvert, ipAddress, refreshToken.Token);
                unitOfWork2.AddDeleteEntitiesDirectlyCall(nameof(SysRefreshtokenEntity), new RelationPredicateBucket(SysRefreshtokenFields.Expires < DateTime.Now));
                unitOfWork2.AddForSave(jwtToken, true);

                unitOfWork2.CommitOrder = new List<UnitOfWorkBlockType>() {
                    UnitOfWorkBlockType.Deletes,
                    UnitOfWorkBlockType.DeletesPerformedDirectly,
                    UnitOfWorkBlockType.Inserts
                };

                unitOfWork2.Commit(adapter, true);

                return new AuthenticateResponse(userModelConvert, jwtToken.GenerateToken, refreshToken.Token);
            }
        }
        public ApiResponse<AuthenticateResponse> AuthenHome(AuthenticateRequest model, string ipAddress)
        {
            using (DataAccessAdapter adapter = new DataAccessAdapterFactory().CreateAdapter())
            {
                UnitOfWork2 unitOfWork2 = new UnitOfWork2();
                var metaData = new LinqMetaData(adapter);
                string password = MD5Hash(_appSettings.Value.Authorize.ClientSecret + model.Password);
                SysuserEntity user = metaData.Sysuser.FirstOrDefault(c => c.Username == model.Username && c.Password == password);
                // user.Log<SysuserEntity>(userId: user.Userid, userName: user.Username, actionName: UserAction.LogIn, comment: "Đăng nhập hệ thống", IpClient: ipAddress);
                //var groupOfUser = metaData.Sysusergroup.Where(c => c.Userid == user.Userid && c.Sysgroup.Appid == model.AppId).FirstOrDefault();
                //if (groupOfUser == null)
                //{
                //    return ApiResponse<AuthenticateResponse>.Generate(GeneralCode.Error, "Tài khoản không có quyền truy cập App.");
                //}
                //else 
                if (user.Trangthai != 1)
                {
                    return ApiResponse<AuthenticateResponse>.Generate(GeneralCode.Error, "Tài khoản chưa được kích hoạt.");
                }

                var userModelConvert = new UserModel()
                {
                    Id = (int)user.Userid,
                    Username = user.Username,
                    FullName = user.Fullname,
                    Password = password,
                    AppId = model.AppId,
                    UserType = user.Loaiuser,
                    //Tag = model.Tag,
                    //UnitCode = user.Unitcode,
                    //UnitName = metaData.Sysunit.First(c => c.Unitcode == user.Unitcode).Tendonvi,
                    RefreshTokens = new List<RefreshToken>()
                };
                // authentication successful so generate jwt and refresh tokens
                var refreshToken = generateRefreshToken(ipAddress);
                RefreshToken jwtToken = generateJwtTokenHome(userModelConvert, ipAddress, refreshToken.Token);
                unitOfWork2.AddDeleteEntitiesDirectlyCall(nameof(SysRefreshtokenEntity), new RelationPredicateBucket(SysRefreshtokenFields.Expires < DateTime.Now));
                unitOfWork2.AddForSave(jwtToken, true);

                unitOfWork2.CommitOrder = new List<UnitOfWorkBlockType>() {
                    UnitOfWorkBlockType.Deletes,
                    UnitOfWorkBlockType.DeletesPerformedDirectly,
                    UnitOfWorkBlockType.Inserts
                };

                unitOfWork2.Commit(adapter, true);

                return new AuthenticateResponse(userModelConvert, jwtToken.GenerateToken, refreshToken.Token, "LOCAL");
            }
        }

        private string MD5Hash(string text)
        {
            MD5 md5 = new MD5CryptoServiceProvider();

            //compute hash from the bytes of text  
            md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(text));

            //get hash result after compute it  
            byte[] result = md5.Hash;

            StringBuilder strBuilder = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                //change it into 2 hexadecimal digits  
                //for each byte  
                strBuilder.Append(result[i].ToString("x2"));
            }

            return strBuilder.ToString();
        }

        public AuthenticateResponse RefreshToken(string token, string ipAddress, string tag)
        {
            try
            {
                using (DataAccessAdapter adapter = new DataAccessAdapterFactory().CreateAdapter())
                {
                    var metaData = new LinqMetaData(adapter);
                    EntityCollection<SysRefreshtokenEntity> collection = new EntityCollection<SysRefreshtokenEntity>();
                    PrefetchPath2 path = new PrefetchPath2(EntityType.SysRefreshtokenEntity);
                    path.Add(SysRefreshtokenEntity.PrefetchPathSysuser);
                    adapter.FetchEntityCollection(collection, new RelationPredicateBucket(SysRefreshtokenFields.Token == token), path);

                    // return null if no user found with token
                    if (collection.Count == 0) return null;

                    var refreshToken = collection[0];
                    var user = refreshToken.Sysuser;
                    // return null if token is no longer active
                    if (refreshToken.Isactive == 0) return null;

                    // replace old refresh token with a new one and save
                    var newRefreshToken = generateRefreshToken(ipAddress);
                    refreshToken.Revoked = DateTime.UtcNow;
                    refreshToken.Revokedbyip = ipAddress;
                    refreshToken.Replacedbytoken = newRefreshToken.Token;
                    refreshToken.Tag = tag;
                    refreshToken.IsDirty = true;
                    adapter.SaveEntity(refreshToken, true);

                    var groupOfUser = metaData.Sysusergroup.Where(c => c.Userid == (int)user.Userid && c.Sysgroup.Appid == (int)refreshToken.Appid).FirstOrDefault();
                    List<MenuOfUserModel> lstMenu = (from c in metaData.Sysgroupmenu
                                                     join p in metaData.Sysmenu on c.Menuid equals p.Menuid
                                                     where c.Groupid == groupOfUser.Groupid && p.State != null
                                                     select new MenuOfUserModel
                                                     {
                                                         State = p.State,
                                                         Function = c.Function
                                                     }).ToList();

                    var userModelConvert = new UserModel()
                    {
                        Id = (int)user.Userid,
                        Username = user.Username,
                        FullName = user.Fullname,
                        Password = user.Password,
                        UnitCode = user.Unitcode,
                        AppId = refreshToken.Appid,
                        UserType = user.Loaiuser,
                        UnitName = metaData.Sysunit.First(c => c.Unitcode == user.Unitcode).Tendonvi,
                        Tag = string.IsNullOrWhiteSpace(tag) ? refreshToken.Tag : tag,
                        lstMenu = lstMenu,
                        RefreshTokens = new List<RefreshToken>()
                    };
                    // generate new jwt
                    RefreshToken tokenGenerate = generateJwtToken(userModelConvert, ipAddress, newRefreshToken.Token);
                    adapter.SaveEntity(tokenGenerate.GetIntanceEntity(), true);

                    return new AuthenticateResponse(userModelConvert, tokenGenerate.GenerateToken, newRefreshToken.Token);
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public bool RevokeToken(string token, string ipAddress)
        {
            using (DataAccessAdapter adapter = new DataAccessAdapterFactory().CreateAdapter())
            {
                EntityCollection<SysRefreshtokenEntity> collection = new EntityCollection<SysRefreshtokenEntity>();
                PrefetchPath2 path = new PrefetchPath2(EntityType.SysRefreshtokenEntity);
                path.Add(SysRefreshtokenEntity.PrefetchPathSysuser);
                adapter.FetchEntityCollection(collection, new RelationPredicateBucket(SysRefreshtokenFields.Token == token), path);

                // return false if no user found with token
                if (collection.Count == 0) return false;
                // return false if token is not active

                var refreshToken = collection[0];
                if (refreshToken.Isactive == 0) return false;

                // revoke token and save
                refreshToken.Revoked = DateTime.UtcNow;
                refreshToken.Revokedbyip = ipAddress;
                adapter.SaveEntity(refreshToken);
                return true;
            }
        }

        public IEnumerable<UserModel> GetAll()
        {
            //return _context.Users;
            return null;
        }

        public UserModel GetById(int id)
        {
            //return _context.Users.Find(id);
            return null;
        }

        // helper methods

        private RefreshToken generateJwtToken(UserModel user, string ipAddress, string refeshToken)
        {
            return BaseGenerateJwtToken(user, ipAddress, refeshToken, true);
        }

        private RefreshToken BaseGenerateJwtToken(UserModel user, string ipAddress, string refeshToken, bool isAddFull = true)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Value.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(Constants.ClaimTypes.UserName, user.Username),
                    new Claim(Constants.ClaimTypes.FullName, user.FullName),
                    new Claim(Constants.ClaimTypes.UserId, user.Id.ToString()),
                    new Claim(Constants.ClaimTypes.AppId, user.AppId.ToString()),
                    new Claim(Constants.ClaimTypes.UnitCode, user.UnitCode ?? ""),
                    new Claim(Constants.ClaimTypes.UnitName, user.UnitName ?? "")
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            if (isAddFull)
            {
                tokenDescriptor.Subject.AddClaim(new Claim(Constants.ClaimTypes.Tag, user.Tag ?? ""));
                tokenDescriptor.Subject.AddClaim(new Claim(Constants.ClaimTypes.UserType, user.UserType == null ? "" : user.ToString()));
                foreach (var item in user.lstMenu)
                {
                    tokenDescriptor.Subject.AddClaim(new Claim(System.Security.Claims.ClaimTypes.Role, String.Join(",", item.State, item.Function)));
                }
            }

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return new RefreshToken()
            {
                Userid = (int)user.Id,
                Expires = tokenDescriptor.Expires,
                Isexpired = 1,
                Createdbyip = ipAddress,
                Token = refeshToken,
                Created = DateTime.Now,
                Isactive = 1,
                Tag = user.Tag,
                Appid = (long?)user.AppId,
                IsNew = true,
                GenerateToken = tokenHandler.WriteToken(token)
            };
        }

        private RefreshToken generateJwtTokenHome(UserModel user, string ipAddress, string refeshToken)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Value.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(Constants.ClaimTypes.UserId, user.Id.ToString()),
                    new Claim(Constants.ClaimTypes.AppId, user.AppId.ToString()),
                    new Claim(Constants.ClaimTypes.UserName, user.Username),
                    new Claim(Constants.ClaimTypes.FullName, user.FullName),
                    new Claim(Constants.ClaimTypes.UnitCode, user.UnitCode ?? ""),
                    new Claim(Constants.ClaimTypes.UnitName, user.UnitName ?? ""),
                    new Claim(Constants.ClaimTypes.Tag, user.Tag ?? ""),
                    new Claim(Constants.ClaimTypes.UserType, user.UserType == null ? "" : user.ToString()),
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return new RefreshToken()
            {
                Userid = (int)user.Id,
                Expires = tokenDescriptor.Expires,
                Isexpired = 1,
                Createdbyip = ipAddress,
                Token = refeshToken,
                Created = DateTime.Now,
                Isactive = 1,
                Tag = user.Tag,
                Appid = (long?)user.AppId,
                IsNew = true,
                GenerateToken = tokenHandler.WriteToken(token)
            };
        }

        private RefreshToken generateRefreshToken(string ipAddress)
        {
            using (var rngCryptoServiceProvider = new RNGCryptoServiceProvider())
            {
                var randomBytes = new byte[64];
                rngCryptoServiceProvider.GetBytes(randomBytes);
                return new RefreshToken
                {
                    Token = Convert.ToBase64String(randomBytes),
                    Expires = DateTime.UtcNow.AddHours(1),
                    Created = DateTime.UtcNow,
                    Createdbyip = ipAddress
                };
            }
        }

        public ApiResponse<RegisterResponse> Register(RegisterRequest model, string ipAddress)
        {
            using (DataAccessAdapter adapter = new DataAccessAdapterFactory().CreateAdapter())
            {
                UnitOfWork2 unitOfWork2 = new UnitOfWork2();
                var metaData = new LinqMetaData(adapter);
                string password = MD5Hash(_appSettings.Value.Authorize.ClientSecret + model.Password);
                SysuserEntity user = metaData.Sysuser.FirstOrDefault(c => c.Username == model.UserName);

                if (user != null)
                    return ApiResponse<RegisterResponse>.Generate(GeneralCode.Error, "Tài khoản đã tồn tại trên hệ thống.");

                user = new SysuserEntity { Email = model.Email, Fullname = model.FullName, Username = model.UserName, Password = password, Phone = model.Phone, Trangthai = 1, Appid = model.AppId, Loaiuser = -1 };

                adapter.SaveEntity(user);

                unitOfWork2.Commit(adapter, true);

                return new RegisterResponse { UserName = model.UserName, FullName = model.FullName, Phone = model.Phone, Email = model.Email, Id = user.Userid };
            }
        }

        public ApiResponse<AuthenticateResponse> AuthenHomeFeedback(AuthenticateRequest model, string ipAddress)
        {
            using (DataAccessAdapter adapter = new DataAccessAdapterFactory().CreateAdapter())
            {
                UnitOfWork2 unitOfWork2 = new UnitOfWork2();
                var metaData = new LinqMetaData(adapter);
                string password = MD5Hash(_appSettings.Value.Authorize.ClientSecret + model.Password);
                SysuserEntity user = metaData.Sysuser.FirstOrDefault(c => c.Username == model.Username && c.Password == password);

                if (user == null) return ApiResponse<AuthenticateResponse>.Generate(GeneralCode.Error, "Tài khoản hoặc mật khẩu không đúng!");
                if (user.Trangthai != 1) return ApiResponse<AuthenticateResponse>.Generate(GeneralCode.Error, "Tài khoản chưa được kích hoạt.");

                var userModelConvert = new UserModel()
                {
                    Id = (int)user.Userid,
                    Username = user.Username,
                    FullName = user.Fullname,
                    Password = password,
                    AppId = model.AppId,
                    UserType = user.Loaiuser,
                    RefreshTokens = new List<RefreshToken>()
                };
                // authentication successful so generate jwt and refresh tokens
                var refreshToken = generateRefreshToken(ipAddress);
                RefreshToken jwtToken = generateJwtTokenHome(userModelConvert, ipAddress, refreshToken.Token);
                unitOfWork2.AddDeleteEntitiesDirectlyCall(nameof(SysRefreshtokenEntity), new RelationPredicateBucket(SysRefreshtokenFields.Expires < DateTime.Now));
                unitOfWork2.AddForSave(jwtToken, true);

                unitOfWork2.CommitOrder = new List<UnitOfWorkBlockType>() {
                    UnitOfWorkBlockType.Deletes,
                    UnitOfWorkBlockType.DeletesPerformedDirectly,
                    UnitOfWorkBlockType.Inserts
                };

                unitOfWork2.Commit(adapter, true);

                return new AuthenticateResponse(userModelConvert, jwtToken.GenerateToken, refreshToken.Token);
            }
        }

        public ApiResponse<BaseAuthenticateResponse> BaseAuthenticate(AuthenticateRequest model, string ipAddress)
        {
            using (DataAccessAdapter adapter = new DataAccessAdapterFactory().CreateAdapter())
            {
                UnitOfWork2 unitOfWork2 = new UnitOfWork2();
                var metaData = new LinqMetaData(adapter);
                string password = MD5Hash(_appSettings.Value.Authorize.ClientSecret + model.Password);
                SysuserEntity user = metaData.Sysuser.FirstOrDefault(c => c.Username == model.Username && c.Password == password);
                if (user == null)
                {
                    return ApiResponse<BaseAuthenticateResponse>.Generate(GeneralCode.NotFound, "Tài khoản hoặc mật khẩu không đúng");
                }
                int? appId = model.AppId;
                if (appId == null || appId == 0)
                {
                    appId = (int)metaData.Sysapp.First(c => c.Dinhdanhapp == model.DinhDanhApp).Appid;
                }
                else if (user.Trangthai != 1)
                {
                    return ApiResponse<BaseAuthenticateResponse>.Generate(GeneralCode.Error, "Tài khoản chưa được kích hoạt.");
                }
                string strAppId = appId.ToString();
                List<MenuOfUserModel> lstMenu = (from c in metaData.Sysgroupmenu
                                                 join p in metaData.Sysmenu on c.Menuid equals p.Menuid
                                                 join t in metaData.Sysusergroup on c.Groupid equals t.Groupid
                                                 where p.State != null && t.Userid == user.Userid && p.Appid == strAppId
                                                 && c.Function != null
                                                 select new MenuOfUserModel
                                                 {
                                                     State = p.State,
                                                     Function = c.Function
                                                 }).ToList();
                //get menu, function from menuuser
                List<MenuOfUserModel> lstMenu_user = (from c in metaData.Sysusermenu
                                                      join p in metaData.Sysmenu on c.Menuid equals p.Menuid
                                                      where c.Userid == user.Userid && p.State != null && p.Appid == strAppId
                                                      && c.Function != null
                                                      select new MenuOfUserModel
                                                      {
                                                          State = p.State,
                                                          Function = c.Function,
                                                      }).ToList();

                foreach (MenuOfUserModel item in lstMenu_user)
                {
                    lstMenu.Add(item);
                }

                if (lstMenu.Count == 0)
                {
                    return ApiResponse<BaseAuthenticateResponse>.Generate(GeneralCode.Error, "Tài khoản không có quyền truy cập App.");
                }

                var userModelConvert = new UserModel()
                {
                    Id = (int)user.Userid,
                    Username = user.Username,
                    FullName = user.Fullname,
                    Password = password,
                    AppId = appId,
                    UnitCode = user.Unitcode,
                    lstMenu = lstMenu,
                    RefreshTokens = new List<RefreshToken>()
                };
                // authentication successful so generate jwt and refresh tokens
                var refreshToken = generateRefreshToken(ipAddress);
                RefreshToken jwtToken = BaseGenerateJwtToken(userModelConvert, ipAddress, refreshToken.Token, true);
                unitOfWork2.AddDeleteEntitiesDirectlyCall(nameof(SysRefreshtokenEntity), new RelationPredicateBucket(SysRefreshtokenFields.Expires < DateTime.Now));
                unitOfWork2.AddForSave(jwtToken, true);

                unitOfWork2.CommitOrder = new List<UnitOfWorkBlockType>() {
                    UnitOfWorkBlockType.Deletes,
                    UnitOfWorkBlockType.DeletesPerformedDirectly,
                    UnitOfWorkBlockType.Inserts };

                unitOfWork2.Commit(adapter, true);
                //user.LogLogin<SysuserEntity>(_currentContext);
                return new BaseAuthenticateResponse(userModelConvert, jwtToken.GenerateToken, refreshToken.Token);
            }
        }
        public BaseAuthenticateResponse RefreshTokenBase(string token, string ipAddress, string tag)
        {
            try
            {
                using (DataAccessAdapter adapter = new DataAccessAdapterFactory().CreateAdapter())
                {
                    var metaData = new LinqMetaData(adapter);
                    EntityCollection<SysRefreshtokenEntity> collection = new EntityCollection<SysRefreshtokenEntity>();
                    PrefetchPath2 path = new PrefetchPath2(EntityType.SysRefreshtokenEntity);
                    path.Add(SysRefreshtokenEntity.PrefetchPathSysuser);
                    adapter.FetchEntityCollection(collection, new RelationPredicateBucket(SysRefreshtokenFields.Token == token), path);

                    // return null if no user found with token
                    if (collection.Count == 0) return null;

                    var refreshToken = collection[0];
                    var user = refreshToken.Sysuser;
                    // return null if token is no longer active
                    if (refreshToken.Isactive == 0) return null;

                    // replace old refresh token with a new one and save
                    var newRefreshToken = generateRefreshToken(ipAddress);
                    refreshToken.Revoked = DateTime.UtcNow;
                    refreshToken.Revokedbyip = ipAddress;
                    refreshToken.Replacedbytoken = newRefreshToken.Token;
                    refreshToken.Tag = tag;
                    refreshToken.IsDirty = true;
                    adapter.SaveEntity(refreshToken, true);

                    var groupOfUser = metaData.Sysusergroup.Where(c => c.Userid == (int)user.Userid && c.Sysgroup.Appid == (int)refreshToken.Appid).FirstOrDefault();
                    List<MenuOfUserModel> lstMenu = (from c in metaData.Sysgroupmenu
                                                     join p in metaData.Sysmenu on c.Menuid equals p.Menuid
                                                     where c.Groupid == groupOfUser.Groupid && p.State != null
                                                     select new MenuOfUserModel
                                                     {
                                                         State = p.State,
                                                         Function = c.Function
                                                     }).ToList();

                    var unit = metaData.Sysunit.FirstOrDefault(c => c.Unitcode == user.Unitcode);
                    var userModelConvert = new UserModel()
                    {
                        Id = (int)user.Userid,
                        Username = user.Username,
                        FullName = user.Fullname,
                        Password = user.Password,
                        AppId = refreshToken.Appid,
                        UnitCode = user.Unitcode,
                        lstMenu = lstMenu,
                        Tag = unit != null ? null : $"CapDonVi:{unit.Capdonvi}",
                        RefreshTokens = new List<RefreshToken>()
                    };
                    // generate new jwt
                    RefreshToken tokenGenerate = generateJwtToken(userModelConvert, ipAddress, newRefreshToken.Token);
                    adapter.SaveEntity(tokenGenerate.GetIntanceEntity(), true);

                    return new BaseAuthenticateResponse(userModelConvert, tokenGenerate.GenerateToken, newRefreshToken.Token);
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public ApiResponse<BaseAuthenticateResponse> BaseAuthenticate_Fuse(AuthenticateRequest model, string ipAddress)
        {
            using (DataAccessAdapter adapter = new DataAccessAdapterFactory().CreateAdapter())
            {
                UnitOfWork2 unitOfWork2 = new UnitOfWork2();
                var metaData = new LinqMetaData(adapter);
                string password = MD5Hash(_appSettings.Value.Authorize.ClientSecret + model.Password);
                SysuserEntity user = metaData.Sysuser.FirstOrDefault(c => c.Email == model.Username && c.Password == password);
                if (user == null)
                {
                    return ApiResponse<BaseAuthenticateResponse>.Generate(GeneralCode.NotFound, "Tài khoản hoặc mật khẩu không đúng");
                }
                int? appId = model.AppId;
                if (appId == null || appId == 0)
                {
                    appId = (int)metaData.Sysapp.First(c => c.Dinhdanhapp == model.DinhDanhApp).Appid;
                }
                else if (user.Trangthai != 1)
                {
                    return ApiResponse<BaseAuthenticateResponse>.Generate(GeneralCode.Error, "Tài khoản chưa được kích hoạt.");
                }

                var userModelConvert = new UserModel()
                {
                    Id = (int)user.Userid,
                    email = user.Email,
                    Username = user.Username,
                    name = user.Fullname,
                    FullName = user.Fullname,
                    Password = password,
                    avatar = user.Imgavatar,
                    status = "online",
                    AppId = appId,
                    UnitCode = user.Unitcode,
                    RefreshTokens = new List<RefreshToken>()
                };
                // authentication successful so generate jwt and refresh tokens
                var refreshToken = generateRefreshToken(ipAddress);
                RefreshToken jwtToken = BaseGenerateJwtToken(userModelConvert, ipAddress, refreshToken.Token, false);
                unitOfWork2.AddDeleteEntitiesDirectlyCall(nameof(SysRefreshtokenEntity), new RelationPredicateBucket(SysRefreshtokenFields.Expires < DateTime.Now));
                unitOfWork2.AddForSave(jwtToken, true);

                unitOfWork2.CommitOrder = new List<UnitOfWorkBlockType>() {
                    UnitOfWorkBlockType.Deletes,
                    UnitOfWorkBlockType.DeletesPerformedDirectly,
                    UnitOfWorkBlockType.Inserts };

                unitOfWork2.Commit(adapter, true);

                return new BaseAuthenticateResponse(userModelConvert, jwtToken.GenerateToken, refreshToken.Token);
            }
        }

        
    }
}
