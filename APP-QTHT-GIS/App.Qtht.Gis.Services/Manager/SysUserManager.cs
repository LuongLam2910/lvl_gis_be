using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using App.Core.Common;
using App.QTHTGis.Dal;
using App.QTHTGis.Dal.DatabaseSpecific;
using App.QTHTGis.Dal.EntityClasses;
using App.QTHTGis.Dal.HelperClasses;
using App.QTHTGis.Dal.Linq;
using App.Qtht.Services.Models;
using Microsoft.Extensions.Options;
using SD.LLBLGen.Pro.LinqSupportClasses;
using SD.LLBLGen.Pro.ORMSupportClasses;
using SD.LLBLGen.Pro.QuerySpec;
using static App.Core.Common.Constants;
using static App.Qtht.Services.Models.SysUserModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Reflection;
using DocumentFormat.OpenXml.Spreadsheet;

namespace App.QTHTGis.Services.Manager;

public interface ISysUserManager
{
    Task<ApiResponse<bool>> CheckSdt(string sdt);
    Task<ApiResponse<object>> SelectUserByUserName(string userName);
    Task<ApiResponse> ChangePassword(ChangePasswordModel model);
    Task<ApiResponse> CheckPassword(string pass);
    Task<ApiResponse> ResetPassword(ResetPasswordModel model);

    Task<ApiResponse<UserInsertViewModel>> SelectFormInsertById();

    Task<ApiResponse> InsertUserAndUserGroup(InsertUpdateModel model);

    Task<ApiResponse<SysuserEntity>> RegisterUserClientMobile(InsertUpdateModel model);

    Task<ApiResponse> UpdateUserAndUserGroup(UpdateUserModel model);

    Task<ApiResponse> DeleteUserAndUserGroup(int key);

    Task<ApiResponse<UserUpdateDetailViewModel>> SelectFormUpdateDetailById(int key);

    Task<ApiResponse> UpdateUserGroup(SelectListUserGroup model);

    Task<ApiResponse> InsertUserClient(SysuserEntity model);

    Task<ApiResponse<List<SysGroupModel.SysGroupSelectByAppidModel>>> SelectByAppId(string appId, int userId);

    Task<ApiResponse> UpdateUserMenuFunction(List<SysusermenuEntity> lstEntity);

    Task<ApiResponse<IEnumerable<object>>> SelectAllSysApp();

    Task<ApiResponse<UserDetailModel>> GetDetails(long userid);

    Task<ApiResponse<DetailUserModel>> GetUserDetails(long userid);

    Task<ApiResponse<object>> GetInformationsForForm(long? userid);

    Task<ApiResponse<IEnumerable<UserSelect>>> SelectAllUser();
    Task<ApiResponse<UserInfoModel>> GetByUserId(long userId);

    Task<ApiResponse<List<CanBoSelectModel>>> GetCanBoListByPhongBan(string phongBan);

    Task<ApiResponse<List<CanBoSelectModel>>> GetCanBoCapDuoiList();
    Task<ApiResponse<CanBoSelectAndUserLevelModel>> GetCanBoCapTrenList();

    //Add new if transfer to App Bac Giang
    Task<ApiResponse<List<UserInfoModel>>> GetUserInUnit(string unitCode);
    Task<ApiResponse<List<UserInfoModel>>> GetUserInfoByListId(List<long> lstUserId);
    Task<ApiResponse<List<UserInfoModel>>> GetUserHigherLevelSameUnitUserCurrent();

    Task<ApiResponse<int>> InsertUserAppCongAn(RegisterUserAppCongAnModel model);
    Task<ApiResponse<SysGroupModel.SysGroupSelectByAppidCusTomModel>> SelectByAppIdCustom(string appId, int userId);
    Task<ApiResponse> UploadAvatar(IList<IFormFile> files, [FromForm] FileUserAvatarModel model);
    //Task<ApiResponse<long>> SelectUserGroup(string username);
}

public class SysUserManager : ISysUserManager
{
    private readonly AppSettingModel _appSetting;
    private readonly ICurrentContext _currentContext;

    public SysUserManager(IOptionsSnapshot<AppSettingModel> appSetting, ICurrentContext currentContext)
    {
        _appSetting = appSetting.Value;
        _currentContext = currentContext;
    }

    public async Task<ApiResponse<List<UserInfoModel>>> GetUserInfoByListId(List<long> lstUserId)
    {
        using (var adapter = new DataAccessAdapterFactory().CreateAdapter())
        {
            var metaData = new LinqMetaData(adapter);
            return await metaData.Sysuser.Where(c => c.Trangthai == 1 && lstUserId.Contains(c.Userid)).Select(c =>
                new UserInfoModel
                {
                    Userid = c.Userid,
                    Unitcode = c.Unitcode,
                    Appid = c.Appid,
                    Fullname = c.Fullname,
                    IdChucvu = c.IdChucvu,
                    Loaiuser = c.Loaiuser,
                    Username = c.Username
                }).ToListAsync();
        }
    }

    public async Task<ApiResponse<List<UserInfoModel>>> GetUserInUnit(string unitCode)
    {
        using (var adapter = new DataAccessAdapterFactory().CreateAdapter())
        {
            var metaData = new LinqMetaData(adapter);
            return await metaData.Sysuser.Where(c => c.Trangthai == 1 && c.Unitcode.StartsWith(unitCode)).Select(c =>
                new UserInfoModel
                {
                    Userid = c.Userid,
                    Unitcode = c.Unitcode,
                    Appid = c.Appid,
                    Fullname = c.Fullname,
                    IdChucvu = c.IdChucvu,
                    Loaiuser = c.Loaiuser,
                    Username = c.Username
                }).ToListAsync();
        }
    }

