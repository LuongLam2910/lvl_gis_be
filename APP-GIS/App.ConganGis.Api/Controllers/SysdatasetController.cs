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
using App.CongAnGis.Dal.EntityClasses;
using static App.CongAnGis.Services.Models.DataModel;

namespace App.CongAnGis.Api.Controllers
{
    [Route("App/gis/api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class SysdatasetController : SysdatasetControllerBase
    {
        private readonly ISysdatasetManager _manager;
        private readonly SysdatasetManagerBase _managerBase = new SysdatasetManagerBase();
        public SysdatasetController(IMemoryCache cache, ILogger<SysdatasetControllerBase> logger, ISysdatasetManager manager) : base(cache, logger)
        {
            _manager = manager;
        }

        [HttpPost]
        [Route("exportDataSet")]
        public virtual IActionResult exportTemplate([FromBody] ExportDataModel model)
        {
            string file = _manager.ExportTemplate(model);
            if (string.IsNullOrEmpty(file)) return null;
            byte[] byteArray = System.IO.File.ReadAllBytes(file);
            return new FileContentResult(byteArray, "application/octet-stream");
        }

        [HttpGet]
        [Route("GetAllSysdataset")]
        public virtual async Task<ApiResponse<List<SysdatasetVM.ItemSysdataset>>> GetAllSysDataset()
        {
            return await _managerBase.SelectAllAsync();
        }

        [HttpPost]
        [Route("InsertAsyncCusTom")]
        public virtual async Task<ApiResponse<SysdatasetEntity>> InsertAsyncCusTom([FromBody] SysdatasetVM.ItemSysdataset _Model)
        {
            return await _manager.InsertAsyncCusTom(_Model);
        }

        [HttpPut]
        [Route("PostUpdateSysdatasetCustom")]
        public virtual async Task<ApiResponse> PostUpdateSysdatasetCustom([FromBody] SysdatasetVM.ItemSysdataset _Model)
        {
            return await _manager.UpdateAsyncCusTom(_Model);
        }

        [HttpDelete]
        [Route("PostDeleteSysdatasetCustom/{_id}")]
        public virtual async Task<ApiResponse> PostDeleteSysdatasetCustom(int _id)
        {
            return await _manager.DeleteAsyncCusTom(_id);
        }
    }
}


