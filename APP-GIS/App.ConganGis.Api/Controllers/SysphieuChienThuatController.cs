using App.ConganGis.Services.Manager;
using App.Core.Common;
using Microsoft.AspNetCore.Mvc;
using static App.CongAnGis.Services.Models.SysBaoChayModel;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using static App.CongAnGis.Services.Models.DataModel;
using static App.ConganGis.Services.Model.SysphieuChienThuatModel;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;

namespace App.ConganGis.Api.Controllers
{
    [Route("App/gis/api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class SysphieuChienThuatController : ControllerBase
    {
        private readonly ISysphieuChienThuatManager _manager;

        public SysphieuChienThuatController(ISysphieuChienThuatManager manager)
        {
            _manager = manager;
        }

        [HttpPost]
        [Route("paging")]
        public virtual async Task<ApiResponse<PageModelView<IEnumerable<SysphieuchienthuatModel>>>> Paging([FromBody] SysphieuchienthuatPagingModel _model)
        {
            return await _manager.Paging(_model);
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("InsertDataCustom")]
        public virtual async Task<ApiResponse> InsertDataCustom([FromBody] phieuChienThuatModel model)
        {
            return await _manager.InsertDataCustom(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("UpdateDataCustom")]
        public virtual async Task<ApiResponse> UpdateDataCustom([FromBody] phieuChienThuatModel model)
        {
            return await _manager.UpdateDataCustom(model);
        }

        [HttpPost]
        [Route("DeleteEntity")]
        public virtual async Task<ApiResponse> DeleteEntity(int id)
        {
            return await _manager.Delete(id);
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("InsertInfoHoso")]
        public virtual async Task<ApiResponse> InsertInfoHoso([FromBody] SysPhieuchienthuatManagerModel model)
        {
            return await _manager.InsertInfoHoso(model);
        }

        [HttpGet]
        [Route("GetByttphieu/{id}")]
        public async Task<ApiResponse<SysListPhieuchienthuatManagerModel>> GetByttphieu([FromRoute]int id)
        {
            return await _manager.GetByttphieu(id);
        }

        [HttpPost]
        [Route("UploadFile")]
        [DisableRequestSizeLimit]
        public async Task<ApiResponse> UploadInfoPhieu(IList<IFormFile> files, [FromForm] UploadImgPhieuchienThuatModel model)
        {
            return await _manager.UploadInfoPhieu(files, model);
        }
    }
}
