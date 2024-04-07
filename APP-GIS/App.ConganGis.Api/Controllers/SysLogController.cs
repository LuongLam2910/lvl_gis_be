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
using DocumentFormat.OpenXml.ExtendedProperties;
using static App.CongAnGis.Services.Manager.SysLogsManager;
using static App.CongAnGis.Services.Model.SysLogModel;

namespace App.CongAnGis.Api.Controllers
{
    [Route("App/gis/api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class SysLogsController : SysimportLogsControllerBase
    {

        private readonly ISysLogsManager _manager;
        public SysLogsController(IMemoryCache cache, ILogger<SysimportLogsControllerBase> logger, ISysLogsManager manager) : base(cache, logger)
        {
            _manager = manager;
        }

        [HttpPost]
        [Route("PostInsertLog")]
        public async Task<ApiResponse> PostInsertLog([FromBody] LogModel _Model)
        {
            return await _manager.insertLog(_Model);
        }
    }
}


