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
using Telerik.Reporting.Cache.Interfaces;
using static App.CongAnGis.Services.ManagerBase.SystrangthietbidoituongVM;
using static App.ConganGis.Services.Model.TrangThietBiModel;

namespace App.ConganGis.Api.Controllers
{
    [Route("App/gis/api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class SystrangthietbidoituongController : SystrangthietbidoituongControllerBase
    {
        private readonly SystrangthietbidoituongManager _manager = new SystrangthietbidoituongManager();
        public SystrangthietbidoituongController(IMemoryCache cache, ILogger<SystrangthietbidoituongControllerBase> logger) : base(cache, logger)
        {
        }

        [HttpPost]
        [Route("PostPagingSystrangthietbidoituongCustom")]
        public virtual async Task<ApiResponse<PageModelView<IEnumerable<TTBJoinModel>>>> PostPagingSystrangthietbidoituongCustom([FromBody] PageSystrangthietbi _model)
        {
            return await _manager.pagingCustom(_model); 
        }


        [HttpPost]
        [Route("UpdateTrangThaiTrangThietBi")]
        public virtual async Task<ApiResponse> UpdateTrangThaiTrangThietBi(int _id, string tablename, int trangthai)
        {
            return await _manager.UpdateTrangThaiTrangThietBi(_id, tablename, trangthai);
        }

        [HttpPost]
        [Route("GetTrangThaiTrangThietBi")]
        public virtual async Task<Boolean> GetTrangThaiTrangThietBi(int id, string tablename)
        {
            return await _manager.GetTrangThaiTrangThietBi(id, tablename);
        }

    }
}


