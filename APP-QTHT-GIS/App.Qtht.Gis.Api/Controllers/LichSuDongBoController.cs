using App.Core.Common;
using App.QTHTGis.Dal.EntityClasses;
using App.QTHTGis.Dal.HelperClasses;
using App.QTHTGis.Services.Manager;
using App.Qtht.Services.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SD.LLBLGen.Pro.ORMSupportClasses;
using System;
using System.Threading.Tasks;

namespace App.QTHTGis.Api.Controllers;

[Authorize]
[Route("App/QthtGis/api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
[ApiController]
public class LichSuDongBoController : DefaultController<LogdagEntity>
{
    private readonly ILichSuDongBoManager _manager;

    public LichSuDongBoController(ILichSuDongBoManager LogdagManager)
    {
        _manager = LogdagManager;
    }

    protected override Func<LogdagEntity, object> SetSelectModelPaging()
    {
        // Có hoặc không
        return c => new {
            c.Dagid,
            c.Ngaychay,
            c.Ngayketthuc,
            c.Tongbanghinguon,
            c.Banghihientai,
            c.Id,
            c.Inserted,
            c.Updated,
            c.Tinhtrang,
            c.Pagenumber,
            c.Pagesize,
            c.Trung,
            c.Nguon,
            c.Appid
        };
    }

    protected override EntityField2[] SetFilterPaging()
    {
        return new[] {
            LogdagFields.Dagid,
            LogdagFields.Ngaychay,
            LogdagFields.Ngayketthuc,
            LogdagFields.Tongbanghinguon,
            LogdagFields.Banghihientai,
            LogdagFields.Id,
            LogdagFields.Inserted,
            LogdagFields.Updated,
            LogdagFields.Tinhtrang,
            LogdagFields.Pagenumber,
            LogdagFields.Pagesize,
            LogdagFields.Trung,
            LogdagFields.Nguon,
            LogdagFields.Appid
        };
    }

    protected override Predicate CheckExistForUpdate(LogdagEntity entity)
    {
        return LogdagFields.Id == entity.Id;
    }

    protected override EntityField2 SetFieldKeyUpdateOrDelete()
    {
        return LogdagFields.Id;
    }

    protected override SortClause SetSortForPaging()
    {
        return LogdagFields.Id | SortOperator.Descending;
    }

    protected override EntityField2 SetColumnKeySelectOne()
    {
        return LogdagFields.Id;
    }

    protected override Predicate CheckDuplicateInsert(LogdagEntity entity)
    {
        return LogdagFields.Id == entity.Id;
    }

    protected override EntityField2 SetFieldConditionPaging()
    {
        return LogdagFields.Appid;
    }

    [HttpPost]
    [Route("ReloadDag")]
    public async Task<ApiResponse> ReloadDag([FromBody] LichSuDongBoModel dag)
    {
        return await _manager.ReloadDag(dag);
    }
}