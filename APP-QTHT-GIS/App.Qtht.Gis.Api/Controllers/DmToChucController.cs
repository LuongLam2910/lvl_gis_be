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
public class DmToChucController : DefaultController<DmtochucEntity>
{
    private readonly IDmToChucManager _manager;

    public DmToChucController(IDmToChucManager dmToChucManager)
    {
        _manager = dmToChucManager;
    }

    protected override Func<DmtochucEntity, object> SetSelectModelPaging()
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
        return new[] { DmtochucFields.Ten };
    }

    protected override Predicate CheckExistForUpdate(DmtochucEntity entity)
    {
        return DmtochucFields.Id == entity.Id;
    }

    protected override EntityField2 SetFieldKeyUpdateOrDelete()
    {
        return DmtochucFields.Id;
    }

    protected override SortClause SetSortForPaging()
    {
        return DmtochucFields.Id | SortOperator.Ascending;
    }

    protected override EntityField2 SetColumnKeySelectOne()
    {
        return DmtochucFields.Id;
    }

    protected override Predicate CheckDuplicateInsert(DmtochucEntity entity)
    {
        return DmtochucFields.Id == entity.Id;
    }

    [HttpGet]
    [Route("GetToChucListByStatus")]
    public Task<ApiResponse<IEnumerable<DmToChucModel>>> GetToChucListByStatus(bool activeStatus)
    {
        return _manager.GetToChucListByStatus(activeStatus);
    }

    [HttpGet]
    [Route("GetToChucDangHoatDongList")]
    public Task<ApiResponse<IEnumerable<DmToChucModel>>> GetToChucDangHoatDongList()
    {
        return _manager.GetToChucListByStatus(true);
    }

    [HttpGet]
    [Route("GetLstChucVu")]
    public Task<ApiResponse<IEnumerable<DmChucVuDetailModel>>> GetLstChucVu()
    {
        return _manager.GetLstChucVu();
    }

    [HttpPost]
    [Route("InsertCustom")]
    public async virtual Task<ApiResponse> InsertCustom([FromBody] DmToChucInsertUpdateModel model)
    {
        return await _manager.InsertCustom(model);
    }

    [HttpGet]
    [Route("SelecOneCustom/{key}")]
    public async virtual Task<ApiResponse<DmToChucInsertUpdateModel>> SelecOneCustom([FromRoute] int key)
    {
        return await _manager.SelecOneCustom(key);
    }

    [HttpPut]
    [Route("UpdateCustom")]
    public async virtual Task<ApiResponse> UpdateCustom([FromBody] DmToChucInsertUpdateModel model)
    {
        return await _manager.UpdateCustom(model);
    }

    [HttpDelete]
    [Route("DeleteCustom/{key}")]
    public async virtual Task<ApiResponse> DeleteCustom([FromRoute] int key)
    {
        return await _manager.DeleteCustom(key);
    }
}