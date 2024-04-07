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
    public class SysmapUserController : SysmapUserControllerBase
    {
        private readonly SysmapUserManager _manager = new SysmapUserManager();
        public SysmapUserController(IMemoryCache cache, ILogger<SysmapUserControllerBase> logger) : base(cache, logger)
        {
        }
    }
}


