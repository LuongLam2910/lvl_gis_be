using App.Core.Common;
using App.QTHTGis.Dal.EntityClasses;
using App.QTHTGis.Dal.HelperClasses;
using App.QTHTGis.Services.Manager;
using App.Qtht.Services.Models;
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
public class DmChucVuController : DefaultController<DmchucvuEntity>
{
    private readonly IDmChucVuManager _manager;

    public DmChucVuController(IDmChucVuManager dmChucVuManager)
    {
        _manager = dmChucVuManager;
    }

    protected override Func<DmchucvuEntity, object> SetSelectModelPaging()
    {
        // Có hoặc không
        return c => new {
            c.Id,
            c.Ten,
            //c.Cap,
            //c.IdTochuc,
            c.Trangthai
        };
    }

    protected override EntityField2[] SetFilterPaging()
    {
        return new[] {
            DmchucvuFields.Id,
            DmchucvuFields.Ten
        };
    }

    protected override Predicate CheckExistForUpdate(DmchucvuEntity entity)
    {
        return DmchucvuFields.Id == entity.Id;
    }

    protected override EntityField2 SetFieldKeyUpdateOrDelete()
    {
        return DmchucvuFields.Id;
    }

    protected override SortClause SetSortForPaging()
    {
        return DmchucvuFields.Id | SortOperator.Ascending;
    }

    protected override EntityField2 SetColumnKeySelectOne()
    {
        return DmchucvuFields.Id;
    }

    protected override Predicate CheckDuplicateInsert(DmchucvuEntity entity)
    {
        return DmchucvuFields.Id == entity.Id;
    }

    [HttpPost]
    [Route("PostPaging")]
    public Task<ApiResponse<PageModelView<IEnumerable<DmChucVuModel>>>> PostPaging([FromBody] PageModel pageModel)
    {
        return _manager.PostPaging(pageModel);
    }

    [HttpGet]
    [Route("GetDetails")]
    public Task<ApiResponse<DmChucVuModel>> GetDetails(int id)
    {
        return _manager.GetDetails(id);
    }

    [HttpGet]
    [Route("GetChucVuListByUnitcode")]
    [AllowAnonymous]
    public Task<ApiResponse<IEnumerable<DmChucVuModel.ChucVuSelectModel>>> GetChucVuListByUnitcode(string unitcode)
    {
        return _manager.GetChucVuListByUnitcode(unitcode);
    }
}