using App.Core.Common;
using App.QTHTGis.Dal.EntityClasses;
using App.QTHTGis.Dal.HelperClasses;
using App.QTHTGis.Services.Manager;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SD.LLBLGen.Pro.ORMSupportClasses;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace App.QTHTGis.Api.Controllers;

[EnableCors("AllowOrigin")]
[Authorize]
[Route("App/QthtGis/api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
[ApiController]
public class SysDvHanhChinhController : DefaultController<SysdvhanhchinhEntity>
{
    private readonly ISysDvHanhChinhManager _manager;

    public SysDvHanhChinhController(ISysDvHanhChinhManager manager)
    {
        _manager = manager;
    }

    protected override Func<SysdvhanhchinhEntity, object> SetSelectModelPaging()
    {
        // Có hoặc không
        return c => new {
            c.Id,
            c.MaDbhc,
            c.TenDbhc,
            c.MaDinhdanhtinh,
            c.CapDbhc,
            c.NgayHhl,
            c.NgayHl,
            c.MaDbhcCha
        };
    }

    protected override EntityField2[] SetFilterPaging()
    {
        return new[] {
            SysdvhanhchinhFields.TenDbhc
        };
    }

    protected override Predicate CheckExistForUpdate(SysdvhanhchinhEntity entity)
    {
        return SysdvhanhchinhFields.Id == entity.Id;
    }

    protected override EntityField2 SetFieldKeyUpdateOrDelete()
    {
        return SysdvhanhchinhFields.Id;
    }

    protected override SortClause SetSortForPaging()
    {
        return SysdvhanhchinhFields.Id | SortOperator.Ascending;
    }

    protected override EntityField2 SetColumnKeySelectOne()
    {
        return SysdvhanhchinhFields.Id;
    }

    protected override Predicate CheckDuplicateInsert(SysdvhanhchinhEntity entity)
    {
        return SysdvhanhchinhFields.Id == entity.Id;
    }

    [HttpGet]
    [Route("SelectMaAndTenDbhc")]
    public Task<ApiResponse<IEnumerable<object>>> SelectMaAndTenDbhc()
    {
        return _manager.SelectMaAndTenDbhc();
    }
}