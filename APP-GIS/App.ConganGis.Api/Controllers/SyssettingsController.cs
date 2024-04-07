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
    public class SyssettingsController : SyssettingsControllerBase
    {
        private readonly SyssettingsManagerBase _manager = new SyssettingsManagerBase();
        public SyssettingsController(IMemoryCache cache, ILogger<SyssettingsControllerBase> logger) : base(cache, logger)
        {
        }
    }
}


