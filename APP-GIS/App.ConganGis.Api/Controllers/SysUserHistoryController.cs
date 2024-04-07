using App.ConganGis.Services.Manager;
using App.CongAnGis.Dal.EntityClasses;
using App.CongAnGis.Services.Manager;
using App.Core.Common;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static App.CongAnGis.Services.Models.SysCauHinhModel;
using static App.CongAnGis.Services.Models.SysUserHistory;

namespace App.ConganGis.Api.Controllers
{
    [Route("App/gis/api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class SysUserHistoryController : Controller
    {
        private readonly ISysUserHistoryManager _manager;
        public SysUserHistoryController(ISysUserHistoryManager manager)
        {
            _manager = manager;
        }

        [HttpPost]
        [Route("paging")]
        public virtual async Task<ApiResponse<PageModelView<IEnumerable<SysUserHistoryModel>>>> Paging([FromBody] SysUserHistoryPagingModel _model)
        {
            return await _manager.Paging(_model);
        }

        [HttpGet]
        [Route("getOneByDate")]
        public virtual async Task<ApiResponse<IEnumerable<SysUserHistoryModel>>> getOneByDate(int userId, string device, string date)
        {
            return await _manager.getOneByDate(userId, device, date);
        }

        [HttpGet]
        [Route("getAllByStatus")]
        public virtual async Task<ApiResponse<IEnumerable<SysUserHistoryModel>>> getAllByStatus()
        {
            return await _manager.getAllByStatus();
        }

        [HttpGet]
        [Route("getAllByUserhistoryLine")]
        public virtual async Task<ApiResponse<IEnumerable<SysUserHistoryLineModel>>> getAllByUserhistoryLine(string userId, string device, string date)
        {
            return await _manager.getAllByUserhistoryLine(userId, device, date);
        }

        [HttpPost]
        [Route("InsertCustomAsync")]
        public virtual async Task<ApiResponse> InsertCustomAsync([FromBody] SysUserHistoryModel _Model)
        {
            return await _manager.InsertAsync(_Model);
        }

        [HttpPost]
        [Route("InsertWait")]
        public virtual async Task<ApiResponse> InsertWait([FromBody] SysUserHistoryModel _Model)
        {
            return await _manager.InsertWait(_Model);
        }


        [HttpPost]
        [Route("DeleteEntity")]
        public virtual async Task<ApiResponse> DeleteEntity(int id)
        {
            return await _manager.Delete(id);
        }
        [HttpGet]
        [Route("CreateHistoryUserJob")]
        public virtual async Task<bool> CreateHistoryUserJob(string jobName)
        {
            return await _manager.createJob(jobName);
        }

        [HttpGet]
        [Route("deleteJob")]
        public virtual async Task<bool> deleteJob(string jobName)
        {
            return await _manager.deleteJob(jobName);
        }


    }
}
