using System;
using App.Core.Common;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.AspNetCore.Mvc;
using Exceptionless;
using App.ConganGis.Services.ManagerBase;

namespace App.CongAnGis.Api.Controllers
{
    public class SysbaotainanControllerBase : ControllerBase
    {

        private readonly SysbaotainanManagerBase _managerBase;
        private const string SysbaotainanCacheKey = "SysbaotainanKey";
        private IMemoryCache _cache;
        private ILogger<SysbaochayControllerBase> _logger;
        public SysbaotainanControllerBase(IMemoryCache cache, ILogger<SysbaotainanControllerBase> logger)
        {
            _managerBase = new SysbaotainanManagerBase();
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
            //_logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        #region Base function 
        [HttpPost]
        [Route("PostPagingSysbaotainan")]
        public virtual async Task<ApiResponse<PageModelView<IEnumerable<SysbaotainanVM.ItemSysbaotainan>>>> PostPagingSysbaochay([FromBody] SysbaotainanVM.PageSysbaotainan _model)
        {
            _logger.Log(LogLevel.Information, "PostPagingSysbaochay from cache.");
            if (_cache.TryGetValue(SysbaotainanCacheKey + "_" + _model.strKey, out ApiResponse<PageModelView<IEnumerable<SysbaotainanVM.ItemSysbaotainan>>> toReturn))
            {
                _logger.Log(LogLevel.Information, "PostPagingSysbaotainan find cache.");
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
                    _cache.Set(SysbaotainanCacheKey + "_" + _model.strKey, toReturn, cacheEntryOptions);
                }
                catch (Exception ex)
                {
                    ex.ToExceptionless().SetProperty("PostPagingSysbaotainan", _model.strKey).Submit();
                }
            }
            return toReturn;
        }

        [HttpPost]
        [Route("PostInsertSysbaotainan")]
        public virtual async Task<ApiResponse> PostInsertSysbaotainan([FromBody] SysbaotainanVM.ItemSysbaotainan _Model)
        {
            return await _managerBase.InsertAsync(_Model);
        }

        [HttpPut]
        [Route("PostUpdateSysbaotainan")]
        public virtual async Task<ApiResponse> PostUpdateSysbaotainan([FromBody] SysbaotainanVM.ItemSysbaotainan _Model)
        {
            return await _managerBase.UpdateAsync(_Model);
        }

        [HttpDelete]
        [Route("PostDeleteSysbaotainan/{_id}")]
        public virtual async Task<ApiResponse> PostDeleteSysbaotainan(int _id)
        {
            return await _managerBase.DeleteAsync(_id);
        }


        [HttpGet]
        [Route("GetOneSysbaotainan/{_id}")]
        public virtual async Task<ApiResponse<SysbaotainanVM.ItemSysbaotainan>> GetOneSysbaochay(int _id)
        {
            return await _managerBase.SelectOneAsync(_id);
            //_logger.Log(LogLevel.Information, "GetOneSysbaochay from cache.");
            //if (_cache.TryGetValue(SysbaochayCacheKey + "_" + _id, out ApiResponse<SysbaotainanVM.ItemSysbaotainan> toReturn))
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
        [Route("GetByiddataSysbaotainan")]
        public virtual async Task<ApiResponse<List<SysbaotainanVM.ItemSysbaotainan>>> GetByiddataSysbaotainan(int? _iddata)
        {
            return await _managerBase.SelectByiddataAsync(_iddata);
        }

        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("GetBycreatebyidSysbaotainan")]
        public virtual async Task<ApiResponse<List<SysbaotainanVM.ItemSysbaotainan>>> GetBycreatebyidSysbaotainan(int? _createbyid)
        {
            return await _managerBase.SelectBycreatebyidAsync(_createbyid);
        }

        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("GetBycreatebySysbaotainan")]
        public virtual async Task<ApiResponse<List<SysbaotainanVM.ItemSysbaotainan>>> GetBycreatebySysbaotainan(object? _createby)
        {
            return await _managerBase.SelectBycreatebyAsync(_createby);
        }

        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("GetBycreatedateSysbaotainan")]
        public virtual async Task<ApiResponse<List<SysbaotainanVM.ItemSysbaotainan>>> GetBycreatedateSysbaotainan(DateTime? _createdate)
        {
            return await _managerBase.SelectBycreatedateAsync(_createdate);
        }

        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("GetByaddressSysbaotainan")]
        public virtual async Task<ApiResponse<List<SysbaotainanVM.ItemSysbaotainan>>> GetByaddressSysbaotainan(string _address)
        {
            return await _managerBase.SelectByaddressAsync(_address);
        }

        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("GetByphonenumberSysbaotainan")]
        public virtual async Task<ApiResponse<List<SysbaotainanVM.ItemSysbaotainan>>> GetByphonenumberSysbaotainan(string _phonenumber)
        {
            return await _managerBase.SelectByphonenumberAsync(_phonenumber);
        }

        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("GetByreasonfireSysbaotainan")]
        public virtual async Task<ApiResponse<List<SysbaotainanVM.ItemSysbaotainan>>> GetByreasonfireSysbaotainan(object? _reasonfire)
        {
            return await _managerBase.SelectByreasonfireAsync(_reasonfire);
        }

        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("GetBystatusSysbaotainan")]
        public virtual async Task<ApiResponse<List<SysbaotainanVM.ItemSysbaotainan>>> GetBystatusSysbaotainan(int? _status)
        {
            return await _managerBase.SelectBystatusAsync(_status);
        }

        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("GetByupdateatSysbaotainan")]
        public virtual async Task<ApiResponse<List<SysbaotainanVM.ItemSysbaotainan>>> GetByupdateatSysbaotainan(DateTime? _updateat)
        {
            return await _managerBase.SelectByupdateatAsync(_updateat);
        }

        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("GetBytablenameSysbaotainan")]
        public virtual async Task<ApiResponse<List<SysbaotainanVM.ItemSysbaotainan>>> GetBytablenameSysbaotainan(string _tablename)
        {
            return await _managerBase.SelectBytablenameAsync(_tablename);
        }

        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("GetBylnglatSysbaotainan")]
        public virtual async Task<ApiResponse<List<SysbaotainanVM.ItemSysbaotainan>>> GetBylnglatSysbaotainan(string _lnglat)
        {
            return await _managerBase.SelectBylnglatAsync(_lnglat);
        }

        #endregion
    }
}


