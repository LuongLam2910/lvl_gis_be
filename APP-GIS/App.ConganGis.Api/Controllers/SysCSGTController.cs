using App.ConganGis.Services.Manager;
using App.ConganGis.Services.Model;
using App.CongAnGis.Api.Controllers;
using App.CongAnGis.Dal.EntityClasses;
using App.CongAnGis.Services.Manager;
using App.CongAnGis.Services.ManagerBase;
using App.Core.Common;
using DocumentFormat.OpenXml.EMMA;
using DocumentFormat.OpenXml.Office2010.Excel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace App.ConganGis.Api.Controllers
{
    [Route("App/gis/api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class SysCSGTController : Controller
    {
        private readonly ISysCSGTManager _manager;
        public SysCSGTController(ISysCSGTManager manager)
        {
            _manager = manager;
        }
        
        [HttpPost]
        [Route("Paging")]
        public virtual async Task<ApiResponse<PageModelView<IEnumerable<CSGTModel>>>> Paging([FromBody]CSGTModelPageModel model)
        { 
            return await _manager.Paging(model);
        }
        [HttpGet]
        [Route("getAll")]
        public virtual async Task<ApiResponse<IEnumerable<CSGTModel>>> getAll()
        { 
            return await _manager.getAll();
        }
        [HttpPost]
        [Route("InsertCustomAsync")]
        public virtual async Task<ApiResponse> InsertCustomAsync([FromBody] CSGTModel model)
        {
            return await _manager.InsertCustomAsync(model);
        }

        [HttpPost]
        [Route("UpdateAsync")]
        public virtual async Task<ApiResponse> UpdateAsync([FromBody] CSGTModel model)
        {
            return await _manager.UpdateAsync(model);
        }
        [HttpDelete]
        [Route("Delete")]
        public virtual async Task<ApiResponse> Delete(int id)
        {
            return await _manager.Delete(id);
        }
    }
}
