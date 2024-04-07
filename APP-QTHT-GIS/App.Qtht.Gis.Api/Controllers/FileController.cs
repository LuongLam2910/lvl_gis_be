using App.Core.Common;
using App.QTHTGis.Services.Manager;
using App.Qtht.Services.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace App.QTHTGis.Api.Controllers
{
    //[EnableCors("AllowOrigin")]
    [ApiController]
    [Route("App/QthtGis/api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class FileController : Controller
    {
        private readonly IFileManager _manager;

        public FileController(IFileManager manager)
        {
            _manager = manager;
        }

        [HttpPost]
        [Route("GetPathFileById")]
        public async Task<ApiResponse<List<FileAttachment.ListFileModel>>> GetPathFileById([FromBody] int[] lstId)
        {
            return await _manager.GetPathFileById(lstId);
        }

        [HttpGet]
        [Route("GetPathFile/{id}")]
        public async Task<ApiResponse<List<FileAttachment.ListFileModel>>> GetPathFile(int id)
        {
            return await _manager.GetPathFile(id);
        }

        [HttpPost]
        [Route("UploadFile")]
        [DisableRequestSizeLimit]
        public async Task<ApiResponse<List<FileAttachment.ResponseModel>>> UploadFile(IList<IFormFile> files, [FromHeader] string folder)
        {
            return await _manager.UploadFile(files, folder);
        }   
        
        [HttpPost]
        [Route("UploadFileBase64")]
        [DisableRequestSizeLimit]
        public async Task<ApiResponse<List<FileAttachment.ResponseModel>>> UploadFileBase64([FromBody]FileAttachment.FileBase64RequestModel model)
        {
            return await _manager.UploadFileBase64(model);
        }

        [HttpPost]
        [Route("UploadFileBase64Multi")]
        [DisableRequestSizeLimit]
        public async Task<ApiResponse<List<FileAttachment.ResponseModel>>> UploadFileBase64Multi([FromBody] FileAttachment.FileBase64MultiRequestModel model)
        {
            return await _manager.UploadFileBase64Multi(model);
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
        [Route("DownloadListFile")]
        [ProducesResponseType(typeof(FileContentResult), 200)]
        public async Task<IActionResult> DownloadListFile([FromHeader] string strFileId)
        {
            FileStreamResult data = await _manager.DownloadListFile(strFileId);
            Response.Headers.Add("x-file-name", Base64Encode(data.FileDownloadName));
            Response.Headers.Add("Access-Control-Expose-Headers", "x-file-name");
            return data;
        }

        [HttpDelete]
        [Route("DeleteFile/{lstId}")]
        public async Task<ApiResponse> DeleteFile(string lstId)
        {
            return await _manager.DeleteFile(lstId);
        }

        private string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }
    }
}