using System;
using App.Core.Common;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.AspNetCore.Mvc;
using Exceptionless;
using App.CongAnGis.Services.ManagerBase;

namespace App.CongAnGis.Api.Controllers
{
    public class SysbaochayControllerBase : ControllerBase
    {

        private readonly SysbaochayManagerBase _managerBase;
        private const string SysbaochayCacheKey = "SysbaochayKey";
        private IMemoryCache _cache;
        private ILogger<SysbaochayControllerBase> _logger;
        public SysbaochayControllerBase(IMemoryCache cache, ILogger<SysbaochayControllerBase> logger)
        {
            _managerBase = new SysbaochayManagerBase();
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        #region Base function 
        [HttpPost]
        [Route("PostPagingSysbaochay")]
        public virtual async Task<ApiResponse<PageModelView<IEnumerable<SysbaochayVM.ItemSysbaochay>>>> PostPagingSysbaochay([FromBody] SysbaochayVM.PageSysbaochay _model)
        {
            _logger.Log(LogLevel.Information, "PostPagingSysbaochay from cache.");
            if (_cache.TryGetValue(SysbaochayCacheKey + "_" + _model.strKey, out ApiResponse<PageModelView<IEnumerable<SysbaochayVM.ItemSysbaochay>>> toReturn))
            {
                _logger.Log(LogLevel.Information, "PostPagingSysbaochay find cache.");
            }
            else
            {
                try
                {
                    _logger.Log(LogLevel.Information, "PostPagingSysbaochay not fount cache. PostPagingSysbaochay from database.");
                    toReturn = await _managerBase.SelectPaging(_model);
                    var cacheEntryOptions = new MemoryCacheEntryOptions()
                            .SetSlidingExpiration(TimeSpan.FromSeconds(60))
                            .SetAbsoluteExpiration(TimeSpan.FromSeconds(3600))
                            .SetPriority(CacheItemPriority.Normal)
                            .SetSize(1024);
                    _cache.Set(SysbaochayCacheKey + "_" + _model.strKey, toReturn, cacheEntryOptions);
                }
                catch (Exception ex)
                {
                    ex.ToExceptionless().SetProperty("PostPagingSysbaochay", _model.strKey).Submit();
                }
            }
            return toReturn;
        }

        [HttpPost]
        [Route("PostInsertSysbaochay")]
        public virtual async Task<ApiResponse> PostInsertSysbaochay([FromBody] SysbaochayVM.ItemSysbaochay _Model)
        {
            return await _managerBase.InsertAsync(_Model);
        }

        [HttpPut]
        [Route("PostUpdateSysbaochay")]
        public virtual async Task<ApiResponse> PostUpdateSysbaochay([FromBody] SysbaochayVM.ItemSysbaochay _Model)
        {
            return await _managerBase.UpdateAsync(_Model);
        }

        [HttpDelete]
        [Route("PostDeleteSysbaochay/{_id}")]
        public virtual async Task<ApiResponse> PostDeleteSysbaochay(int _id)
        {
            return await _managerBase.DeleteAsync(_id);
        }


        [HttpGet]
        [Route("GetOneSysbaochay/{_id}")]
        public virtual async Task<ApiResponse<SysbaochayVM.ItemSysbaochay>> GetOneSysbaochay(int _id)
        {
            return await _managerBase.SelectOneAsync(_id);
            //_logger.Log(LogLevel.Information, "GetOneSysbaochay from cache.");
            //if (_cache.TryGetValue(SysbaochayCacheKey + "_" + _id, out ApiResponse<SysbaochayVM.ItemSysbaochay> toReturn))
            //{
            //    _logger.Log(LogLevel.Information, "GetOneSysbaochay find cache.");
            //}
            //else
            //{
            //    try
            //    {
            //        _logger.Log(LogLevel.Information, "GetOneSysbaochay not fount cache. GetOneSysbaochay from database.");
            //        toReturn = await _managerBase.SelectOneAsync(_id);
            //        var cacheEntryOptions = new MemoryCacheEntryOptions()
            //                .SetSlidingExpiration(TimeSpan.FromSeconds(60))
            //                .SetAbsoluteExpiration(TimeSpan.FromSeconds(3600))
            //                .SetPriority(CacheItemPriority.Normal)
            //                .SetSize(1024);
            //        _cache.Set(SysbaochayCacheKey + "_" + _id, toReturn, cacheEntryOptions);
            //    }
            //    catch (Exception ex)
            //    {
            //        ex.ToExceptionless().SetProperty("GetOneSysbaochay", _id).Submit();
            //    }
            //}
            //return toReturn;
        }


        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("GetByiddataSysbaochay")]
        public virtual async Task<ApiResponse<List<SysbaochayVM.ItemSysbaochay>>> GetByiddataSysbaochay(int? _iddata)
        {
            return await _managerBase.SelectByiddataAsync(_iddata);
        }

        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("GetBycreatebyidSysbaochay")]
        public virtual async Task<ApiResponse<List<SysbaochayVM.ItemSysbaochay>>> GetBycreatebyidSysbaochay(int? _createbyid)
        {
            return await _managerBase.SelectBycreatebyidAsync(_createbyid);
        }

        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("GetBycreatebySysbaochay")]
        public virtual async Task<ApiResponse<List<SysbaochayVM.ItemSysbaochay>>> GetBycreatebySysbaochay(object? _createby)
        {
            return await _managerBase.SelectBycreatebyAsync(_createby);
        }

        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("GetBycreatedateSysbaochay")]
        public virtual async Task<ApiResponse<List<SysbaochayVM.ItemSysbaochay>>> GetBycreatedateSysbaochay(DateTime? _createdate)
        {
            return await _managerBase.SelectBycreatedateAsync(_createdate);
        }

        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("GetByaddressSysbaochay")]
        public virtual async Task<ApiResponse<List<SysbaochayVM.ItemSysbaochay>>> GetByaddressSysbaochay(string _address)
        {
            return await _managerBase.SelectByaddressAsync(_address);
        }

        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("GetByphonenumberSysbaochay")]
        public virtual async Task<ApiResponse<List<SysbaochayVM.ItemSysbaochay>>> GetByphonenumberSysbaochay(string _phonenumber)
        {
            return await _managerBase.SelectByphonenumberAsync(_phonenumber);
        }

        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("GetByreasonfireSysbaochay")]
        public virtual async Task<ApiResponse<List<SysbaochayVM.ItemSysbaochay>>> GetByreasonfireSysbaochay(object? _reasonfire)
        {
            return await _managerBase.SelectByreasonfireAsync(_reasonfire);
        }

        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("GetBystatusSysbaochay")]
        public virtual async Task<ApiResponse<List<SysbaochayVM.ItemSysbaochay>>> GetBystatusSysbaochay(int? _status)
        {
            return await _managerBase.SelectBystatusAsync(_status);
        }

        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("GetByupdateatSysbaochay")]
        public virtual async Task<ApiResponse<List<SysbaochayVM.ItemSysbaochay>>> GetByupdateatSysbaochay(DateTime? _updateat)
        {
            return await _managerBase.SelectByupdateatAsync(_updateat);
        }

        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("GetBytablenameSysbaochay")]
        public virtual async Task<ApiResponse<List<SysbaochayVM.ItemSysbaochay>>> GetBytablenameSysbaochay(string _tablename)
        {
            return await _managerBase.SelectBytablenameAsync(_tablename);
        }

        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("GetBylnglatSysbaochay")]
        public virtual async Task<ApiResponse<List<SysbaochayVM.ItemSysbaochay>>> GetBylnglatSysbaochay(string _lnglat)
        {
            return await _managerBase.SelectBylnglatAsync(_lnglat);
        }

        #endregion
    }
}


