using App.Core.Common;
using App.QTHTGis.Dal.EntityClasses;
using App.QTHTGis.Dal.HelperClasses;
using App.QTHTGis.Services.Manager;
using App.Qtht.Services.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SD.LLBLGen.Pro.ORMSupportClasses;
using System.Collections.Generic;
using System.Threading.Tasks;
using static App.Qtht.Services.Models.SysUnitModel;

namespace App.QTHTGis.Api.Controllers;

[EnableCors("AllowOrigin")]
[Route("App/QthtGis/api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
[ApiController]
public class SysUnitController : DefaultController<SysunitEntity>
{
    private readonly ISysUnitManager _unitManager;

    public SysUnitController(ISysUnitManager unitManager)
    {
        _unitManager = unitManager;
    }

    [HttpPost]
    [Route("PagingCustom")]
    public async Task<ApiResponse<PageModelView<IEnumerable<SysUnitPagingModel>>>> PostPagingCustom([FromBody] PageModel pageModel)
    {
        return await _unitManager.Paging(pageModel, SetSortForPaging(), SetFilterPaging());
    }

    [HttpPost]
    [Route("GetListTenDonViByUnitcode")]
    public async Task<ApiResponse<EntityCollection<SysunitEntity>>> GetListTenDonVi(List<string> unitcodes)
    {
        return await _unitManager.GetListTenDonVi(unitcodes);
    }


    [HttpDelete]
    [Route("DeleteData/{unitcode}")]
    public async Task<ApiResponse> DeleteData(string unitcode)
    {
        return await _unitManager.Delete(unitcode);
    }

    [HttpDelete]
    [Route("DeleteByApp/{unitcode}/{appName}")]
    public async Task<ApiResponse> DeleteByApp(string unitcode, string appName)
    {
        return await _unitManager.DeleteByApp(unitcode, appName);
    }

    [HttpGet]
    [Route("GetSysUnitList")]
    public async Task<ApiResponse<IEnumerable<SysUnitModel.UnitSelectModel>>> GetSysUnitList()
    {
        return await _unitManager.GetSysUnitList();
    }

    [HttpGet]
    [Route("GetSysUnitListByDinhDanhApp")]
    public async Task<ApiResponse<IEnumerable<SysUnitModel.UnitSelectModel>>> GetSysUnitListByDinhDanhApp(
        string dinhDanhApp)
    {
        IPredicate[] conditions = {
            SysunitFields.Dinhdanhapp == dinhDanhApp
        };

        return await _unitManager.GetSysUnitList(conditions);
    }

    [HttpGet]
    [Route("GetUnitName/{unitcode}")]
    public async Task<ApiResponse<SysUnitModel.UnitSelectModel>> GetUnitName([FromRoute] string unitcode)
    {
        return await _unitManager.GetUnitName(unitcode);
    }

    [Route("SelectMasterInsert")]
    [HttpGet]
    public async Task<ApiResponse<SysUnitModel.UnitMasterInsert>> SelectMasterInsert()
    {
        return await _unitManager.SelectMasterInsert();
    }

    [HttpGet]
    [Route("SelectOneUnit/{unitcode}")]
    public async virtual Task<ApiResponse<SysunitEntity>> SelectOneUnit([FromRoute] string unitcode)
    {
        return await _unitManager.SelectOne(unitcode);
    }

    [HttpGet]
    [Route("GetAllCongAn")]
    public async Task<ApiResponse<List<CongAnList>>> GetAllCongAn()
    {
        return await _unitManager.GetAllCongAn();
    }

    protected override Predicate CheckDuplicateInsert(SysunitEntity entity)
    {
        return SysunitFields.Unitcode == entity.Unitcode;
    }

    protected override EntityField2 SetFieldKeyUpdateOrDelete()
    {
        return SysunitFields.Unitcode;
    }

    protected override Predicate CheckExistForUpdate(SysunitEntity entity)
    {
        return SysunitFields.Unitcode == entity.Unitcode;
    }

    protected override EntityField2[] SetFilterPaging()
    {
        return new[] {
            SysunitFields.Tendonvi
        };
    }

    protected override SortClause SetSortForPaging()
    {
        return SysunitFields.Ngaybanhanhqd | SortOperator.Descending;
    }
}