    public async Task<ApiResponse> ChangePassword(ChangePasswordModel model)
    {
        try
        {
            using (var adapter = new DataAccessAdapterFactory().CreateAdapter())
            {
                var metaData = new LinqMetaData(adapter);
                var user = metaData.Sysuser.FirstOrDefault(c => c.Userid == _currentContext.UserId);
                var password = MD5Hash(_appSetting.Authorize.ClientSecret + model.OldPassword);
                if (password == user.Password)
                {
                    user.Password = MD5Hash(_appSetting.Authorize.ClientSecret + model.NewPassword);
                    user.IsNew = false;
                    await adapter.SaveEntityAsync(user);
                    return GeneralCode.Success;
                }

                return ApiResponse.Generate(GeneralCode.Error, "Nhập mật khẩu cũ sai");
            }
        }
        catch (ORMException ex)
        {
            return ApiResponse.Generate(GeneralCode.Error, ex.Message);
        }
    }

    public async Task<ApiResponse> CheckPassword(string passs)
    {
        try
        {
            using (var adapter = new DataAccessAdapterFactory().CreateAdapter())
            {
                var metaData = new LinqMetaData(adapter);
                var user = metaData.Sysuser.FirstOrDefault(c => c.Userid == _currentContext.UserId);
                var password = MD5Hash(_appSetting.Authorize.ClientSecret + passs);
                if (password == user.Password)
                {
                    return GeneralCode.Success;
                }

                return ApiResponse.Generate(GeneralCode.Error, "Nhập mật khẩu cũ sai");
            }
        }
        catch (ORMException ex)
        {
            return ApiResponse.Generate(GeneralCode.Error, ex.Message);
        }
    }


    public async Task<ApiResponse<IEnumerable<object>>> SelectAllSysApp()
    {
        try
        {
            return await Task.Run(() =>
            {
                using (var adapter = new DataAccessAdapterFactory().CreateAdapter())
                {
                    var fields = new ResultsetFields(3);
                    var collection = new EntityCollection<SysappEntity>();
                    var sortExpression = new SortExpression(SysappFields.Appid | SortOperator.Ascending);
                    adapter.FetchEntityCollection(collection, null, 0, sortExpression);
                    return ApiResponse<IEnumerable<object>>.Generate(collection, GeneralCode.Success);
                }
            });
        }
        catch (Exception ex)
        {
            return ApiResponse<IEnumerable<object>>.Generate(null, GeneralCode.Error, ex.Message);
        }
    }

    public async Task<ApiResponse<IEnumerable<UserSelect>>> SelectAllUser()
    {
        try
        {
            return await Task.Run(() =>
            {
                using (var adapter = new DataAccessAdapterFactory().CreateAdapter())
                {
                    var collect = new EntityCollection<SysuserEntity>();

                    var filter = new RelationPredicateBucket();

                    filter.PredicateExpression.Add(SysuserFields.Trangthai == 1);

                    adapter.FetchEntityCollection(collect, filter);

                    var data = collect.Select(c => new UserSelect
                    {
                        Userid = c.Userid,
                        Unitcode = c.Unitcode,
                        Fullname = c.Fullname,
                        Username = c.Username,
                        Phongban = c.Phongban
                    }).ToList();

                    return ApiResponse<IEnumerable<UserSelect>>.Generate(data, GeneralCode.Success, null);
                }
            });
        }
        catch (Exception ex)
        {
            return ApiResponse<IEnumerable<UserSelect>>.Generate(null, GeneralCode.Error, ex.Message);
        }
    }

    public async Task<ApiResponse<SysGroupModel.SysGroupSelectByAppidCusTomModel>> SelectByAppIdCustom(string appId, int userId)
    {
        try
        {
            return await Task.Run(() =>
            {
                var collectMenu = new EntityCollection<SysmenuEntity>();
                var filter =
                    new RelationPredicateBucket(
                        SysmenuFields.Appid.In(appId.Split(",").Where(c => c != "" && c != null).ToList()));
                // RelationPredicateBucket filter = new RelationPredicateBucket(SysmenuFields.Appid.In(appId.Split(",").Where(c => c != "" && c != null).Select(c => int.Parse(c)).ToList()));

                var path = new PrefetchPath2(EntityType.SysmenuEntity);
                var predicateSysApp = new PredicateExpression(SysusermenuFields.Userid == userId);
                path.Add(SysmenuEntity.PrefetchPathSysusermenus, 0, predicateSysApp);

                using (var adapter = new DataAccessAdapter())
                {
                    adapter.FetchEntityCollection(collectMenu, filter, path);
                    //FIXME: eo biet nhu nao cho nay
                    var appCurrent = new SysappEntity(_currentContext.AppId);
                    adapter.FetchEntity(appCurrent);
                    return new SysGroupModel.SysGroupSelectByAppidCusTomModel
                    {
                        DinhDanhApp = appCurrent.Dinhdanhapp,
                        lstDetails = collectMenu.OrderBy(c => c.Sothutu).Select(c =>
                            new SysGroupModel.SysGroupSelectByAppidCusTomModel.SysGroupSelectByAppIdModelDetail
                            {
                                Menuid = c.Menuid,
                                Machucnang = c.Machucnang,
                                Macha = c.Macha,
                                Tenchucnang = c.Tenchucnang,
                                Appid = c.Appid,
                                Function = c.Sysusermenus.FirstOrDefault() == null
                                    ? new[] { "" }
                                    : c.Sysusermenus.First().Function.Split(','),
                                IsNew = c.Sysusermenus.FirstOrDefault() == null ? true : false
                            }).ToList()
                    };
                }
            });
        }
        catch (Exception ex)
        {
            return ApiResponse<SysGroupModel.SysGroupSelectByAppidCusTomModel>.Generate(GeneralCode.Error, ex.Message);
        }
    }

