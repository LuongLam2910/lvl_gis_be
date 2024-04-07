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
using App.ConganGis.Services.ManagerBase;
using App.ConganGis.Services.Model;
using static App.ConganGis.Services.Model.SysBaoTaiNanModel;

namespace App.CongAnGis.Api.Controllers
{
    [Route("App/gis/api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class SysbaotainanController : SysbaotainanControllerBase
    {
        private readonly ISysbaotainanManager _manager;
        public SysbaotainanController(IMemoryCache cache, ILogger<SysbaotainanControllerBase> logger, IHubContext<SignalR> hubcontext, ISysbaotainanManager manager) : base(cache, logger)
        {
            HubContext = hubcontext;
            _manager = manager;
        }

        private IHubContext<SignalR> HubContext { get; set; }


        [HttpPost]
        [Route("paging")]
        public virtual async Task<ApiResponse<PageModelView<IEnumerable<SysbaotainanVM.ItemSysbaotainan>>>> Paging([FromBody] SysbaotainanModel _model)
        {
            return await _manager.Paging(_model);
        }

        [HttpPost]
        [Route("getAllBaoTaiNan")]
        public virtual async Task<ApiResponse<IEnumerable<BaoTaiNanModel>>> getAllBaoTaiNan()
        {
            return await _manager.getAllBaoTaiNan();
        }

        [HttpPost]
        [Route("getLngLat")]
        public virtual string getLngLat(string Address)
        {
            return _manager.GetLngLat(Address);
        }

        [HttpPost]
        [Route("InsertCustomAsync")]
        public virtual async Task<ApiResponse> InsertCustomAsync([FromBody] BaoTaiNanModel _Model)
        {
            return await _manager.InsertCustomAsync(_Model, HubContext);
        }

    }
}


