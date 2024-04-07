using App.Core.Common;
using App.QTHTGis.Dal.EntityClasses;
using App.QTHTGis.Dal.HelperClasses;
using App.QTHTGis.Services.Manager;
using App.Qtht.Services.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SD.LLBLGen.Pro.ORMSupportClasses;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static App.Qtht.Services.Models.SysUserModel;
using Microsoft.AspNetCore.Http;

namespace App.QTHTGis.Api.Controllers;

[Authorize]
[Route("App/QthtGis/api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
[ApiController]
public class SysUserController : DefaultController<SysuserEntity>
{
    private readonly ISysUserManager _userManager;

    public SysUserController(ISysUserManager userManager)
    {
        _userManager = userManager;
    }

    protected override Func<SysuserEntity, object> SetSelectModelPaging()
    {
        return c => new {
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
            c.Password,
            c.Phone,
            c.Quoctich,
            c.Socmnd,
            c.Imgavatar,
            c.Appid
        };
    }

    protected override Func<SysuserEntity, object> SetModelSelectAll()
    {
        return c => new {
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
            c.Password,
            c.Phone,
            c.Quoctich,
            c.Socmnd,
            c.Imgavatar,
            c.Phongban,
            c.Appid
        };
    }

    protected override EntityField2[] SetFilterPaging()
    {
        return new[] { SysuserFields.Fullname, SysuserFields.Username, SysuserFields.Email, SysuserFields.Phone };
    }

    protected override EntityField2 SetFieldConditionPaging()
    {
        // Nếu không lọc theo điều kiện mặc định thì không sử dụng
        return SysuserFields.Userid;
    }

    protected override EntityField2 SetColumnKeySelectOne()
    {
        // Bắt buộc có khi sử dụng SelectOne
        return SysuserFields.Userid;
    }

    protected override SortClause SetSortSelectAll()
    {
        return SysuserFields.Userid | SortOperator.Descending;
    }

    protected override EntityField2 SetFieldKeyUpdateOrDelete()
    {
        return SysuserFields.Userid;
    }

    protected override SysuserEntity SetValueEntityUpdate(SysuserEntity source)
    {
        SysuserEntity entity = new();
        entity.Userid = source.Userid;
        entity.Trangthai = source.Trangthai;
        entity.Unitcode = source.Unitcode;
        entity.Username = source.Username;
        entity.Checkisyn = source.Checkisyn;
        entity.Diachi = source.Diachi;
        entity.Email = source.Email;
        entity.Fullname = source.Fullname;
        entity.Gioitinh = source.Gioitinh;
        entity.Idsystudien = source.Idsystudien;
        entity.Madbhc = source.Madbhc;
        entity.Ngaysinh = source.Ngaysinh;
        entity.Password = source.Password;
        entity.Phone = source.Phone;
        entity.Quoctich = source.Quoctich;
        entity.Socmnd = source.Socmnd;
        entity.Imgavatar = source.Imgavatar;
        entity.Appid = source.Appid;
        entity.IsNew = false;
        return entity;
    }

    [AllowAnonymous]
    [HttpPost]
    [Route("CheckSdt")]
    public async Task<ApiResponse<bool>> CheckSdt(string sdt)
    {
        return await _userManager.CheckSdt(sdt);
    }

    [HttpGet]
    [Route("SelectFormUpdateDetailById/{key}")]
    public async Task<ApiResponse<UserUpdateDetailViewModel>> SelectFormUpdateDetailById(int key)
    {
        return await _userManager.SelectFormUpdateDetailById(key);
    }

    [HttpGet]
    [Route("SelectFormInsertById/")]
    public async Task<ApiResponse<UserInsertViewModel>> SelectFormInsertById()
    {
        return await _userManager.SelectFormInsertById();
    }

    [AllowAnonymous]
    [HttpGet]
    [Route("SelectUserByUserName/{userName}")]
    public async Task<ApiResponse<object>> SelectUserByUserName(string userName)
    {
        return await _userManager.SelectUserByUserName(userName);
    }

    [Route("InsertUserAndUserGroup")]
    [HttpPost]
    [AllowAnonymous]
    public async Task<ApiResponse> InsertUserAndUserGroup([FromBody] InsertUpdateModel model)
    {
        return await _userManager.InsertUserAndUserGroup(model);
    }

    [Route("InsertUserClient")]
    [HttpPost]
    [AllowAnonymous]
    public async virtual Task<ApiResponse> InsertUserClient(SysuserEntity model)
    {
        return await _userManager.InsertUserClient(model);
    }

    [Route("InsertUserAppCongAn")]
    [HttpPost]
    [AllowAnonymous]
    public async Task<ApiResponse<int>> InsertUserAppCongAn([FromBody] RegisterUserAppCongAnModel model)
    {
        if (ModelState.IsValid)
        {
            return await _userManager.InsertUserAppCongAn(model);
        }

        return ApiResponse<int>.Generate(GeneralCode.Error, "Input incorrect.");
    }

    [Route("UpdateCustomUser")]
    [HttpPost]
    public async Task<ApiResponse> UpdateUserAndUserGroup([FromBody] UpdateUserModel model)
    {
        if (!ModelState.IsValid)
        {
            return null;
        }

        return await _userManager.UpdateUserAndUserGroup(model);
    }

    [HttpDelete]
    [Route("DeleteCustomUser/{key}")]
    public async virtual Task<ApiResponse> DeleteUserAndUserGroup([FromRoute] int key)
    {
        return await _userManager.DeleteUserAndUserGroup(key);
    }

    [Route("UpdateUserGroup")]
    [HttpPut]
    public async Task<ApiResponse> UpdateUserGroup([FromBody] SelectListUserGroup model)
    {
        return await _userManager.UpdateUserGroup(model);
    }

    [Route("ChangePassword")]
    [HttpPut]
    public async Task<ApiResponse> ChangePassword([FromBody] ChangePasswordModel model)
    {
        return await _userManager.ChangePassword(model);
    }

    [Route("CheckPassword")]
    [HttpGet]
    public async Task<ApiResponse> CheckPassword(string model)
    {
        return await _userManager.CheckPassword(model);
    }

    [Route("ResetPassword")]
    [HttpPut]
    [AllowAnonymous]
    public async Task<ApiResponse> ResetPassword([FromBody] ResetPasswordModel model)
    {
        return await _userManager.ResetPassword(model);
    }

    [HttpGet]
    [Route("SelectAllSysApp")]
    public async Task<ApiResponse<IEnumerable<object>>> SelectAllSysApp()
    {
        return await _userManager.SelectAllSysApp();
    }

    [HttpGet]
    [Route("SelectByAppId/{key}")]
    public async virtual Task<ApiResponse<List<SysGroupModel.SysGroupSelectByAppidModel>>> SelectByAppId(
        [FromRoute] string key, int userId)
    {
        return await _userManager.SelectByAppId(key, userId);
    }

    [HttpGet]
    [Route("SelectByAppIdCustom/{key}")]
    public async virtual Task<ApiResponse<SysGroupModel.SysGroupSelectByAppidCusTomModel>> SelectByAppIdCustom(
        [FromRoute] string key, int userId)
    {
        return await _userManager.SelectByAppIdCustom(key, userId);
    }

    [HttpPost]
    [Route("UpdateUserMenuFunction")]
    public async virtual Task<ApiResponse> UpdateUserMenuFunction([FromBody] List<SysusermenuEntity> entity)
    {
        return await _userManager.UpdateUserMenuFunction(entity);
    }

    [HttpGet]
    [Route("GetDetails/{id}")]
    public async Task<ApiResponse<UserDetailModel>> GetDetails([FromRoute] long id)
    {
        return await _userManager.GetDetails(id);
    }

    [HttpGet]
    [Route("GetUserDetails/{id}")]
    public async Task<ApiResponse<DetailUserModel>> GetUserDetails([FromRoute] long id)
    {
        return await _userManager.GetUserDetails(id);
    }

    [HttpGet]
    [Route("GetInformationsForForm")]
    public async Task<ApiResponse<object>> GetInformationsForForm(long? id)
    {
        return await _userManager.GetInformationsForForm(id);
    }

    [HttpGet]
    [Route("SelectAllUser")]
    public async Task<ApiResponse<IEnumerable<UserSelect>>> SelectAllUser()
    {
        return await _userManager.SelectAllUser();
    }

    [HttpGet]
    [Route("GetByUserId/{userId}")]
    public async Task<ApiResponse<UserInfoModel>> GetByUserId([FromRoute] long userId)
    {
        return await _userManager.GetByUserId(userId);
    }


    [HttpGet]
    [Route("GetCanBoListByPhongBan/{phongBan}")]
    public async Task<ApiResponse<List<CanBoSelectModel>>> GetCanBoListByPhongBan([FromRoute] string phongBan)
    {
        return await _userManager.GetCanBoListByPhongBan(phongBan);
    }

    [HttpGet]
    [Route("GetCanBoCapDuoiList")]
    public async Task<ApiResponse<List<CanBoSelectModel>>> GetCanBoCapDuoiList()
    {
        return await _userManager.GetCanBoCapDuoiList();
    }

    [HttpGet]
    [Route("GetCanBoCapTrenList")]
    public async Task<ApiResponse<CanBoSelectAndUserLevelModel>> GetCanBoCapTrenList()
    {
        return await _userManager.GetCanBoCapTrenList();
    }


    //Add new if transfer to App Bac Giang
    [HttpGet]
    [Route("GetUserInUnit/{unitCode}")]
    public async Task<ApiResponse<List<UserInfoModel>>> GetUserInUnit([FromRoute] string unitCode)
    {
        return await _userManager.GetUserInUnit(unitCode);
    }

    [HttpPost]
    [Route("GetUserInfoByListId")]
    public async Task<ApiResponse<List<UserInfoModel>>> GetUserInfoByListId([FromBody] List<long> lstUserId)
    {
        return await _userManager.GetUserInfoByListId(lstUserId);
    }

    [HttpGet]
    [Route("GetUserHigherLevelSameUnitUserCurrent")]
    public async Task<ApiResponse<List<UserInfoModel>>> GetUserHigherLevelSameUnitUserCurrent()
    {
        return await _userManager.GetUserHigherLevelSameUnitUserCurrent();
    }

    [HttpPost]
    [Route("UploadFile")]
    [DisableRequestSizeLimit]
    public async Task<ApiResponse> UploadAvatar(IList<IFormFile> files, [FromForm] FileUserAvatarModel model)
    {
        return await _userManager.UploadAvatar(files, model);
    }

    //[HttpGet]
    //[Route("SelectUserGroup/{username}")]
    //public async Task<ApiResponse<long>> SelectUserGroup(string username)
    //{
    //    return await _userManager.SelectUserGroup(username);
    //}
}