    public async Task<ApiResponse<List<SysGroupModel.SysGroupSelectByAppidModel>>> SelectByAppId(string appId, int userId)
    {
        try
        {
            return await Task.Run(() =>
            {
                var collectMenu = new EntityCollection<SysmenuEntity>();
                RelationPredicateBucket filter = new RelationPredicateBucket(SysmenuFields.Appid.In(appId.Split(",").Where(c => c != "" && c != null).Select(c => int.Parse(c)).ToList()));

                PrefetchPath2 path = new PrefetchPath2(EntityType.SysmenuEntity);
                PredicateExpression predicateSysApp = new PredicateExpression(SysusermenuFields.Userid == userId);
                path.Add(SysmenuEntity.PrefetchPathSysusermenus, 0, predicateSysApp);

                using (var adapter = new DataAccessAdapter())
                {
                    adapter.FetchEntityCollection(collectMenu, filter, path);
                }
                return collectMenu.OrderBy(c => c.Sothutu).Select(c => new SysGroupModel.SysGroupSelectByAppidModel
                {
                    Menuid = c.Menuid,
                    Machucnang = c.Machucnang,
                    Macha = c.Macha,
                    Tenchucnang = c.Tenchucnang,
                    Appid = c.Appid,
                    Function = c.Sysusermenus.FirstOrDefault() == null ? new string[] { "" } : c.Sysusermenus.First().Function.Split(','),
                    IsNew = c.Sysusermenus.FirstOrDefault() == null ? true : false
                }).ToList();
            });
        }
        catch (Exception ex)
        {
            return ApiResponse<List<SysGroupModel.SysGroupSelectByAppidModel>>.Generate(GeneralCode.Error, ex.Message);
        }
    }

    public async Task<ApiResponse> UpdateUserMenuFunction(List<SysusermenuEntity> lstEntity)
    {
        using (var adapter = new DataAccessAdapterFactory().CreateAdapter())
        {
            try
            {
                var unitOfWork2 = new UnitOfWork2();
                var lstDelete = lstEntity.Where(c => c.Function == "").ToList();
                unitOfWork2.AddDeleteEntitiesDirectlyCall(nameof(SysusermenuEntity),
                    new RelationPredicateBucket(
                        (Predicate)SysusermenuFields.Userid.In(lstDelete.Select(c => c.Userid).Distinct().ToList())
                        & (Predicate)SysusermenuFields.Menuid.In(lstDelete.Select(c => c.Menuid))));
                unitOfWork2.AddCollectionForSave(new EntityCollection<SysusermenuEntity>(lstEntity
                    .Where(c => c.Function != "").Select(c => new SysusermenuEntity
                    {
                        Userid = c.Userid,
                        Menuid = c.Menuid,
                        Function = c.Function,
                        IsNew = c.IsNew
                    })));
                unitOfWork2.CommitOrder = new List<UnitOfWorkBlockType>
                {
                    UnitOfWorkBlockType.DeletesPerformedDirectly, UnitOfWorkBlockType.Inserts,
                    UnitOfWorkBlockType.Updates
                };
                await unitOfWork2.CommitAsync(adapter);
                return GeneralCode.Success;
            }
            catch (ORMException ex)
            {
                return ApiResponse.Generate(GeneralCode.Error, ex.Message);
            }
        }
    }

    public async Task<ApiResponse<UserInsertViewModel>> SelectFormInsertById()
    {
        return await Task.Run(() =>
        {
            using (var adapter = new DataAccessAdapterFactory().CreateAdapter())
            {
                var metaData = new LinqMetaData(adapter);

                return new UserInsertViewModel
                {
                    ListApp = metaData.Sysapp.Select(c => new UserInsertViewModel.AppForUserModel
                    {
                        Id = (int)c.Appid,
                        Name = c.Tenapp,
                        DinhDanhApp = c.Dinhdanhapp
                    }).ToList(),

                    /**
                     * 2021-07-05 - Unused, remove logic
                     */
                    //ListUnit = metaData.Sysunit.Select(c => new SysUserModel.UserInsertViewModel.UnitForUserModel
                    //{
                    //    UnitCode = c.Unitcode,
                    //    UnitName = c.Tendonvi
                    //}).ToList(),
                    //ListTudien = metaData.Systudien.Where(c => c.Fieldname == "DMCV").Select(c => new SysUserModel.UserInsertViewModel.TudienForUserModel
                    //{
                    //    Id = (int)c.Id,
                    //    Name = c.TenTudien
                    //}).ToList(),
                    ListGroup = metaData.Sysgroup.Select(c => new UserInsertViewModel.GroupForUserModel
                    {
                        Id = (int)c.Groupid,
                        Name = c.Groupname,
                        Appid = (decimal)c.Appid
                    }).ToList()
                };
            }
        });
    }

    public async Task<ApiResponse> InsertUserAndUserGroup(InsertUpdateModel model)
    {
        using (var adapter = new DataAccessAdapterFactory().CreateAdapter())
        {
            try
            {
                model.UserData.Password = MD5Hash(_appSetting.Authorize.ClientSecret + model.UserData.Password);
                var metaData = new LinqMetaData(adapter);
                if (metaData.Sysuser.FirstOrDefault(c => c.Username == model.UserData.Username) != null)
                    return ApiResponse.Generate(GeneralCode.Error, "Tài khoản đã tồn tại.");

                await adapter.SaveEntityAsync(model.UserData, true);
                var ListGroupUserConvert = model.ListGroupId.Select(c => new SysusergroupEntity
                {
                    Userid = model.UserData.Userid,
                    Groupid = c
                });
                var ListUserGroup = new EntityCollection<SysusergroupEntity>(ListGroupUserConvert);
                await adapter.SaveEntityCollectionAsync(ListUserGroup);
                return GeneralCode.Success;
            }
            catch (Exception ex)
            {
                return ApiResponse.Generate(GeneralCode.Error, ex.Message);
            }
        }
    }

    public async Task<ApiResponse<SysuserEntity>> RegisterUserClientMobile(InsertUpdateModel model)
    {
        using (var adapter = new DataAccessAdapterFactory().CreateAdapter())
        {
            try
            {
                model.UserData.Password = MD5Hash(_appSetting.Authorize.ClientSecret + model.UserData.Password);

                await adapter.SaveEntityAsync(model.UserData, true);

                var ListGroupUserConvert = model.ListGroupId.Select(c => new SysusergroupEntity
                {
                    Userid = model.UserData.Userid,
                    Groupid = c
                });

                var ListUserGroup = new EntityCollection<SysusergroupEntity>(ListGroupUserConvert);

                await adapter.SaveEntityCollectionAsync(ListUserGroup);

                return model.UserData;
            }
            catch (Exception ex)
            {
                return ApiResponse<SysuserEntity>.Generate(GeneralCode.Error, ex.Message);
            }
        }
    }

