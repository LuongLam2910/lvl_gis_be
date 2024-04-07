using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using App.Core.Common;
using App.QTHTGis.Dal;
using App.Qtht.Services.Authorize;
using App.QTHTGis.Dal.EntityClasses;
using App.QTHTGis.Dal.HelperClasses;
using App.QTHTGis.Dal.Linq;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SD.LLBLGen.Pro.ORMSupportClasses;
using static App.Core.Common.Constants;
using ClaimTypes = System.Security.Claims.ClaimTypes;
using System.Linq;

namespace App.QTHTGis.Services.Manager;

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
                var data = Authenticate(new AuthenticateRequest
                {
                    DinhDanhApp = model.AppName,
                    Username = model.UserName
                }, null,true);
                return data.Data.JwtToken;
            }
            catch (Exception)
            {
                return null;
            }
        });
    }

    public ApiResponse<AuthenticateResponse> Authenticate(AuthenticateRequest model, string ipAddress,
        bool? loginSso = false)
    {
        try
        {
            using (var adapter = new DataAccessAdapterFactory().CreateAdapter())
            {

                var unitOfWork2 = new UnitOfWork2();
                var metaData = new LinqMetaData(adapter);
                var password = MD5Hash(_appSettings.Value.Authorize.ClientSecret + model.Password);
                var user = metaData.Sysuser.FirstOrDefault(c => c.Username == model.Username && c.Password == password);
                if (user == null)
                {
                    if (loginSso == true)
                    {
                        user = metaData.Sysuser.FirstOrDefault(c => c.Username == model.Username);
                        if (user == null)
                            return ApiResponse<AuthenticateResponse>.Generate(GeneralCode.NotFound, "Login SSO fail.");
                    }
                    else
                    {
                        return ApiResponse<AuthenticateResponse>.Generate(GeneralCode.NotFound,
                            "Tài khoản hoặc mật khẩu không đúng");
                    }
                }

                user.Log(user.Userid, userName: user.Username, actionName: UserAction.LogIn,
                    comment: "Đăng nhập hệ thống", IpClient: ipAddress);
                var appId = model.AppId;
                if (appId == null)
                    appId = (int)metaData.Sysapp.First(c => c.Dinhdanhapp == model.DinhDanhApp).Appid;
                else if (user.Trangthai != 1)
                    return ApiResponse<AuthenticateResponse>.Generate(GeneralCode.Error,
                        "Tài khoản chưa được kích hoạt.");
                //var groupOfUser = metaData.Sysusergroup.Where(c => c.Userid == user.Userid && c.Sysgroup.Appid == model.AppId).FirstOrDefault();
                //get menu, function from menugroup
                var strAppId = appId.ToString();
                //get menu, function from menugroup
                var lstMenu = (from c in metaData.Sysgroupmenu
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
                var lstMenu_user = (from c in metaData.Sysusermenu
                                    join p in metaData.Sysmenu on c.Menuid equals p.Menuid
                                    where c.Userid == user.Userid && p.State != null && p.Appid == strAppId
                                          && c.Function != null
                                    select new MenuOfUserModel
                                    {
                                        State = p.State,
                                        Function = c.Function
                                    }).ToList();

                foreach (var item in lstMenu_user) lstMenu.Add(item);

                if (lstMenu.Count == 0)
                    return ApiResponse<AuthenticateResponse>.Generate(GeneralCode.Error,
                        "Tài khoản không có quyền truy cập App.");
                var userModelConvert = new UserModel();
                userModelConvert.Id = (int)user.Userid;
                userModelConvert.Username = user.Username;
                userModelConvert.maTinh = user.matinh;
                userModelConvert.maHuyen = user.mahuyen;
                userModelConvert.maXa = user.maxa;
                userModelConvert.FullName = user.Fullname;
                userModelConvert.email = user.Email;
                userModelConvert.diachi = user.Diachi;
                userModelConvert.phone = user.Phone;
                userModelConvert.Password = password;
                userModelConvert.AppId = appId;
                userModelConvert.Tag = model.Tag;
                userModelConvert.UnitCode = user.Unitcode;
                userModelConvert.UserType = user.Loaiuser;
                userModelConvert.UnitName = metaData.Sysunit.First(c => c.Unitcode == user.Unitcode).Tendonvi;
                userModelConvert.lstMenu = lstMenu;
                userModelConvert.RefreshTokens = new List<RefreshToken>();
                // authentication successful so generate jwt and refresh tokens
                var refreshToken = generateRefreshToken(ipAddress);
                var jwtToken = generateJwtToken(userModelConvert, ipAddress, refreshToken.Token);
                unitOfWork2.AddDeleteEntitiesDirectlyCall(nameof(SysRefreshtokenEntity),
                    new RelationPredicateBucket(SysRefreshtokenFields.Expires < DateTime.Now));
                unitOfWork2.AddForSave(jwtToken, true);

                unitOfWork2.CommitOrder = new List<UnitOfWorkBlockType>
                {
                    UnitOfWorkBlockType.Deletes,
                    UnitOfWorkBlockType.DeletesPerformedDirectly,
                    UnitOfWorkBlockType.Inserts
                };

                unitOfWork2.Commit(adapter, true);

                return new AuthenticateResponse(userModelConvert, jwtToken.GenerateToken, refreshToken.Token);
            }
        }
        catch (Exception ex)
        {
            return ApiResponse<AuthenticateResponse>.Generate(GeneralCode.Error, ex.Message);
        }
    }

    public ApiResponse<AuthenticateResponse> AuthenticateMobile(AuthenticateRequest model, string ipAddress)
    {
        using (var adapter = new DataAccessAdapterFactory().CreateAdapter())
        {
            var unitOfWork2 = new UnitOfWork2();
            var metaData = new LinqMetaData(adapter);
            var password = MD5Hash(_appSettings.Value.Authorize.ClientSecret + model.Password);
            var user = metaData.Sysuser.FirstOrDefault(c => c.Username == model.Username && c.Password == password);
            //var groupOfUser = metaData.Sysusergroup.Where(c => c.Userid == user.Userid && c.Sysgroup.Appid == model.AppId).FirstOrDefault();
            //if (groupOfUser == null)
            //{
            //    return ApiResponse<AuthenticateResponse>.Generate(GeneralCode.Error, "Tài khoản không có quyền truy cập App.");
            //}
            if (user.Trangthai != 1)
                return ApiResponse<AuthenticateResponse>.Generate(GeneralCode.Error, "Tài khoản chưa được kích hoạt.");

            var userModelConvert = new UserModel
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

            if (!string.IsNullOrEmpty(userModelConvert.UnitCode))
                userModelConvert.UnitName = metaData.Sysunit.First(c => c.Unitcode == user.Unitcode).Tendonvi;

            // authentication successful so generate jwt and refresh tokens
            var refreshToken = generateRefreshToken(ipAddress);
            var jwtToken = generateJwtTokenHome(userModelConvert, ipAddress, refreshToken.Token);
            unitOfWork2.AddDeleteEntitiesDirectlyCall(nameof(SysRefreshtokenEntity),
                new RelationPredicateBucket(SysRefreshtokenFields.Expires < DateTime.Now));
            unitOfWork2.AddForSave(jwtToken, true);

            unitOfWork2.CommitOrder = new List<UnitOfWorkBlockType>
            {
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
        using (var adapter = new DataAccessAdapterFactory().CreateAdapter())
        {
            var unitOfWork2 = new UnitOfWork2();
            var metaData = new LinqMetaData(adapter);
            var password = MD5Hash(_appSettings.Value.Authorize.ClientSecret + model.Password);
            var user = metaData.Sysuser.FirstOrDefault(c => c.Username == model.Username && c.Password == password);
            // user.Log<SysuserEntity>(userId: user.Userid, userName: user.Username, actionName: UserAction.LogIn, comment: "Đăng nhập hệ thống", IpClient: ipAddress);
            //var groupOfUser = metaData.Sysusergroup.Where(c => c.Userid == user.Userid && c.Sysgroup.Appid == model.AppId).FirstOrDefault();
            //if (groupOfUser == null)
            //{
            //    return ApiResponse<AuthenticateResponse>.Generate(GeneralCode.Error, "Tài khoản không có quyền truy cập App.");
            //}
            //else 
            if (user.Trangthai != 1)
                return ApiResponse<AuthenticateResponse>.Generate(GeneralCode.Error, "Tài khoản chưa được kích hoạt.");

            var userModelConvert = new UserModel
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
            var jwtToken = generateJwtTokenHome(userModelConvert, ipAddress, refreshToken.Token);
            unitOfWork2.AddDeleteEntitiesDirectlyCall(nameof(SysRefreshtokenEntity),
                new RelationPredicateBucket(SysRefreshtokenFields.Expires < DateTime.Now));
            unitOfWork2.AddForSave(jwtToken, true);

            unitOfWork2.CommitOrder = new List<UnitOfWorkBlockType>
            {
                UnitOfWorkBlockType.Deletes,
                UnitOfWorkBlockType.DeletesPerformedDirectly,
                UnitOfWorkBlockType.Inserts
            };

            unitOfWork2.Commit(adapter, true);

            return new AuthenticateResponse(userModelConvert, jwtToken.GenerateToken, refreshToken.Token, "LOCAL");
        }
    }

    public AuthenticateResponse RefreshToken(string token, string ipAddress, string tag)
    {
        try
        {
            using (var adapter = new DataAccessAdapterFactory().CreateAdapter())
            {
                var metaData = new LinqMetaData(adapter);
                var collection = new EntityCollection<SysRefreshtokenEntity>();
                var path = new PrefetchPath2(EntityType.SysRefreshtokenEntity);
                path.Add(SysRefreshtokenEntity.PrefetchPathSysuser);
                adapter.FetchEntityCollection(collection,
                    new RelationPredicateBucket(SysRefreshtokenFields.Token == token), path);

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

                var groupOfUser = metaData.Sysusergroup
                    .Where(c => c.Userid == (int)user.Userid && c.Sysgroup.Appid == (int)refreshToken.Appid)
                    .FirstOrDefault();
                var lstMenu = (from c in metaData.Sysgroupmenu
                    join p in metaData.Sysmenu on c.Menuid equals p.Menuid
                    where c.Groupid == groupOfUser.Groupid && p.State != null
                    select new MenuOfUserModel
                    {
                        State = p.State,
                        Function = c.Function
                    }).ToList();

                var userModelConvert = new UserModel
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
                var tokenGenerate = generateJwtToken(userModelConvert, ipAddress, newRefreshToken.Token);
                adapter.SaveEntity(tokenGenerate.GetIntanceEntity(), true);

                return new AuthenticateResponse(userModelConvert, tokenGenerate.GenerateToken, newRefreshToken.Token);
            }
        }
        catch (Exception)
        {
            return null;
        }
    }

    public bool RevokeToken(string token, string ipAddress)
    {
        using (var adapter = new DataAccessAdapterFactory().CreateAdapter())
        {
            var collection = new EntityCollection<SysRefreshtokenEntity>();
            var path = new PrefetchPath2(EntityType.SysRefreshtokenEntity);
            path.Add(SysRefreshtokenEntity.PrefetchPathSysuser);
            adapter.FetchEntityCollection(collection, new RelationPredicateBucket(SysRefreshtokenFields.Token == token),
                path);

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

    public ApiResponse<RegisterResponse> Register(RegisterRequest model, string ipAddress)
    {
        using (var adapter = new DataAccessAdapterFactory().CreateAdapter())
        {
            var unitOfWork2 = new UnitOfWork2();
            var metaData = new LinqMetaData(adapter);
            var password = MD5Hash(_appSettings.Value.Authorize.ClientSecret + model.Password);
            var user = metaData.Sysuser.FirstOrDefault(c => c.Username == model.UserName);

            if (user != null)
                return ApiResponse<RegisterResponse>.Generate(GeneralCode.Error, "Tài khoản đã tồn tại trên hệ thống.");

            user = new SysuserEntity
            {
                Email = model.Email, Fullname = model.FullName, Username = model.UserName, Password = password,
                Phone = model.Phone, Trangthai = 1, Appid = model.AppId, Loaiuser = -1
            };

            adapter.SaveEntity(user);

            unitOfWork2.Commit(adapter, true);

            return new RegisterResponse
            {
                UserName = model.UserName, FullName = model.FullName, Phone = model.Phone, Email = model.Email,
                Id = user.Userid
            };
        }
    }

    public ApiResponse<AuthenticateResponse> AuthenHomeFeedback(AuthenticateRequest model, string ipAddress)
    {
        using (var adapter = new DataAccessAdapterFactory().CreateAdapter())
        {
            var unitOfWork2 = new UnitOfWork2();
            var metaData = new LinqMetaData(adapter);
            var password = MD5Hash(_appSettings.Value.Authorize.ClientSecret + model.Password);
            var user = metaData.Sysuser.FirstOrDefault(c => c.Username == model.Username && c.Password == password);

            if (user == null)
                return ApiResponse<AuthenticateResponse>.Generate(GeneralCode.Error,
                    "Tài khoản hoặc mật khẩu không đúng!");
            if (user.Trangthai != 1)
                return ApiResponse<AuthenticateResponse>.Generate(GeneralCode.Error, "Tài khoản chưa được kích hoạt.");

            var userModelConvert = new UserModel
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
            var jwtToken = generateJwtTokenHome(userModelConvert, ipAddress, refreshToken.Token);
            unitOfWork2.AddDeleteEntitiesDirectlyCall(nameof(SysRefreshtokenEntity),
                new RelationPredicateBucket(SysRefreshtokenFields.Expires < DateTime.Now));
            unitOfWork2.AddForSave(jwtToken, true);

            unitOfWork2.CommitOrder = new List<UnitOfWorkBlockType>
            {
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
        try
        {
            using (var adapter = new DataAccessAdapterFactory().CreateAdapter())
            {
                var unitOfWork2 = new UnitOfWork2();
                var metaData = new LinqMetaData(adapter);
                var password = MD5Hash(_appSettings.Value.Authorize.ClientSecret + model.Password);
                var user = metaData.Sysuser.FirstOrDefault(c => (c.Username == model.Username || c.Username == Md5Function.Decrypt_phone(model.Username)) && c.Password == password);

                if (user == null)
                    return ApiResponse<BaseAuthenticateResponse>.Generate(GeneralCode.NotFound,
                        "Tài khoản hoặc mật khẩu không đúng");
                var appId = model.AppId;
                if (appId == null || appId == 0)
                    appId = (int)metaData.Sysapp.First(c => c.Dinhdanhapp == model.DinhDanhApp).Appid;
                else if (user.Trangthai != 1)
                    return ApiResponse<BaseAuthenticateResponse>.Generate(GeneralCode.Error,
                        "Tài khoản chưa được kích hoạt.");
                var strAppId = appId.ToString();
                var lstMenu = (from c in metaData.Sysgroupmenu
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
                var lstMenu_user = (from c in metaData.Sysusermenu
                    join p in metaData.Sysmenu on c.Menuid equals p.Menuid
                    where c.Userid == user.Userid && p.State != null && p.Appid == strAppId
                          && c.Function != null
                    select new MenuOfUserModel
                    {
                        State = p.State,
                        Function = c.Function
                    }).ToList();

                foreach (var item in lstMenu_user) lstMenu.Add(item);

                if (lstMenu.Count == 0)
                    return ApiResponse<BaseAuthenticateResponse>.Generate(GeneralCode.Error,
                        "Tài khoản không có quyền truy cập App.");

                var userModelConvert = new UserModel
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
                var jwtToken = BaseGenerateJwtToken(userModelConvert, ipAddress, refreshToken.Token);

                unitOfWork2.AddDeleteEntitiesDirectlyCall(nameof(SysRefreshtokenEntity),
                    new RelationPredicateBucket(SysRefreshtokenFields.Expires < DateTime.Now));
                unitOfWork2.AddForSave(jwtToken, true);

                unitOfWork2.CommitOrder = new List<UnitOfWorkBlockType>
                {
                    UnitOfWorkBlockType.Deletes,
                    UnitOfWorkBlockType.DeletesPerformedDirectly,
                    UnitOfWorkBlockType.Inserts
                };

                unitOfWork2.Commit(adapter, true);
                return new BaseAuthenticateResponse(userModelConvert, jwtToken.GenerateToken, refreshToken.Token);
            }
        }
        catch (Exception ex)
        {
            var st = new StackTrace(ex, true);
            // Get the top stack frame
            var frame = st.GetFrame(0);
            // Get the line number from the stack frame
            var line = frame.GetFileLineNumber();
            return ApiResponse<BaseAuthenticateResponse>.Generate(GeneralCode.Error, ex.Message + " - " + line);
        }
    }

    public BaseAuthenticateResponse RefreshTokenBase(string token, string ipAddress, string tag)
    {
        try
        {
            using (var adapter = new DataAccessAdapterFactory().CreateAdapter())
            {
                var metaData = new LinqMetaData(adapter);
                var collection = new EntityCollection<SysRefreshtokenEntity>();
                var path = new PrefetchPath2(EntityType.SysRefreshtokenEntity);
                path.Add(SysRefreshtokenEntity.PrefetchPathSysuser);
                adapter.FetchEntityCollection(collection,
                    new RelationPredicateBucket(SysRefreshtokenFields.Token == token), path);

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

                var groupOfUser = metaData.Sysusergroup
                    .Where(c => c.Userid == (int)user.Userid && c.Sysgroup.Appid == (int)refreshToken.Appid)
                    .FirstOrDefault();
                var lstMenu = (from c in metaData.Sysgroupmenu
                    join p in metaData.Sysmenu on c.Menuid equals p.Menuid
                    where c.Groupid == groupOfUser.Groupid && p.State != null
                    select new MenuOfUserModel
                    {
                        State = p.State,
                        Function = c.Function
                    }).ToList();

                var unit = metaData.Sysunit.FirstOrDefault(c => c.Unitcode == user.Unitcode);
                var userModelConvert = new UserModel
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
                var tokenGenerate = generateJwtToken(userModelConvert, ipAddress, newRefreshToken.Token);
                adapter.SaveEntity(tokenGenerate.GetIntanceEntity(), true);

                return new BaseAuthenticateResponse(userModelConvert, tokenGenerate.GenerateToken,
                    newRefreshToken.Token);
            }
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public ApiResponse<BaseAuthenticateResponse> BaseAuthenticate_Fuse(AuthenticateRequest model, string ipAddress)
    {
        using (var adapter = new DataAccessAdapterFactory().CreateAdapter())
        {
            var unitOfWork2 = new UnitOfWork2();
            var metaData = new LinqMetaData(adapter);
            var password = MD5Hash(_appSettings.Value.Authorize.ClientSecret + model.Password);
            var user = metaData.Sysuser.FirstOrDefault(c => c.Email == model.Username && c.Password == password);
            if (user == null)
                return ApiResponse<BaseAuthenticateResponse>.Generate(GeneralCode.NotFound,
                    "Tài khoản hoặc mật khẩu không đúng");
            var appId = model.AppId;
            if (appId == null || appId == 0)
                appId = (int)metaData.Sysapp.First(c => c.Dinhdanhapp == model.DinhDanhApp).Appid;
            else if (user.Trangthai != 1)
                return ApiResponse<BaseAuthenticateResponse>.Generate(GeneralCode.Error,
                    "Tài khoản chưa được kích hoạt.");

            var userModelConvert = new UserModel
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
            var jwtToken = BaseGenerateJwtToken(userModelConvert, ipAddress, refreshToken.Token, false);
            unitOfWork2.AddDeleteEntitiesDirectlyCall(nameof(SysRefreshtokenEntity),
                new RelationPredicateBucket(SysRefreshtokenFields.Expires < DateTime.Now));
            unitOfWork2.AddForSave(jwtToken, true);

            unitOfWork2.CommitOrder = new List<UnitOfWorkBlockType>
            {
                UnitOfWorkBlockType.Deletes,
                UnitOfWorkBlockType.DeletesPerformedDirectly,
                UnitOfWorkBlockType.Inserts
            };

            unitOfWork2.Commit(adapter, true);

            return new BaseAuthenticateResponse(userModelConvert, jwtToken.GenerateToken, refreshToken.Token);
        }
    }

    private string MD5Hash(string text)
    {
        MD5 md5 = new MD5CryptoServiceProvider();

        //compute hash from the bytes of text  
        md5.ComputeHash(Encoding.ASCII.GetBytes(text));

        //get hash result after compute it  
        var result = md5.Hash;

        var strBuilder = new StringBuilder();
        for (var i = 0; i < result.Length; i++)
            //change it into 2 hexadecimal digits  
            //for each byte  
            strBuilder.Append(result[i].ToString("x2"));

        return strBuilder.ToString();
    }

    // helper methods

    private RefreshToken generateJwtToken(UserModel user, string ipAddress, string refeshToken)
    {
        return BaseGenerateJwtToken(user, ipAddress, refeshToken);
    }

    private RefreshToken BaseGenerateJwtToken(UserModel user, string ipAddress, string refeshToken,
        bool isAddFull = true)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_appSettings.Value.Secret);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new(Constants.ClaimTypes.UserName, user.Username),
                new(Constants.ClaimTypes.FullName, user.FullName),
                new(Constants.ClaimTypes.UserId, user.Id.ToString()),
                new(Constants.ClaimTypes.AppId, user.AppId.ToString()),
                new(Constants.ClaimTypes.UnitCode, user.UnitCode ?? ""),
                new(Constants.ClaimTypes.UnitName, user.UnitName ?? "")
            }),
            Expires = DateTime.UtcNow.AddDays(1),
            SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        if (isAddFull)
        {
            tokenDescriptor.Subject.AddClaim(new Claim(Constants.ClaimTypes.Tag, user.Tag ?? ""));
            tokenDescriptor.Subject.AddClaim(new Claim(Constants.ClaimTypes.UserType,
                user.UserType == null ? "" : user.ToString()));
            foreach (var item in user.lstMenu)
                tokenDescriptor.Subject.AddClaim(
                    new Claim(ClaimTypes.Role, string.Join(",", item.State, item.Function)));
        }

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return new RefreshToken
        {
            Userid = user.Id,
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
                new(Constants.ClaimTypes.UserId, user.Id.ToString()),
                new(Constants.ClaimTypes.AppId, user.AppId.ToString()),
                new(Constants.ClaimTypes.UserName, user.Username),
                new(Constants.ClaimTypes.FullName, user.FullName),
                new(Constants.ClaimTypes.UnitCode, user.UnitCode ?? ""),
                new(Constants.ClaimTypes.UnitName, user.UnitName ?? ""),
                new(Constants.ClaimTypes.Tag, user.Tag ?? ""),
                new(Constants.ClaimTypes.UserType, user.UserType == null ? "" : user.ToString())
            }),
            Expires = DateTime.UtcNow.AddHours(1),
            SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return new RefreshToken
        {
            Userid = user.Id,
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
}