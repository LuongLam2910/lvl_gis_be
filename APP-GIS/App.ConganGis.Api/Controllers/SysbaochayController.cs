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
using App.CongAnGis.Services.Models;
using static App.CongAnGis.Services.Models.SysBaoChayModel;
using Microsoft.AspNetCore.SignalR;
using App.CongAnGis.Services.Hubs;
using DocumentFormat.OpenXml.Office2010.Excel;
using Microsoft.AspNet.SignalR.Messaging;
using static Microsoft.ApplicationInsights.MetricDimensionNames.TelemetryContext;
using Microsoft.AspNetCore.Authorization;

namespace App.CongAnGis.Api.Controllers
{
    [Route("App/gis/api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class SysbaochayController : SysbaochayControllerBase
    {
        private readonly ISysbaochayManager _manager;
        public SysbaochayController(IMemoryCache cache, ILogger<SysbaochayControllerBase> logger, IHubContext<SignalR> hubcontext, ISysbaochayManager manager) : base(cache, logger)
        {
            HubContext = hubcontext;
            _manager = manager;
        }

        private IHubContext<SignalR> HubContext { get; set; }


        [HttpPost]
        [Authorize]
        [Route("paging")]
        public virtual async Task<ApiResponse<PageModelView<IEnumerable<SysbaochayVM.ItemSysbaochay>>>> Paging([FromBody] SysbaochayModel _model)
        {
            return await _manager.Paging(_model);
        }

        [HttpPost]
        [Route("getAllBaoChay")]
        public virtual async Task<ApiResponse<IEnumerable<BaoChayModel>>> getAllBaoChay()
        {
            return await _manager.getAllBaoChay();
        }

        [HttpPost]
        [Route("getLngLat")]
        public virtual string getLngLat(string Address)
        {
            return _manager.getLngLat(Address);
        }

        [HttpPost]
        [Route("InsertCustomAsync")]
        public virtual async Task<ApiResponse> InsertCustomAsync([FromBody] BaoChayModel _Model)
        {
            return  await _manager.InsertCustomAsync(_Model, HubContext);
        }

    }
}