    public async Task<ApiResponse> InsertUserClient(SysuserEntity model)
    {
        using (var adapter = new DataAccessAdapterFactory().CreateAdapter())
        {
            try
            {
                model.Password = MD5Hash(_appSetting.Authorize.ClientSecret + model.Password);
                var metaData = new LinqMetaData(adapter);
                if (metaData.Sysuser.FirstOrDefault(c => c.Username == model.Username) != null)
                    return ApiResponse.Generate(GeneralCode.Error, "Tài khoản đã tồn tại.");
                await adapter.SaveEntityAsync(model, true);
                var ListGroupUserConvert = new SysusergroupEntity
                {
                    Userid = model.Userid,
                    Groupid = 1
                };
                await adapter.SaveEntityAsync(ListGroupUserConvert);
                return GeneralCode.Success;
            }
            catch (Exception ex)
            {
                return ApiResponse.Generate(GeneralCode.Error, ex.Message);
            }
        }
    }

    public async Task<ApiResponse<UserUpdateDetailViewModel>> SelectFormUpdateDetailById(int key)
    {
        try
        {
            return await Task.Run(() =>
            {
                using (var adapter = new DataAccessAdapterFactory().CreateAdapter())
                {
                    var metaData = new LinqMetaData(adapter);
                    var user = metaData.Sysuser.First(c => c.Userid == key);

                    // Get ChucVu information
                    var chucVu = metaData.Dmchucvu.SingleOrDefault(c => c.Id == user.IdChucvu);

                    var chucVuDTO = new DmChucVuModel.ChucVuSelectModel();

                    if (chucVu != null)
                    {
                        chucVuDTO.Id = chucVu.Id;
                        chucVuDTO.Ten = chucVu.Ten;
                        //chucVuDTO.Cap = chucVu.Cap;
                    }

                    return new UserUpdateDetailViewModel
                    {
                        TuDien = metaData.Systudien.Where(c => c.Fieldname == "DMCV").Select(c =>
                            new UserUpdateDetailViewModel.UserTuDienViewModel
                            {
                                TuDienId = (int)c.Id,
                                TenTuDien = c.TenTudien
                            }).ToList(),
                        Units = metaData.Sysunit.Select(c => new UserUpdateDetailViewModel.UserUnitViewModel
                        {
                            UnitCode = c.Unitcode,
                            UnitName = c.Tendonvi
                        }).ToList(),
                        App = metaData.Sysgroup.Select(c => new UserUpdateDetailViewModel.UserViewAppModel
                        {
                            Id = (int)c.Groupid,
                            AppName = c.Groupname,
                            AppId = (long)c.Appid
                        }).ToList(),
                        User = user,
                        UserGroups = metaData.Sysusergroup.Where(c => c.Userid == user.Userid).Select(c =>
                            new UserUpdateDetailViewModel.UserGroupModel
                            {
                                GroupId = (int)c.Groupid,
                                UserId = (int)c.Userid
                            }).ToList(),
                        ChucVu = chucVuDTO
                    };
                }
            });
        }
        catch (ORMException ex)
        {
            return ApiResponse<UserUpdateDetailViewModel>.Generate(null, GeneralCode.Error, ex.Message);
        }
    }

    public async Task<ApiResponse> UpdateUserAndUserGroup(UpdateUserModel model)
    {
        using (var adapter = new DataAccessAdapterFactory().CreateAdapter())
        {
            try
            {
                var unitOfWork2 = new UnitOfWork2();

                var metaData = new LinqMetaData(adapter);
                var user = metaData.Sysuser.FirstOrDefault(c => c.Userid == model.UserData.userid);

                if (user == null) throw new NullReferenceException("Không tồn tại tài khoản.");

                var userUpdate = new SysuserEntity(model.UserData.userid);
                adapter.FetchEntity(userUpdate);
                userUpdate.Userid = model.UserData.userid;
                userUpdate.Trangthai = model.UserData.trangthai;
                userUpdate.Unitcode = model.UserData.unitcode;
                userUpdate.Username = model.UserData.username;
                userUpdate.Email = model.UserData.email;
                userUpdate.Fullname = model.UserData.fullname;
                userUpdate.Password = user.Password;
                userUpdate.Phone = model.UserData.phone;
                userUpdate.IdChucvu = model.UserData.idchucvu;
                userUpdate.Loaiuser = model.UserData.loaiuser;
                userUpdate.Diachi = model.UserData.diachi;
                userUpdate.IsNew = false;
                unitOfWork2.AddForSave(userUpdate, true);

                unitOfWork2.CommitOrder = new List<UnitOfWorkBlockType>
                {
                    UnitOfWorkBlockType.Updates,
                    UnitOfWorkBlockType.DeletesPerformedDirectly,
                    UnitOfWorkBlockType.Inserts
                };

                await unitOfWork2.CommitAsync(adapter);
                return GeneralCode.Success;
            }

            catch (Exception ex)
            {
                return ApiResponse.Generate(GeneralCode.Error, ex.Message);
            }
        }
    }

