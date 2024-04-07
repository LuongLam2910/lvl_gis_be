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
    public class SyssettingsControllerBase : ControllerBase
    {

        private readonly SyssettingsManagerBase _managerBase;
        private const string SyssettingCacheKey = "SyssettingKey";
        private IMemoryCache _cache;
        private ILogger<SyssettingsControllerBase> _logger;
        public SyssettingsControllerBase(IMemoryCache cache, ILogger<SyssettingsControllerBase> logger)
        {
            _managerBase = new SyssettingsManagerBase();
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        #region Base function 
        [HttpPost]
        [Route("PostPagingSyssetting")]
        public virtual async Task<ApiResponse<PageModelView<IEnumerable<SyssettingsVM.ItemSyssettings>>>> PostPagingSyssetting([FromBody] SyssettingsVM.PageSyssettings _model)
        {
            _logger.Log(LogLevel.Information, "PostPagingSyssetting from cache.");
            if (_cache.TryGetValue(SyssettingCacheKey + "_" + _model.strKey, out ApiResponse<PageModelView<IEnumerable<SyssettingsVM.ItemSyssettings>>> toReturn))
            {
                _logger.Log(LogLevel.Information, "PostPagingSyssetting find cache.");
            }
            else
            {
                try
                {
                    _logger.Log(LogLevel.Information, "PostPagingSyssetting not fount cache. PostPagingSyssetting from database.");
                    toReturn = await _managerBase.SelectPaging(_model);
                    var cacheEntryOptions = new MemoryCacheEntryOptions()
                            .SetSlidingExpiration(TimeSpan.FromSeconds(60))
                            .SetAbsoluteExpiration(TimeSpan.FromSeconds(3600))
                            .SetPriority(CacheItemPriority.Normal)
                            .SetSize(1024);
                    _cache.Set(SyssettingCacheKey + "_" + _model.strKey, toReturn, cacheEntryOptions);
                }
                catch (Exception ex)
                {
                    ex.ToExceptionless().SetProperty("PostPagingSyssetting", _model.strKey).Submit();
                }
            }
            return toReturn;
        }

        [HttpPost]
        [Route("PostInsertSyssetting")]
        public virtual async Task<ApiResponse> PostInsertSyssetting([FromBody] SyssettingsVM.ItemSyssettings _Model)
        {
            return await _managerBase.InsertAsync(_Model);
        }

        [HttpPut]
        [Route("PostUpdateSyssetting")]
        public virtual async Task<ApiResponse> PostUpdateSyssetting([FromBody] SyssettingsVM.ItemSyssettings _Model)
        {
            return await _managerBase.UpdateAsync(_Model);
        }

        [HttpDelete]
        [Route("PostDeleteSyssetting/{_id}")]
        public virtual async Task<ApiResponse> PostDeleteSyssetting(int _id)
        {
            return await _managerBase.DeleteAsync(_id);
        }


        [HttpGet]
        [Route("GetOneSyssetting/{_id}")]
        public virtual async Task<ApiResponse<SyssettingsVM.ItemSyssettings>> GetOneSyssetting(int _id)
        {
            _logger.Log(LogLevel.Information, "GetOneSyssetting from cache.");
            if (_cache.TryGetValue(SyssettingCacheKey + "_" + _id, out ApiResponse<SyssettingsVM.ItemSyssettings> toReturn))
            {
                _logger.Log(LogLevel.Information, "GetOneSyssetting find cache.");
            }
            else
            {
                try
                {
                    _logger.Log(LogLevel.Information, "GetOneSyssetting not fount cache. GetOneSyssetting from database.");
                    toReturn = await _managerBase.SelectOneAsync(_id);
                    var cacheEntryOptions = new MemoryCacheEntryOptions()
                            .SetSlidingExpiration(TimeSpan.FromSeconds(60))
                            .SetAbsoluteExpiration(TimeSpan.FromSeconds(3600))
                            .SetPriority(CacheItemPriority.Normal)
                            .SetSize(1024);
                    _cache.Set(SyssettingCacheKey + "_" + _id, toReturn, cacheEntryOptions);
                }
                catch (Exception ex)
                {
                    ex.ToExceptionless().SetProperty("GetOneSyssetting", _id).Submit();
                }
            }
            return toReturn;
        }


        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("GetBynameSyssetting")]
        public virtual async Task<ApiResponse<List<SyssettingsVM.ItemSyssettings>>> GetBynameSyssetting(string _name)
        {
            return await _managerBase.SelectBynameAsync(_name);
        }

        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("GetByconfigSyssetting")]
        public virtual async Task<ApiResponse<List<SyssettingsVM.ItemSyssettings>>> GetByconfigSyssetting(int? _config)
        {
            return await _managerBase.SelectByconfigAsync(_config);
        }

        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("GetByusercreatedSyssetting")]
        public virtual async Task<ApiResponse<List<SyssettingsVM.ItemSyssettings>>> GetByusercreatedSyssetting(int? _usercreated)
        {
            return await _managerBase.SelectByusercreatedAsync(_usercreated);
        }

        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("GetBydatecreatedSyssetting")]
        public virtual async Task<ApiResponse<List<SyssettingsVM.ItemSyssettings>>> GetBydatecreatedSyssetting(DateTime? _datecreated)
        {
            return await _managerBase.SelectBydatecreatedAsync(_datecreated);
        }

        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("GetBydatemodifiedSyssetting")]
        public virtual async Task<ApiResponse<List<SyssettingsVM.ItemSyssettings>>> GetBydatemodifiedSyssetting(DateTime? _datemodified)
        {
            return await _managerBase.SelectBydatemodifiedAsync(_datemodified);
        }

        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("GetByusermodifiedSyssetting")]
        public virtual async Task<ApiResponse<List<SyssettingsVM.ItemSyssettings>>> GetByusermodifiedSyssetting(int? _usermodified)
        {
            return await _managerBase.SelectByusermodifiedAsync(_usermodified);
        }

        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("GetBycodeSyssetting")]
        public virtual async Task<ApiResponse<List<SyssettingsVM.ItemSyssettings>>> GetBycodeSyssetting(string _code)
        {
            return await _managerBase.SelectBycodeAsync(_code);
        }

        #endregion
    }
}


