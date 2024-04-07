using App.Core.Common;
using App.QTHTGis.Dal.EntityClasses;
using App.QTHTGis.Dal.HelperClasses;
using App.QTHTGis.Services.Manager;
using App.Qtht.Services.Models;
using Microsoft.AspNetCore.Mvc;
using SD.LLBLGen.Pro.ORMSupportClasses;
using System;
using System.Threading.Tasks;

namespace App.QTHTGis.Api.Controllers;

//[Authorize]
[Route("App/QthtGis/api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
[ApiController]
public class SysMenuController : DefaultController<SysmenuEntity>
{
    private readonly ISysMenuManager _menuManager;

    public SysMenuController(ISysMenuManager menuManager)
    {
        _menuManager = menuManager;
    }

    protected override Func<SysmenuEntity, object> SetSelectModelPaging()
    {
        return c => new {
            c.Appid,
            c.Cssclass,
            c.Function,
            c.Iconclass,
            c.Macha,
            c.Machucnang,
            c.Menuid,
            c.Sothutu,
            c.State,
            c.Tenchucnang,
            c.Trangthai,
            c.Unitcode
        };
    }

    //Map object cho select ALL nếu không map mặc định select all properties
    protected override Func<SysmenuEntity, object> SetModelSelectAll()
    {
        return c => new {
            c.Appid,
            c.Cssclass,
            c.Function,
            c.Iconclass,
            c.Macha,
            c.Machucnang,
            c.Menuid,
            c.Sothutu,
            c.State,
            c.Tenchucnang,
            c.Trangthai,
            c.Unitcode
        };
    }

    protected override EntityField2[] SetFilterPaging()
    {
        // Có hoặc không. Dùng khi thực hiện Search trong page => khai báo các column Searh
        return new[] {
            SysmenuFields.Appid, SysmenuFields.Machucnang, SysmenuFields.Tenchucnang, SysmenuFields.Macha,
            SysmenuFields.State
        };
    }

    // Có hoặc không. Nếu khi filter có điều kiện đặc biệt cần filter list page theo điều kiện nào đó thì sử dụng
    protected override EntityField2 SetFieldConditionPaging()
    {
        // Nếu không lọc theo điều kiện mặc định thì không sử dụng
        return SysmenuFields.Menuid;
    }

    //Set key khi sử dụng API SelectOne. key sẽ được áp dụng vào column nào
    protected override EntityField2 SetColumnKeySelectOne()
    {
        // Bắt buộc có khi sử dụng SelectOne
        return SysmenuFields.Menuid;
    }

    // Khi sử dụng SelectAll có sort hay không ? nếu có thì ghi đè lại phương thức này
    protected override SortClause SetSortSelectAll()
    {
        return SysmenuFields.Menuid | SortOperator.Descending;
    }

    // Khi sử dụng Update/Delete bắt buộc khai báo, khai báo column key dùng để update hoặc delete
    protected override EntityField2 SetFieldKeyUpdateOrDelete()
    {
        return SysmenuFields.Menuid;
    }

    // Có thể sử dụng hoặc không. nếu trong trường hợp chỉ cho phép update trường nào đó thì làm mới Entity từ API Update
    protected override SysmenuEntity SetValueEntityUpdate(SysmenuEntity source)
    {
        SysmenuEntity entity = new();
        entity.Appid = source.Appid;
        entity.Cssclass = source.Cssclass;
        entity.Function = source.Function;
        entity.Iconclass = source.Iconclass;
        entity.Macha = source.Macha;
        entity.Trangthai = source.Trangthai;
        entity.Machucnang = source.Machucnang;
        entity.Menuid = source.Menuid;
        entity.Sothutu = source.Sothutu;
        entity.State = source.State;
        entity.Tenchucnang = source.Tenchucnang;
        entity.IsNew = false;
        return entity;
    }

    [Route("GetItemAddNewEdit")]
    [HttpGet]
    public async Task<ApiResponse<SysMenuModel.ItemAddNew>> GetItemAddNewEdit()
    {
        return await _menuManager.GetItemAddNewEdit();
    }
}