    public async Task<ApiResponse> DeleteUserAndUserGroup(int key)
    {
        try
        {
            using (var adapter = new DataAccessAdapterFactory().CreateAdapter())
            {
                var unitOfWork2 = new UnitOfWork2();

                var sysUserGroup = new EntityCollection<SysusergroupEntity>();
                var sysUserMenu = new EntityCollection<SysusermenuEntity>();
                var sysUser = new EntityCollection<SysuserEntity>();

                adapter.FetchEntityCollection(sysUserGroup,
                    new RelationPredicateBucket(SysusergroupFields.Userid == key));
                adapter.FetchEntityCollection(sysUserMenu,
                    new RelationPredicateBucket(SysusermenuFields.Userid == key));
                adapter.FetchEntityCollection(sysUser, new RelationPredicateBucket(SysuserFields.Userid == key));

                unitOfWork2.AddDeleteEntitiesDirectlyCall(nameof(SysusergroupEntity),
                    new RelationPredicateBucket(SysusergroupFields.Userid == key));
                unitOfWork2.AddDeleteEntitiesDirectlyCall(nameof(SysusermenuEntity),
                    new RelationPredicateBucket(SysusermenuFields.Userid == key));
                unitOfWork2.AddDeleteEntitiesDirectlyCall(nameof(SysuserEntity),
                    new RelationPredicateBucket(SysuserFields.Userid == key));

                await unitOfWork2.CommitAsync(adapter, true);

                sysUserGroup.LogObject(_currentContext.UserId, UserAction.Delete, "Xóa SysUserGroup theo UserId",
                    _currentContext.IpClient, _currentContext.UserName, _currentContext.AppId);
                sysUserMenu.LogObject(_currentContext.UserId, UserAction.Delete, "Xóa SysUserMenu theo UserId",
                    _currentContext.IpClient, _currentContext.UserName, _currentContext.AppId);
                sysUser.LogObject(_currentContext.UserId, UserAction.Delete, "Xóa SysUser theo UserId",
                    _currentContext.IpClient, _currentContext.UserName, _currentContext.AppId);

                return GeneralCode.Success;
            }
        }
        catch (ORMEntityIsDeletedException ex)
        {
            return ApiResponse.Generate(GeneralCode.Error, ex.Message);
        }
    }

    public async Task<ApiResponse> UpdateUserGroup(SelectListUserGroup model)
    {
        try
        {
            using (var adapter = new DataAccessAdapterFactory().CreateAdapter())
            {
                var unitOfWork2 = new UnitOfWork2();
                unitOfWork2.AddDeleteEntitiesDirectlyCall(nameof(SysusergroupEntity),
                    new RelationPredicateBucket(SysusergroupFields.Userid == model.UserId));
                unitOfWork2.CommitOrder = new List<UnitOfWorkBlockType>
                    { UnitOfWorkBlockType.DeletesPerformedDirectly, UnitOfWorkBlockType.Inserts };
                var ListGroupUserConvert = model.ListGroupId.Select(c => new SysusergroupEntity
                {
                    Userid = model.UserId,
                    Groupid = c
                });
                var ListUserGroup = new EntityCollection<SysusergroupEntity>(ListGroupUserConvert);
                unitOfWork2.AddCollectionForSave(ListUserGroup);
                await unitOfWork2.CommitAsync(adapter);
                return GeneralCode.Success;
            }
        }
        catch (ORMException ex)
        {
            return ApiResponse.Generate(GeneralCode.Error, ex.Message);
        }
    }

    public async Task<ApiResponse<UserDetailModel>> GetDetails(long userid)
    {
        using (var adapter = new DataAccessAdapterFactory().CreateAdapter())
        {
            try
            {
                return await Task.Run(() =>
                {
                    var result = new UserDetailModel();

                    var metaData = new LinqMetaData(adapter);

                    var user = metaData.Sysuser
                        .Select(i => new
                        {
                            i.Userid,
                            i.Username,
                            i.Email,
                            i.Fullname,
                            i.Phone,
                            i.Trangthai,
                            i.Appid,
                            i.Unitcode,
                            i.Phongban,
                            i.IdChucvu,
                            i.Cap,
                            i.Loaiuser
                        })
                        .FirstOrDefault(i => i.Userid == userid);

                    result.Username = user.Username;
                    result.Email = user.Email;
                    result.Fullname = user.Fullname;
                    result.Phone = user.Phone;
                    result.Trangthai = user.Trangthai == 1 ? "Sử dụng" : "Không sử dụng";
                    if (!string.IsNullOrEmpty(user.Appid))
                        result.AppName = metaData.Sysapp.FirstOrDefault(x => x.Appid == int.Parse(user.Appid))?.Tenapp;
                    result.UnitName = metaData.Sysunit.FirstOrDefault(x => x.Unitcode == user.Unitcode)?.Tendonvi;
                    result.PhongBanName = metaData.Sysunit.FirstOrDefault(x => x.Unitcode == user.Phongban)?.Tendonvi;
                    result.ChucVuName = metaData.Dmchucvu.FirstOrDefault(x => x.Id == user.IdChucvu)?.Ten;
                    result.Loaiuser = user.Loaiuser;

                    return result;
                });
            }
            catch (ORMException ex)
            {
                return ApiResponse<UserDetailModel>.Generate(null, GeneralCode.Error, ex.Message);
            }
        }
    }


