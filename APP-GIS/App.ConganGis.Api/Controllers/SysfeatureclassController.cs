using System;
using App.Core.Common;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.AspNetCore.Mvc;
using Exceptionless;
using App.CongAnGis.Services.Manager;
using App.CongAnGis.Services.ManagerBase;

namespace App.CongAnGis.Api.Controllers
{
    [Route("App/gis/api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class SysfeatureclasController : SysfeatureclassControllerBase
    {

        private readonly ISysfeatureclassManager _manager;
        public SysfeatureclasController(IMemoryCache cache, ILogger<SysfeatureclassControllerBase> logger, ISysfeatureclassManager manager) : base(cache, logger)
        {
            _manager = manager;
        }

        [HttpPost]
        [Route("PostInsertSysfeatureclas")]
        override
        public async Task<ApiResponse> PostInsertSysfeatureclas([FromBody] SysfeatureclassVM.ItemSysfeatureclass _Model)
        {
            return await _manager.InsertCustom(_Model);
        }

        [HttpGet]
        [Route("GetAllFeatureclass")]
        public virtual async Task<ApiResponse<List<SysfeatureclassVM.ItemSysfeatureclass>>> GetAllFeatureclass()
        {
            return await _manager.SelectAllAsync();
        }

        [HttpDelete]
        [Route("DeleteSysfeatureclas/{_id}")]
        public virtual async Task<ApiResponse> DeleteSysfeatureclas(int _id)
        {
            return await _manager.DeleteMulti(_id);
        }

        [HttpDelete]
        [Route("DeleteData/{_id}")]
        public virtual async Task<ApiResponse> DeleteData(int _id)
        {
            return await _manager.DeleteData(_id);
        }

        [HttpPost]
        [Route("FeatureClassPagingCustom")]
        public virtual async Task<ApiResponse<PageModelView<IEnumerable<SysfeatureclassVM.ItemSysfeatureclass>>>> SelectPagingCustom([FromBody] SysfeatureclassVM.PageSysfeatureclass _model)
        {
            return await _manager.SelectPagingCustom(_model);
        }
    }
}


