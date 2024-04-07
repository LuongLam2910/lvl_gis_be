using App.ConganGis.Services.Manager;
using App.CongAnGis.Services.Manager;
using App.Core.Common;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using static App.CongAnGis.Services.Models.SysCauHinhModel;

namespace App.ConganGis.Api.Controllers
{
    [Route("App/gis/api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class SysCauHinhController : Controller
    {
        private readonly ISysCauHinhManager _manager;
        public SysCauHinhController(ISysCauHinhManager manager)
        {
            _manager = manager;
        }

        [HttpPost]
        [Route("paging")]
        public virtual async Task<ApiResponse<PageModelView<IEnumerable<CauHinhResponseModel>>>> Paging([FromBody] CauHinhPagingModel _model)
        {
            return await _manager.Paging(_model);
        }

        [HttpPost]
        [Route("InsertCustomAsync")]
        public virtual async Task<ApiResponse> InsertCustomAsync([FromBody] CauHinhResponseModel _Model)
        {
            return await _manager.InsertAsync(_Model);
        }

        [HttpPost]
        [Route("distancematrix")]
        public virtual async Task<ApiResponse> distancematrix([FromBody] CauHinhResponseModel _Model)
        {
            return await _manager.distancematrix(_Model);
        }

        [HttpPost]
        [Route("DeleteEntity")]
        public virtual async Task<ApiResponse> DeleteEntity(int id)
        {
            return await _manager.Delete(id);
        }
        
    }
}