    public async Task<ApiResponse<DetailUserModel>> GetUserDetails(long userid)
    {
        using (var adapter = new DataAccessAdapterFactory().CreateAdapter())
        {
            try
            {
                return await Task.Run(() =>
                {
                    var result = new DetailUserModel();

                    var metaData = new LinqMetaData(adapter);
                    string location = Directory.GetDirectoryRoot(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location)) + FileBase.File;
                    var user = metaData.Sysuser
                        .Select(i => new
                        {
                            i.Userid,
                            i.Username,
                            i.Email,
                            i.Fullname,
                            i.Phone,
                            i.Trangthai,
                            i.Appid,
                            i.Unitcode,
                            i.Phongban,
                            i.IdChucvu,
                            i.Cap,
                            i.Diachi,
                            i.Password,
                            i.Ngaysinh,
                            i.Imgavatar

                        })
                        .FirstOrDefault(i => i.Userid == userid);
                    result.Username = user.Username;
                    result.Email = user.Email;
                    result.Fullname = user.Fullname;
                    result.Phone = user.Phone;
                    result.Trangthai = user.Trangthai == 1 ? "Sử dụng" : "Không sử dụng";
                    result.Unitcode = user.Unitcode;
                    if (!string.IsNullOrEmpty(user.Appid))
                        result.AppName = metaData.Sysapp.FirstOrDefault(x => x.Appid == int.Parse(user.Appid))?.Tenapp;
                    result.UnitName = metaData.Sysunit.FirstOrDefault(x => x.Unitcode == user.Unitcode)?.Tendonvi;
                    result.PhongBanName = metaData.Sysunit.FirstOrDefault(x => x.Unitcode == user.Phongban)?.Tendonvi;
                    result.ChucVuName = metaData.Dmchucvu.FirstOrDefault(x => x.Id == user.IdChucvu)?.Ten;
                    result.Password = user.Password;
                    result.DiaChi = user.Diachi;
                    result.NgaySinh = user.Ngaysinh;
                    string path = user.Imgavatar.StartsWith("\\") ? user.Imgavatar : user.Imgavatar;
                    result.imgAvatar = path;
                    return result;
                });
            }
            catch (ORMException ex)
            {
                return ApiResponse<DetailUserModel>.Generate(null, GeneralCode.Error, ex.Message);
            }
        }
    }

    public async Task<ApiResponse<object>> GetInformationsForForm(long? userid)
    {
        using (var adapter = new DataAccessAdapterFactory().CreateAdapter())
        {
            try
            {
                return await Task.Run(() =>
                {
                    var metaData = new LinqMetaData(adapter);

                    var lstApp = metaData.Sysapp
                        .Where(i => i.Trangthai == 1)
                        .Select(i => new
                        {
                            i.Appid,
                            i.Tenapp,
                            i.Dinhdanhapp
                        })
                        .ToList();

                    //var lstGroup = metaData.Sysgroup
                    //    .Select(i => new
                    //    {
                    //        i.Groupid,
                    //        i.Groupname,
                    //        i.Appid,
                    //    })
                    //    .ToList();

                    var lstUnit = metaData.Sysunit
                        .Where(i => i.Trangthai == 1 && i.Type == 1)
                        .Select(i => new
                        {
                            i.Unitcode,
                            i.Tendonvi,
                            i.Dinhdanhapp,
                            i.IdTochuc,
                            i.Loaiunit
                        })
                        .ToList();

                    var lstPhongBan = metaData.Dmphongban
                        .Where(i => i.Trangthai == 1)
                        .Select(i => new
                        {
                            i.Ten,
                            i.Unitcode,
                        })
                        .ToList();

                    var lstChucVu = metaData.TochucChucvu
                        .Select(i => new
                        {
                            i.IdTochuc,
                            i.IdChucvu,
                            i.Cap,
                            i.Dmchucvu.Ten
                        })
                        .ToList();

                    return new
                    {
                        user = userid.HasValue
                            ? metaData.Sysuser
                                .Select(i => new
                                    {
                                        i.Userid,
                                        i.Username,
                                        i.Email,
                                        i.Fullname,
                                        i.Phone,
                                        i.Trangthai,
                                        i.Appid,
                                        i.Unitcode,
                                        i.Phongban,
                                        i.IdChucvu,
                                        i.Cap,
                                        i.Password,
                                        i.Loaiuser,
                                        i.Diachi
                                    }
                                )
                                .FirstOrDefault(i => i.Userid == userid)
                            : null,
                        lstApp,
                        lstUnit,
                        lstPhongBan,
                        lstChucVu
                    };
                });
            }
            catch (ORMException ex)
            {
                return ApiResponse<object>.Generate(null, GeneralCode.Error, ex.Message);
            }
        }
    }

    public async Task<ApiResponse<UserInfoModel>> GetByUserId(long userId)
    {
        using (var adapter = new DataAccessAdapterFactory().CreateAdapter())
        {
            try
            {
                return await Task.Run(() =>
                {
                    var entities = new EntityCollection<SysuserEntity>();
                    var filter = new RelationPredicateBucket(SysuserFields.Userid == userId);
                    adapter.FetchEntityCollection(entities, filter);
                    if (adapter.GetDbCount(entities, filter) == 0)
                        return ApiResponse<UserInfoModel>.Generate(null, GeneralCode.NotFound);

                    var model = new UserInfoModel();
                    model.Userid = entities[0].Userid;
                    model.Unitcode = entities[0].Unitcode;
                    model.Loaiuser = entities[0].Loaiuser;

                    return model;
                });
            }
            catch (ORMException ex)
            {
                return ApiResponse<UserInfoModel>.Generate(null, GeneralCode.Error, ex.Message);
            }
        }
    }

    public async Task<ApiResponse<List<CanBoSelectModel>>> GetCanBoListByPhongBan(string phongBan)
    {
        using (var adapter = new DataAccessAdapterFactory().CreateAdapter())
        {
            try
            {
                return await Task.Run(() =>
                {
                    var metaData = new LinqMetaData(adapter);

                    return metaData.Sysuser.Where(x => x.Trangthai == 1 && x.Phongban == phongBan)
                        .Select(x => new CanBoSelectModel
                        {
                            Value = x.Userid,
                            Name = x.Fullname
                        })
                        .ToList();
                });
            }
            catch (ORMException ex)
            {
                return ApiResponse<List<CanBoSelectModel>>.Generate(null, GeneralCode.Error, ex.Message);
            }
        }
    }

    public async Task<ApiResponse<List<CanBoSelectModel>>> GetCanBoCapDuoiList()
    {
        using (var adapter = new DataAccessAdapterFactory().CreateAdapter())
        {
            try
            {
                return await Task.Run(() =>
                {
                    var metaData = new LinqMetaData(adapter);

                    var currentUser = metaData.Sysuser.FirstOrDefault(x => x.Userid == _currentContext.UserId);

                    if (currentUser == null) throw new Exception("Không tồn tại user!");

                    return metaData.Sysuser
                        .Where(x => x.Trangthai == 1)
                        .Where(x => x.Unitcode == currentUser.Unitcode)
                        .Where(x => x.Phongban == currentUser.Phongban)
                        .Where(x => x.Cap > currentUser.Cap)
                        .Select(x => new CanBoSelectModel
                        {
                            Value = x.Userid,
                            Name = x.Fullname
                        })
                        .ToList();
                });
            }
            catch (ORMException ex)
            {
                return ApiResponse<List<CanBoSelectModel>>.Generate(null, GeneralCode.Error, ex.Message);
            }
        }
    }

    public async Task<ApiResponse<CanBoSelectAndUserLevelModel>> GetCanBoCapTrenList()
    {
        using (var adapter = new DataAccessAdapterFactory().CreateAdapter())
        {
            try
            {
                return await Task.Run(() =>
                {
                    var model = new CanBoSelectAndUserLevelModel();

                    var metaData = new LinqMetaData(adapter);

                    var currentUser = metaData.Sysuser.FirstOrDefault(x => x.Userid == _currentContext.UserId);

                    if (currentUser == null) throw new Exception("Không tồn tại user!");

                    model.lstCanBoCapTren = metaData.Sysuser
                        .Where(x => x.Trangthai == 1)
                        .Where(x => x.Unitcode == currentUser.Unitcode)
                        .Where(x => x.Phongban == currentUser.Phongban)
                        .Where(x => x.Cap < currentUser.Cap)
                        .Select(x => new CanBoSelectModel
                        {
                            Value = x.Userid,
                            Name = x.Fullname
                        })
                        .ToList();

                    model.Cap = currentUser.Cap;

                    return model;
                });
            }
            catch (ORMException ex)
            {
                return ApiResponse<CanBoSelectAndUserLevelModel>.Generate(null, GeneralCode.Error, ex.Message);
            }
        }
    }

    public async Task<ApiResponse<List<UserInfoModel>>> GetUserHigherLevelSameUnitUserCurrent()
    {
        using (var adapter = new DataAccessAdapterFactory().CreateAdapter())
        {
            var metaData = new LinqMetaData(adapter);
            var currentUser = metaData.Sysuser.First(c => c.Userid == _currentContext.UserId);
            var currentUnit = metaData.Sysunit.First(c => c.Unitcode == _currentContext.UnitCode);
            var toChucCuaDonVi = metaData.TochucChucvu.FirstOrDefault(c =>
                c.IdChucvu == currentUser.IdChucvu && c.IdChucvu == currentUnit.IdTochuc);
            if (toChucCuaDonVi == null) return new List<UserInfoModel>();

            return await (from c in metaData.Sysuser
                join p in metaData.Sysunit on c.Unitcode equals p.Unitcode
                join t in metaData.TochucChucvu on p.IdTochuc equals t.IdTochuc
                where t.IdChucvu == c.IdChucvu && t.Cap <= toChucCuaDonVi.Cap
                                               && c.Trangthai == 1 && c.Userid != _currentContext.UserId
                select new UserInfoModel
                {
                    Userid = c.Userid,
                    Unitcode = c.Unitcode,
                    Appid = c.Appid,
                    Fullname = c.Fullname,
                    IdChucvu = c.IdChucvu,
                    Loaiuser = c.Loaiuser,
                    Username = c.Username
                }).ToListAsync();
        }
    }


    public async Task<ApiResponse<object>> SelectUserByUserName(string userName)
    {
        using (var adapter = new DataAccessAdapterFactory().CreateAdapter())
        {
            try
            {
                return await Task.Run(() =>
                {
                    var entity = new EntityCollection<SysuserEntity>();
                    adapter.FetchEntityCollection(entity,
                        new RelationPredicateBucket(SysuserFields.Username == userName));
                    return entity.Select(
                        c => new
                        {
                            c.Userid,
                            c.Trangthai,
                            c.Unitcode,
                            c.Username,
                            c.Checkisyn,
                            c.Diachi,
                            c.Email,
                            c.Fullname,
                            c.Gioitinh,
                            c.Idsystudien,
                            c.Madbhc,
                            c.Ngaysinh,
                            c.Phone,
                            c.Quoctich,
                            c.Socmnd,
                            c.Imgavatar
                        }
                    ).ToList()[0];
                });
            }
            catch (ORMException ex)
            {
                return ApiResponse<object>.Generate(null, GeneralCode.Error, ex.Message);
            }
        }
    }

    public async Task<ApiResponse<bool>> CheckSdt(string sdt)
    {
        try
        {
            using (var adapter = new DataAccessAdapterFactory().CreateAdapter())
            {
                var metaData = new LinqMetaData(adapter);

                var lstData = (from entity in metaData.Sysuser
                    where entity.Username == sdt
                    select entity)?.ToList();
                if (lstData.Count() > 0) return true;
                return false;
            }
        }
        catch (ORMException ex)
        {
            return ApiResponse<bool>.Generate(false, GeneralCode.Error, ex.Message);
        }
    }

    public async Task<ApiResponse<int>> InsertUserAppCongAn(RegisterUserAppCongAnModel model)
    {
        try
        {
            using (var adapter = new DataAccessAdapterFactory().CreateAdapter())
            {
                var metaData = new LinqMetaData(adapter);
                SysuserEntity user;
                var lstAllMenu = new List<SysmenuEntity>();
                var unit = metaData.Sysunit.FirstOrDefault(c => c.Unitcode == model.UnitCode);
                if (!model.IsNew)
                {
                    user = metaData.Sysuser.First(c => c.Username == model.UserName);
                    user.IsNew = false;
                    user.IsDirty = true;
                }
                else
                {
                    //if (metaData.Sysuser.FirstOrDefault(c => c.Username == model.UserName) != null &&  model.UserName != "0000000000")
                    var checkSysuser = metaData.Sysuser.FirstOrDefault(c => c.Username == model.UserName || c.Username == Md5Function.Decrypt_phone(model.UserName) || c.Username == Md5Function.Encrypt_phone(model.UserName));
                    if (checkSysuser != null) return ApiResponse<int>.Generate(GeneralCode.Error, "Tài khoản đã tồn tại.");

                    user = new SysuserEntity();
                    user.IsNew = true;
                    user.IsDirty = true;
                    var appIdStr = metaData.Sysapp.First(c => c.Dinhdanhapp == "APPCONGAN").Appid.ToString();
                    if (unit == null)
                    {
                        //Trong truong nha dan tu dang ky thi add danh sach menu cho chu nha tro
                        var lstMenuParrentCode = metaData.Sysmenu
                            .Where(c => c.Appid == appIdStr && c.Machucnang.StartsWith("2") && c.Macha != null &&
                                        c.Macha != "").Select(c => c.Macha).ToList();
                        lstAllMenu = metaData.Sysmenu.Where(c =>
                            c.Appid == appIdStr &&
                            (c.Machucnang.StartsWith("2") || lstMenuParrentCode.Contains(c.Machucnang))).ToList();
                        //lstAllMenu = metaData.Sysmenu.Where(c => c.Appid == appIdStr && (c.Machucnang.StartsWith("2") || c.Machucnang.StartsWith("3") || c.Macha == null)).ToList();
                    }

                    user.Password = MD5Hash(_appSetting.Authorize.ClientSecret + model.Password);
                    user.Trangthai = 1;
                }

                user.Username = Md5Function.Encrypt_phone(model.UserName);
                user.Fullname = model.FullName;
                user.Userid = Convert.ToInt64(model.UserId);
                if (unit != null) user.Unitcode = model.UnitCode;
                await adapter.SaveEntityAsync(user, true);
                if (model.IsNew)
                {
                    if (model.GroupId != null)
                    {
                        var sysusergroupEntity = new SysusergroupEntity
                        {
                            IsNew = true,
                            Groupid = (int)model.GroupId,
                            Userid = user.Userid
                        };
                        adapter.SaveEntity(sysusergroupEntity);
                    }

                    if (lstAllMenu.Count > 0)
                    {
                        var lstInsert = lstAllMenu.Select(c => new SysusermenuEntity
                        {
                            IsNew = true,
                            Userid = user.Userid,
                            Menuid = c.Menuid,
                            Function = "1,2,3,4,5,6"
                        }).ToList();
                        adapter.SaveEntityCollection(new EntityCollection<SysusermenuEntity>(lstInsert));
                    }
                }

                return (int)user.Userid;
            }
        }
        catch (Exception ex)
        {
            return ApiResponse<int>.Generate(GeneralCode.Error, ex.Message);
        }
    }

    public async Task<ApiResponse> ResetPassword(ResetPasswordModel model)
    {
        try
        {
            using (var adapter = new DataAccessAdapterFactory().CreateAdapter())
            {
                var metaData = new LinqMetaData(adapter);
                var user = metaData.Sysuser.FirstOrDefault(c => c.Username == model.UserName);
                if (user != null)
                {
                    user.Password = MD5Hash(_appSetting.Authorize.ClientSecret + model.Password);
                    user.IsNew = false;
                    await adapter.SaveEntityAsync(user);
                    return GeneralCode.Success;
                }

                return ApiResponse.Generate(GeneralCode.Error, "Tài khoản không tồn tại");
            }
        }
        catch (ORMException ex)
        {
            return ApiResponse.Generate(GeneralCode.Error, ex.Message);
        }
    }

    //public async Task<ApiResponse<long>> SelectUserGroup(string username)
    //{
    //    try
    //    {
    //        using (var adapter = new DataAccessAdapterFactory().CreateAdapter())
    //        {
    //            var metaData = new LinqMetaData(adapter);
    //            var groupid = metaData.Sysusergroup.FirstOrDefault(x => x.Sysuser.Username == username)?.Groupid;
    //            return ApiResponse<long>.Generate((long)groupid, GeneralCode.Success, "Success");
    //        }
    //    }
    //    catch (ORMException ex)
    //    {
    //        return ApiResponse<long>.Generate(GeneralCode.Error, ex.Message);
    //    }
    //}

    public async Task<ApiResponse<object>> SelectOneCustom(int key)
    {
        using (var adapter = new DataAccessAdapterFactory().CreateAdapter())
        {
            try
            {
                return await Task.Run(() =>
                {
                    var entity = new EntityCollection<SysuserEntity>();
                    adapter.FetchEntityCollection(entity, new RelationPredicateBucket(SysuserFields.Userid == key));
                    return entity.Select(
                        c => new
                        {
                            c.Userid,
                            c.Trangthai,
                            c.Unitcode,
                            c.Username,
                            c.Checkisyn,
                            c.Diachi,
                            c.Email,
                            c.Fullname,
                            c.Gioitinh,
                            c.Idsystudien,
                            c.Madbhc,
                            c.Ngaysinh,
                            c.Phone,
                            c.Quoctich,
                            c.Socmnd,
                            c.Imgavatar
                        }
                    ).ToList()[0];
                });
            }
            catch (ORMException ex)
            {
                return ApiResponse<object>.Generate(null, GeneralCode.Error, ex.Message);
            }
        }
    }

    public async Task<ApiResponse> UploadAvatar(IList<IFormFile> files, FileUserAvatarModel model)
    {
        using (var adapter = new DataAccessAdapterFactory().CreateAdapter())
        {
            try
            {
                var unitOfWork2 = new UnitOfWork2();
                foreach (var item in files)
                {
                    if (item.Length > 0)
                    {
                        string rootPath = Directory.GetDirectoryRoot(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location));
                        if (item.Length > (_appSetting.FileLocation.MaxSize * 1048576)) // Bytes (B)
                        {
                            return ApiResponse.Generate(GeneralCode.Error, "Dung dượng file quá lớn");
                        }
                        string filePath = _appSetting.FileLocation.Path;
                        if (string.IsNullOrWhiteSpace(filePath))
                        {
                            filePath = rootPath + FileBase.File + @"\SysFileManager";
                        }

                        if (!string.IsNullOrWhiteSpace(model.folder))
                        {
                            filePath += @"\" + model.folder;
                        }

                        if (!Directory.Exists(filePath))
                        {
                            Directory.CreateDirectory(filePath);
                        }

                        string fullPath = filePath + @"\" + item.FileName;
                        using (var fileStream = item.OpenReadStream())

                        using (var stream = new FileStream(fullPath, FileMode.Create))
                        {

                            await item.CopyToAsync(stream);
                        }
                        string prePath = rootPath + FileBase.File;
                        string subPath = fullPath.Replace(prePath, "");

                        if (model.type == "Image")
                        {
                            var avtarUpdate = new SysuserEntity(_currentContext.UserId);
                            adapter.FetchEntity(avtarUpdate);
                            avtarUpdate.Imgavatar = subPath;
                            unitOfWork2.AddForSave(avtarUpdate, true);
                            await unitOfWork2.CommitAsync(adapter);
                            return GeneralCode.Success;
                        }
                    }
                }
                return ApiResponse.Generate(GeneralCode.Success, "Thành Công");
            }
            catch (System.Exception ex)
            {
                return ApiResponse.Generate(GeneralCode.Error, ex.Message);
            }
        }
    }

    public static string MD5Hash(string text)
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
}