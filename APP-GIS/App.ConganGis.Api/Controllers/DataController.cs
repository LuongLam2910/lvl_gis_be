using System;
using App.Core.Common;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.AspNetCore.Mvc;
using App.CongAnGis.Services.Manager;
using Microsoft.AspNetCore.Authorization;
using Exceptionless;
using App.CongAnGis.Services.Models;
using static SQLite.SQLite3;
using static App.CongAnGis.Services.Models.DataModel;
namespace App.CongAnGis.Api.Controllers
{
    [Route("App/gis/api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class DataController : Controller
    {

        private readonly IDataManager _manager;

        public DataController(IDataManager manager)
        {
            _manager = manager;
        }


        [HttpPost]
        [AllowAnonymous]
        [Route("getDataByTable")]
        public virtual async Task<ApiResponse<string>> getDataByTable(string tableName, string format, string filter)
        {
            return await _manager.getDataByTable(tableName, format, filter);
        }

        //[HttpPost]
        //[AllowAnonymous]
        //[Route("readFile")]
        //public virtual async Task<ApiResponse> insertCustom([FromBody] List<nhatroModel> model)
        //{
        //    return await _manager.insertCustom(model);
        //}

        [HttpGet]
        [AllowAnonymous]
        [Route("{tablename}/data")]
        public virtual async Task<ContentResult> GetData(string tablename, string format, string filter)
        {
            string result = await _manager.GetData(tablename, format, filter);
            return new ContentResult
            {
                Content = result,
                ContentType = "application/json",
            };
        }
        [HttpPost]
        [AllowAnonymous]
        [Route("pagingData")]
        public virtual async Task<ApiResponse<PageModelView<string>>> pagingData([FromBody] DataSearchModel model)
        {
            return await _manager.pagingData(model);
            
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("{tablename}/import")]
        public virtual async Task<ApiResponse> ImportData([FromBody] ImportDataModel model)
        {
            return await _manager.ImportData(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("{tablename}/ImportDataExcel")]
        public virtual async Task<ApiResponse> ImportDataExcel([FromBody] ImportDataModel model)
        {
            return await _manager.ImportDataExcel(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("countData")]
        public virtual async Task<ApiResponse<ThongKeModelResponse>> countData([FromBody] ThongKeModel model)
        {
            return await _manager.countData(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("countDataPCCC")]
        public virtual async Task<ApiResponse<ThongKeModelResponse>> countDataPCCC([FromBody] ThongKeModel model)
        {
            return await _manager.countDataPCCC(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("InsertDataCustom")]
        public virtual async Task<ApiResponse<int>> InsertDataCustom([FromBody] CreateDataModel model)
        {
            return await _manager.InsertDataCustom(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("countDataBieudo")]
        public virtual async Task<ApiResponse<ThongKeModelResponse>> countDataBieudo([FromBody] ThongKeModel model)
        {
            return await _manager.countDataBieudo(model);
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("selectOntShape")]
        public virtual async Task<ApiResponse<string>> selectOntShape(string tableName, int id)
        {
            return await _manager.selectOntShape(tableName, id);
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("ChangeStatusByTableName")]
        public virtual async Task<ApiResponse> ChangeStatusByTableName(string tablename, int status, int id)
        {
            return await _manager.ChangeStatusByTableName(tablename, status, id);
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("getIconByTable")]
        public virtual async Task<ApiResponse<IEnumerable<IconModel>>> getIconByTable(string tablename)
        {
            return await _manager.getIconByTable(tablename);
        }

        [HttpPost]
        [Route("UpdateDataCustom")]
        public virtual async Task<ApiResponse<int>> UpdateDataCustom([FromBody] CreateOrUpdateDataModel model)
        {
            return await _manager.UpdateDataCustom(model);
        }

        [HttpDelete]
        [Route("DeleteData/{tablename}/{key}")]
        public virtual async Task<ApiResponse> DeleteDataByKey(string tablename, string key)
        {
            return await _manager.DeleteData(tablename, key);
        }

        [HttpPost]
        [Route("export")]
        public virtual IActionResult ExportData([FromBody] ExportDataModel model)
        {
            string file = _manager.ExportData(model);
            if (string.IsNullOrEmpty(file)) return Json("No data");
            byte[] byteArray = System.IO.File.ReadAllBytes(file);
            return new FileContentResult(byteArray, "application/octet-stream");
        }

        [HttpPost]
        [Route("exportTemplate")]
        public virtual IActionResult exportTemplate([FromBody] ExportDataModel model)
        {
            string file = _manager.ExportTemplate(model);
            if (string.IsNullOrEmpty(file)) return Json("No data");
            byte[] byteArray = System.IO.File.ReadAllBytes(file);
            return new FileContentResult(byteArray, "application/octet-stream");
        }
    }
}


