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

namespace App.QTHTGis.Api.Controllers;

[Authorize]
[Route("App/QthtGis/api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
[ApiController]
public class
    SysAppController : DefaultController<SysappEntity> // string ở đây là type Key dùng để select , update, delete
{
    private readonly ISysAppManager _appManager;

    public SysAppController(ISysAppManager appManager)
    {
        _appManager = appManager;
    }

    [HttpGet]
    [Route("SelectbyStatus")]
    [AllowAnonymous]
    public async Task<ApiResponse<EntityCollection<SysappEntity>>> SelectbyStatus()
    {
        return await _appManager.SelectbyStatus();
    }

    [HttpGet]
    [Route("SelectAll")]
    [AllowAnonymous]
    public override Task<ApiResponse<IEnumerable<object>>> SelectAll()
    {
        return base.SelectAll();
    }

    [HttpGet]
    [Route("SelectCheckExistWithUserId/{userId}")]
    public async Task<ApiResponse<List<SysAppModel.AppAndCheckUserInAppModel>>> SelectCheckExistWithUserId(int userId)
    {
        return await _appManager.SelectCheckExistWithUserId(userId);
    }


    //Map object select page sang Model
    protected override Func<SysappEntity, object> SetSelectModelPaging()
    {
        // Có hoặc không
        return c => new {
            c.Appid,
            c.Tenapp,
            c.Mota,
            c.Trangthai
        };
    }

    //Map object cho select ALL nếu không map mặc định select all properties
    protected override Func<SysappEntity, object> SetModelSelectAll()
    {
        return c => new { c.Tenapp, c.Mota, c.State, c.Dinhdanhapp, c.Appid };
    }

    protected override EntityField2[] SetFilterPaging()
    {
        // Có hoặc không. Dùng khi thực hiện Search trong page => khai báo các column Searh
        return new[] { SysappFields.Appid, SysappFields.Tenapp, SysappFields.Mota };
    }

    // Có hoặc không. Dùng để thêm điều kiện sort mặc định khi lấy dữ liệu page
    //protected override SortClause SetSortForPaging()
    //{
    //    // Không bắt buộc
    //    return DmdantocFields.Madantoc | SortOperator.Descending;
    //}

    // Có hoặc không. Nếu khi filter có điều kiện đặc biệt cần filter list page theo điều kiện nào đó thì sử dụng
    protected override EntityField2 SetFieldConditionPaging()
    {
        // Nếu không lọc theo điều kiện mặc định thì không sử dụng
        return SysappFields.Appid;
    }

    //Set key khi sử dụng API SelectOne. key sẽ được áp dụng vào column nào
    protected override EntityField2 SetColumnKeySelectOne()
    {
        // Bắt buộc có khi sử dụng SelectOne
        return SysappFields.Appid;
    }

    // Khi sử dụng SelectAll có sort hay không ? nếu có thì ghi đè lại phương thức này
    protected override SortClause SetSortSelectAll()
    {
        return SysappFields.Appid | SortOperator.Ascending;
    }

    // Khi sử dụng Update/Delete bắt buộc khai báo, khai báo column key dùng để update hoặc delete
    protected override EntityField2 SetFieldKeyUpdateOrDelete()
    {
        return SysappFields.Appid;
    }

    // Có thể sử dụng hoặc không. nếu trong trường hợp chỉ cho phép update trường nào đó thì làm mới Entity từ API Update
    protected override SysappEntity SetValueEntityUpdate(SysappEntity source)
    {
        SysappEntity entity = new();
        entity.Appid = source.Appid;
        entity.Tenapp = source.Tenapp;
        entity.Mota = source.Mota;
        entity.Trangthai = source.Trangthai;
        entity.IsNew = false;
        return entity;
    }
}