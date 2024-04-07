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
using static App.CongAnGis.Services.Model.SysFileModel;
using Microsoft.AspNetCore.Http;

namespace App.CongAnGis.Api.Controllers
{
    [Route("App/gis/api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class SysfileController : SysfileControllerBase
    {
        private readonly ISysfileManager _manager;
        public SysfileController(ISysfileManager manager, IMemoryCache cache, ILogger<SysfileControllerBase> logger) : base(cache, logger)
        {
            _manager = manager;
        }

        [HttpPost]
        [Route("PagingSysfileByFolderId")]
        public virtual async Task<ApiResponse<PageModelView<IEnumerable<SysfileVM.ItemSysfile>>>> PostPagingSysfile([FromBody] PageSysfileModel _model)
        {
            return await _manager.PagingFileByFolderId(_model);
        }

        [HttpGet]
        [Route("GetColumns")]
        public async Task<ApiResponse<IEnumerable<string>>> GetColumns(int fileId)
        {
            return await _manager.GetColumns(fileId);
        }

        [HttpPost]
        [Route("UploadFile")]
        [DisableRequestSizeLimit]

        public async Task<ApiResponse<List<SysFileResponseModel>>> UploadSysFile(IList<IFormFile> files,[FromHeader] string folder, [FromHeader] int folderId)
        {
            return await _manager.UploadSysFile(files, folder , folderId);
        }
    }
}


