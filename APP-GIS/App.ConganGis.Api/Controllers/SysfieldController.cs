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
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Authorization;
using static App.CongAnGis.Services.Model.SysFieldModel;

namespace App.CongAnGis.Api.Controllers
{
    [Route("App/gis/api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class SysfieldController : SysfieldControllerBase
    {
        private readonly SysfieldManager _manager = new SysfieldManager();
        public SysfieldController(IMemoryCache cache, ILogger<SysfieldControllerBase> logger) : base(cache, logger)
        {
        }

        [HttpGet]
        [Route("GetAllSysfield/{_id}")]
        public virtual async Task<ApiResponse<List<SysfieldVM.ItemSysfield>>> GetAllSysfield(int _id)
        {
            return await _manager.SelectAllFieldAsync(_id);
        }

        [HttpPost]
        [Route("PostInsertSysfield")]
        public virtual async Task<ApiResponse> PostInsertSysfield([FromBody] SysfieldVM.ItemSysfield _Model)
        {
            return await _manager.InsertAsyncCustom(_Model);
        }

        [HttpDelete]
        [Route("PostDeleteSysfield/{_id}")]
        public virtual async Task<ApiResponse> PostDeleteSysfield(int _id)
        {
            return await _manager.DeleteAsyncCustom(_id);
        }

        [HttpGet]
        [Route("GetFieldsByTableName")]
        public Task<ApiResponse<IEnumerable<FieldModel>>> GetFieldsByTableName(string tablename)
        {
            return _manager.GetFieldsByTableName(tablename);
        }

        [HttpGet]
        [Route("GetFieldsForInsertOrUpdate")]
        public Task<ApiResponse<IEnumerable<FieldModel>>> GetFieldsForInsertOrUpdate(string tablename)
        {
            return _manager.GetFieldsForInsertOrUpdate(tablename);
        }
    }
}


