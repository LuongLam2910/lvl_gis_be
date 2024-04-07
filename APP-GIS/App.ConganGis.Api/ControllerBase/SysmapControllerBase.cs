using System;
using App.Core.Common;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.AspNetCore.Mvc;
using Exceptionless;
using App.CongAnGis.Services.ManagerBase;
using DocumentFormat.OpenXml.ExtendedProperties;

namespace App.CongAnGis.Api.Controllers
{
    public class SysmapControllerBase : ControllerBase
    {

        private readonly SysmapManagerBase _managerBase;
        private const string SysmapCacheKey = "SysmapKey";
        private IMemoryCache _cache;
        private ILogger<SysmapControllerBase> _logger;
        public SysmapControllerBase(IMemoryCache cache, ILogger<SysmapControllerBase> logger)
        {
            _managerBase = new SysmapManagerBase();
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        [Route("GetAllSysmap")]
        public virtual async Task<ApiResponse<List<SysmapVM.ItemSysmap>>> GetAllSysMap()
        {
            return await _managerBase.SelectAllAsync();
        }

        #region Base function 
        [HttpPost]
        [Route("PostPagingSysmap")]
        public virtual async Task<ApiResponse<PageModelView<IEnumerable<SysmapVM.ItemSysmap>>>> PostPagingSysmap([FromBody] SysmapVM.PageSysmap _model)
        {
            /*
            _logger.Log(LogLevel.Information, "PostPagingSysmap from cache.");
            if (_cache.TryGetValue(SysmapCacheKey + "_" + _model.strKey, out ApiResponse<PageModelView<IEnumerable<SysmapVM.ItemSysmap>>> toReturn))
            {
                _logger.Log(LogLevel.Information, "PostPagingSysmap find cache.");
            }
            else
            {
                try
                {
                    _logger.Log(LogLevel.Information, "PostPagingSysmap not fount cache. PostPagingSysmap from database.");
                    toReturn = await _managerBase.SelectPaging(_model);
                    var cacheEntryOptions = new MemoryCacheEntryOptions()
                            .SetSlidingExpiration(TimeSpan.FromSeconds(60))
                            .SetAbsoluteExpiration(TimeSpan.FromSeconds(3600))
                            .SetPriority(CacheItemPriority.Normal)
                            .SetSize(1024);
                    _cache.Set(SysmapCacheKey + "_" + _model.strKey, toReturn, cacheEntryOptions);
                }
                catch (Exception ex)
                {
                    ex.ToExceptionless().SetProperty("PostPagingSysmap", _model.strKey).Submit();
                }
            }
            return toReturn; */
            return await _managerBase.SelectPaging(_model);
        }

        [HttpPost]
        [Route("PostInsertSysmap")]
        public virtual async Task<ApiResponse> PostInsertSysmap([FromBody] SysmapVM.ItemSysmap _Model)
        {
            return await _managerBase.InsertAsync(_Model);
        }

        [HttpPut]
        [Route("PostUpdateSysmap")]
        public virtual async Task<ApiResponse> PostUpdateSysmap([FromBody] SysmapVM.ItemSysmap _Model)
        {
            return await _managerBase.UpdateAsync(_Model);
        }

        [HttpDelete]
        [Route("PostDeleteSysmap/{_id}")]
        public virtual async Task<ApiResponse> PostDeleteSysmap(int _id)
        {
            return await _managerBase.DeleteAsync(_id);
        }


        [HttpGet]
        [Route("GetOneSysmap/{_id}")]
        public virtual async Task<ApiResponse<SysmapVM.ItemSysmap>> GetOneSysmap(int _id)
        {
            /*
            _logger.Log(LogLevel.Information, "GetOneSysmap from cache.");
            if (_cache.TryGetValue(SysmapCacheKey + "_" + _id, out ApiResponse<SysmapVM.ItemSysmap> toReturn))
            {
                _logger.Log(LogLevel.Information, "GetOneSysmap find cache.");
            }
            else
            {
                try
                {
                    _logger.Log(LogLevel.Information, "GetOneSysmap not fount cache. GetOneSysmap from database.");
                    toReturn = await _managerBase.SelectOneAsync(_id);
                    var cacheEntryOptions = new MemoryCacheEntryOptions()
                            .SetSlidingExpiration(TimeSpan.FromSeconds(60))
                            .SetAbsoluteExpiration(TimeSpan.FromSeconds(3600))
                            .SetPriority(CacheItemPriority.Normal)
                            .SetSize(1024);
                    _cache.Set(SysmapCacheKey + "_" + _id, toReturn, cacheEntryOptions);
                }
                catch (Exception ex)
                {
                    ex.ToExceptionless().SetProperty("GetOneSysmap", _id).Submit();
                }
            }
            return toReturn;*/
            return await _managerBase.SelectOneAsync(_id);
        }


        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("GetBynameSysmap")]
        public virtual async Task<ApiResponse<List<SysmapVM.ItemSysmap>>> GetBynameSysmap(string _name)
        {
            return await _managerBase.SelectBynameAsync(_name);
        }

        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("GetByconfigSysmap")]
        public virtual async Task<ApiResponse<List<SysmapVM.ItemSysmap>>> GetByconfigSysmap(int? _config)
        {
            return await _managerBase.SelectByconfigAsync(_config);
        }

        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("GetByusercreatedSysmap")]
        public virtual async Task<ApiResponse<List<SysmapVM.ItemSysmap>>> GetByusercreatedSysmap(int? _usercreated)
        {
            return await _managerBase.SelectByusercreatedAsync(_usercreated);
        }

        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("GetBydatecreatedSysmap")]
        public virtual async Task<ApiResponse<List<SysmapVM.ItemSysmap>>> GetBydatecreatedSysmap(DateTime? _datecreated)
        {
            return await _managerBase.SelectBydatecreatedAsync(_datecreated);
        }

        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("GetBydatemodifiedSysmap")]
        public virtual async Task<ApiResponse<List<SysmapVM.ItemSysmap>>> GetBydatemodifiedSysmap(DateTime? _datemodified)
        {
            return await _managerBase.SelectBydatemodifiedAsync(_datemodified);
        }

        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("GetByusermodifiedSysmap")]
        public virtual async Task<ApiResponse<List<SysmapVM.ItemSysmap>>> GetByusermodifiedSysmap(int? _usermodified)
        {
            return await _managerBase.SelectByusermodifiedAsync(_usermodified);
        }

        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("GetBystatusSysmap")]
        public virtual async Task<ApiResponse<List<SysmapVM.ItemSysmap>>> GetBystatusSysmap(string _status)
        {
            return await _managerBase.SelectBystatusAsync(_status);
        }

        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("GetByiconhomeSysmap")]
        public virtual async Task<ApiResponse<List<SysmapVM.ItemSysmap>>> GetByiconhomeSysmap(string _iconhome)
        {
            return await _managerBase.SelectByiconhomeAsync(_iconhome);
        }

        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("GetByunitcodeSysmap")]
        public virtual async Task<ApiResponse<List<SysmapVM.ItemSysmap>>> GetByunitcodeSysmap(string _unitcode)
        {
            return await _managerBase.SelectByunitcodeAsync(_unitcode);
        }

        #endregion
    }
}


