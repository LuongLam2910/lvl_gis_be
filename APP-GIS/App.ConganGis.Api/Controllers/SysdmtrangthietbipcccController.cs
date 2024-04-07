using System;
using App.Core.Common;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.AspNetCore.Mvc;
using Exceptionless;
using App.CongAnGis.Services.Manager;

namespace App.CongAnGis.Api.Controllers
{
    [Route("App/gis/api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class SysdmtrangthietbipcccController : SysdmtrangthietbipcccControllerBase
    {
        private readonly SysdmtrangthietbipcccManager _manager = new SysdmtrangthietbipcccManager();
        public SysdmtrangthietbipcccController(IMemoryCache cache, ILogger<SysdmtrangthietbipcccControllerBase> logger) : base(cache, logger)
        {
        }
    }
}


