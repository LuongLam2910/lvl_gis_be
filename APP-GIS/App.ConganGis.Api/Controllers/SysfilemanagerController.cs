using System;
using App.Core.Common;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.AspNetCore.Mvc;
using Exceptionless;
using App.CongAnGis.Services.Manager;
using Microsoft.AspNetCore.Http;
using static App.CongAnGis.Services.Models.FileAttachment;
using DocumentFormat.OpenXml.EMMA;
using Microsoft.AspNetCore.Authorization;
using static App.CongAnGis.Services.Model.SysFileModel;

namespace App.CongAnGis.Api.Controllers
{
    [Route("App/gis/api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class SysfilemanagerController : SysfilemanagerControllerBase
    {
        private readonly ISysfilemanagerManager _manager;
        public SysfilemanagerController(IMemoryCache cache, ILogger<SysfilemanagerControllerBase> logger, ISysfilemanagerManager manager) : base(cache, logger)
        {
            _manager = manager;
        }

        [HttpPost]
        [Route("UploadFile")]
        [DisableRequestSizeLimit]

        public async Task<ApiResponse> UploadFile(IList<IFormFile> files, [FromForm]FileManagerModel model)
        {
            return await _manager.UploadFile(files, model);
        }

        [HttpGet]
        [Route("GetFileByIdBase64")]
        public Task<ApiResponse<SysFileManagerModel>> GetFileByIdBase64(string tableName, int idFeatureData)
        {
            return _manager.GetFileByIdBase64(tableName, idFeatureData);
        }
        [HttpGet]
        [Route("DownloadFile")]
        [ProducesResponseType(typeof(FileContentResult), 200)]
        public async Task<IActionResult> Download([FromHeader] int fileId)
        {
            FileStreamResult data = await _manager.Download(fileId);
            Response.Headers.Add("x-file-name", Base64Encode(data.FileDownloadName));
            Response.Headers.Add("Access-Control-Expose-Headers", "x-file-name");
            return data;
        }
        [HttpGet]
        [Route("GetFileById")]
        public Task<ApiResponse<SysFileManagerModel>> GetFileById(string tableName, int idFeatureData)
        {
            return _manager.GetFileById(tableName, idFeatureData);
        }

        [HttpDelete]
        [Route("DeleteSysFileManager")]
        public Task<ApiResponse> DeleteSysFileManager(string fileName, int idData)
        {
            return _manager.DeleteSysFileManager(fileName, idData);
        }
        private string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

    }
}


