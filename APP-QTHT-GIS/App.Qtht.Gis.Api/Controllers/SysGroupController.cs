using App.Core.Common;
using App.Core.Common.RoleConstants;
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

namespace App.QTHTGis.Api.Controllers;

[Authorize]
[Route("App/QthtGis/api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
[ApiController]
public class SysGroupController : DefaultController<SysgroupEntity>
{
    private readonly ISysGroupManager _groupManager;

    public SysGroupController(ISysGroupManager appManager)
    {
        _groupManager = appManager;
    }

    [HttpGet]
    [Route("SelectAll")]
    //[AllowAnonymous]
    [AuthorizeRole(RoleConstants.Dmht.BacLuong)]
    [AuthorizeRole(RoleConstants.Role.View)]
    public override Task<ApiResponse<IEnumerable<object>>> SelectAll()
    {
        return base.SelectAll();
    }


    //Map object select page sang Model
    protected override Func<SysgroupEntity, object> SetSelectModelPaging()
    {
        // Có hoặc không
        return c => new {
            c.Appid,
            c.Description,
            c.Groupid,
            c.Groupname,
            c.Status,
            c.Unitcode
        };
    }

    //Map object cho select ALL nếu không map mặc định select all properties
    protected override Func<SysgroupEntity, object> SetModelSelectAll()
    {
        return c => new {
            c.Appid,
            c.Description,
            c.Groupid,
            c.Groupname,
            c.Status,
            c.Unitcode
        };
    }

    protected override EntityField2[] SetFilterPaging()
    {
        // Có hoặc không. Dùng khi thực hiện Search trong page => khai báo các column Searh
        return new[] { SysgroupFields.Groupid, SysgroupFields.Groupname };
    }

    // Có hoặc không. Nếu khi filter có điều kiện đặc biệt cần filter list page theo điều kiện nào đó thì sử dụng
    protected override EntityField2 SetFieldConditionPaging()
    {
        // Nếu không lọc theo điều kiện mặc định thì không sử dụng
        return SysgroupFields.Groupid;
    }

    //Set key khi sử dụng API SelectOne. key sẽ được áp dụng vào column nào
    protected override EntityField2 SetColumnKeySelectOne()
    {
        // Bắt buộc có khi sử dụng SelectOne
        return SysgroupFields.Groupid;
    }

    // Khi sử dụng SelectAll có sort hay không ? nếu có thì ghi đè lại phương thức này
    protected override SortClause SetSortSelectAll()
    {
        return SysgroupFields.Groupid | SortOperator.Ascending;
    }

    // Khi sử dụng Update/Delete bắt buộc khai báo, khai báo column key dùng để update hoặc delete
    protected override EntityField2 SetFieldKeyUpdateOrDelete()
    {
        return SysgroupFields.Groupid;
    }

    // Có thể sử dụng hoặc không. nếu trong trường hợp chỉ cho phép update trường nào đó thì làm mới Entity từ API Update
    protected override SysgroupEntity SetValueEntityUpdate(SysgroupEntity source)
    {
        SysgroupEntity entity = new();
        entity.Groupid = source.Groupid;
        entity.Groupname = source.Groupname;
        entity.Description = source.Description;
        entity.Status = source.Status;
        entity.Unitcode = source.Unitcode;
        entity.Appid = source.Appid;
        entity.IsNew = false;
        return entity;
    }

    [HttpGet]
    [Route("SelectGroupApp")]
    public async Task<ApiResponse<SysGroupModel.GetDataAppViewModel>> SelectGroupApp()
    {
        return await _groupManager.SelectGroupApp();
    }

    [HttpGet]
    [Route("SelectGroupByAppId/{appId}/{level}")]
    public async Task<ApiResponse<List<SysGroupModel.GroupInfoViewModel>>> SelectGroupByAppId(int appId, int? level)
    {
        return await _groupManager.SelectGroupByAppId(appId, level);
    }

    [HttpGet]
    [Route("SelectByGroupIdAppId/{key}")]
    public async virtual Task<ApiResponse<List<SysGroupModel.SysGroupSelectByAppidModel>>> SelectByGroupIdAppId(
        [FromRoute] int key)
    {
        return await _groupManager.SelectByGroupIdAppId(key);
    }

    [HttpPost]
    [Route("UpdateGroupMenuFunction")]
    public async virtual Task<ApiResponse> UpdateGroupMenuFunction([FromBody] List<SysgroupmenuEntity> entity)
    {
        return await _groupManager.UpdateGroupMenuFunction(entity);
    }

    [HttpDelete]
    [Route("Delete/{key}")]
    public override async Task<ApiResponse> PostDelete([FromRoute] int key)
    {
        return await _groupManager.DeleteSysGroup(key);
    }
}