using App.Core.Common;
using App.QTHTGis.Dal.EntityClasses;
using App.QTHTGis.Dal.HelperClasses;
using App.Qtht.Services.Manager;
using App.Qtht.Services.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SD.LLBLGen.Pro.ORMSupportClasses;
using System;
using System.Threading.Tasks;
using App.QTHTGis.Services.Manager;

namespace App.QTHTGis.Api.Controllers;

[Route("App/QthtGis/api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
[ApiController]
public class DangkychiaseController : DefaultController<DangkychiaseEntity>
{
    private readonly IDangkychiaseManager _manager;
    private readonly ISysUserManager _userManager;

    public DangkychiaseController(IDangkychiaseManager manager, ISysUserManager userManager)
    {
        _manager = manager;
        _userManager = userManager;
    }

    [HttpPost]
    [Route("Insertdata")]
    public async virtual Task<ApiResponse> Insertdata([FromBody] DangkychiaseEntity entity)
    {
        return await _manager.Insert(entity);
    }

    [Route("InsertUserClient")]
    [HttpPost]
    [AllowAnonymous]
    public async virtual Task<ApiResponse> InsertUserClient([FromBody] SysuserEntity model)
    {
        return await _userManager.InsertUserClient(model);
    }

    [Route("InsertUser")]
    [HttpPost]
    [AllowAnonymous]
    public async Task<ApiResponse> InsertUser([FromBody] SysUserModel.InsertUpdateModel model)

    {
        if (ModelState.IsValid)
        {
            return await _userManager.InsertUserAndUserGroup(model);
        }

        return null;
    }

    [Route("RegisterUserClientMobile")]
    [HttpPost]
    [AllowAnonymous]
    public async Task<ApiResponse<SysuserEntity>> RegisterUserClientMobile(
        [FromBody] SysUserModel.InsertUpdateModel model)

    {
        if (ModelState.IsValid)
        {
            return await _userManager.RegisterUserClientMobile(model);
        }

        return null;
    }

    protected override Func<DangkychiaseEntity, object> SetSelectModelPaging()
    {
        // Có hoặc không
        return c => new {
            c.Id,
            c.Dieukhoan,
            c.Donvicongtac,
            c.Email,
            c.Hoten,
            c.Motadulieu,
            c.Sodt,
            c.Tendulieu,
            c.Ngaydangky,
            c.Status
        };
    }

    //Map object cho select ALL nếu không map mặc định select all properties
    //protected override Func<DgdtDanhgiabandauEntity, object> SetModelSelectAll()
    //{
    //    return null;
    //}

    protected override EntityField2[] SetFilterPaging()
    {
        // Có hoặc không. Dùng khi thực hiện Search trong page => khai báo các column Searh
        return new[] {
            DangkychiaseFields.Dieukhoan, DangkychiaseFields.Donvicongtac, DangkychiaseFields.Email,
            DangkychiaseFields.Hoten, DangkychiaseFields.Motadulieu, DangkychiaseFields.Sodt,
            DangkychiaseFields.Tendulieu
        };
    }

    // Có hoặc không. Nếu khi filter có điều kiện đặc biệt cần filter list page theo điều kiện nào đó thì sử dụng
    protected override EntityField2 SetFieldConditionPaging()
    {
        // Nếu không lọc theo điều kiện mặc định thì không sử dụng
        return DangkychiaseFields.Id;
    }

    //Set key khi sử dụng API SelectOne. key sẽ được áp dụng vào column nào
    protected override EntityField2 SetColumnKeySelectOne()
    {
        // Bắt buộc có khi sử dụng SelectOne
        return DangkychiaseFields.Id;
    }

    // Khi sử dụng SelectAll có sort hay không ? nếu có thì ghi đè lại phương thức này
    protected override SortClause SetSortSelectAll()
    {
        return DangkychiaseFields.Id | SortOperator.Ascending;
    }

    // Khi sử dụng Update/Delete bắt buộc khai báo, khai báo column key dùng để update hoặc delete
    protected override EntityField2 SetFieldKeyUpdateOrDelete()
    {
        return DangkychiaseFields.Id;
    }
}