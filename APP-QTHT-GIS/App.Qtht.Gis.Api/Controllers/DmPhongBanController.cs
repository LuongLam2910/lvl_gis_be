using App.Core.Common;
using App.QTHTGis.Services.Manager;
using App.Qtht.Services.Models;
using App.QTHTGis.Dal.EntityClasses;
using App.QTHTGis.Dal.HelperClasses;
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
public class DmPhongBanController : DefaultController<DmphongbanEntity>
{
    private readonly IDmPhongBanManager _manager;

    public DmPhongBanController(IDmPhongBanManager manager)
    {
        _manager = manager;
    }

    protected override Func<DmphongbanEntity, object> SetSelectModelPaging()
    {
        // Có hoặc không
        return c => new {
            c.Id,
            c.Ten,
            c.Trangthai
        };
    }

    protected override EntityField2[] SetFilterPaging()
    {
        return new[] { DmphongbanFields.Id, DmphongbanFields.Ten };
    }

    protected override Predicate CheckExistForUpdate(DmphongbanEntity entity)
    {
        return DmphongbanFields.Id == entity.Id;
    }

    protected override EntityField2 SetFieldKeyUpdateOrDelete()
    {
        return DmphongbanFields.Id;
    }

    protected override SortClause SetSortForPaging()
    {
        return DmphongbanFields.Id | SortOperator.Ascending;
    }

    protected override EntityField2 SetColumnKeySelectOne()
    {
        return DmphongbanFields.Id;
    }

    protected override Predicate CheckDuplicateInsert(DmphongbanEntity entity)
    {
        return DmphongbanFields.Id == entity.Id;
    }

    [HttpPost]
    [Route("PostPaging")]
    public Task<ApiResponse<PageModelView<IEnumerable<DmPhongBanModel>>>> PostPaging([FromBody] PageModel pageModel)
    {
        return _manager.PostPaging(pageModel);
    }

    [HttpGet]
    [Route("GetDetails")]
    public Task<ApiResponse<DmPhongBanModel>> GetDetails(int id)
    {
        return _manager.GetDetails(id);
    }

    [HttpGet]
    [Route("GetPhongBanListByUnitcode")]
    public Task<ApiResponse<IEnumerable<DmPhongBanModel.PhongBanSelectModel>>> GetPhongBanListByUnitcode(
        string unitcode)
    {
        return _manager.GetPhongBanListByUnitcode(unitcode);
    }
}