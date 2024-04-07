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
    public class SysfieldControllerBase : ControllerBase
    {

        private readonly SysfieldManagerBase _managerBase;
        private const string SysfieldCacheKey = "SysfieldKey";
        private IMemoryCache _cache;
        private ILogger<SysfieldControllerBase> _logger;
        public SysfieldControllerBase(IMemoryCache cache, ILogger<SysfieldControllerBase> logger)
        {
            _managerBase = new SysfieldManagerBase();
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        #region Base function 
        [HttpPost]
        [Route("PostPagingSysfield")]
        public virtual async Task<ApiResponse<PageModelView<IEnumerable<SysfieldVM.ItemSysfield>>>> PostPagingSysfield([FromBody] SysfieldVM.PageSysfield _model)
        {
            _logger.Log(LogLevel.Information, "PostPagingSysfield from cache.");
            if (_cache.TryGetValue(SysfieldCacheKey + "_" + _model.strKey, out ApiResponse<PageModelView<IEnumerable<SysfieldVM.ItemSysfield>>> toReturn))
            {
                _logger.Log(LogLevel.Information, "PostPagingSysfield find cache.");
            }
            else
            {
                try
                {
                    _logger.Log(LogLevel.Information, "PostPagingSysfield not fount cache. PostPagingSysfield from database.");
                    toReturn = await _managerBase.SelectPaging(_model);
                    var cacheEntryOptions = new MemoryCacheEntryOptions()
                            .SetSlidingExpiration(TimeSpan.FromSeconds(60))
                            .SetAbsoluteExpiration(TimeSpan.FromSeconds(3600))
                            .SetPriority(CacheItemPriority.Normal)
                            .SetSize(1024);
                    _cache.Set(SysfieldCacheKey + "_" + _model.strKey, toReturn, cacheEntryOptions);
                }
                catch (Exception ex)
                {
                    ex.ToExceptionless().SetProperty("PostPagingSysfield", _model.strKey).Submit();
                }
            }
            return toReturn;
        }

        //[HttpPost]
        //[Route("PostInsertSysfield")]
        //public virtual async Task<ApiResponse> PostInsertSysfield([FromBody] SysfieldVM.ItemSysfield _Model)
        //{
        //    return await _managerBase.InsertAsync(_Model);
        //}

        [HttpPut]
        [Route("PostUpdateSysfield")]
        public virtual async Task<ApiResponse> PostUpdateSysfield([FromBody] SysfieldVM.ItemSysfield _Model)
        {
            return await _managerBase.UpdateAsync(_Model);
        }

        //[HttpDelete]
        //[Route("PostDeleteSysfield/{_id}")]
        //public virtual async Task<ApiResponse> PostDeleteSysfield(int _id)
        //{
        //    return await _managerBase.DeleteAsync(_id);
        //}


        [HttpGet]
        [Route("GetOneSysfield/{_id}")]
        public virtual async Task<ApiResponse<SysfieldVM.ItemSysfield>> GetOneSysfield(int _id)
        {
            _logger.Log(LogLevel.Information, "GetOneSysfield from cache.");
            if (_cache.TryGetValue(SysfieldCacheKey + "_" + _id, out ApiResponse<SysfieldVM.ItemSysfield> toReturn))
            {
                _logger.Log(LogLevel.Information, "GetOneSysfield find cache.");
            }
            else
            {
                try
                {
                    _logger.Log(LogLevel.Information, "GetOneSysfield not fount cache. GetOneSysfield from database.");
                    toReturn = await _managerBase.SelectOneAsync(_id);
                    var cacheEntryOptions = new MemoryCacheEntryOptions()
                            .SetSlidingExpiration(TimeSpan.FromSeconds(60))
                            .SetAbsoluteExpiration(TimeSpan.FromSeconds(3600))
                            .SetPriority(CacheItemPriority.Normal)
                            .SetSize(1024);
                    _cache.Set(SysfieldCacheKey + "_" + _id, toReturn, cacheEntryOptions);
                }
                catch (Exception ex)
                {
                    ex.ToExceptionless().SetProperty("GetOneSysfield", _id).Submit();
                }
            }
            return toReturn;
        }


        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = false)]
        [Route("GetBynameSysfield")]
        public virtual async Task<ApiResponse<List<SysfieldVM.ItemSysfield>>> GetBynameSysfield(string _name)
        {
            return await _managerBase.SelectBynameAsync(_name);
        }

        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("GetBydatatypeSysfield")]
        public virtual async Task<ApiResponse<List<SysfieldVM.ItemSysfield>>> GetBydatatypeSysfield(string _datatype)
        {
            return await _managerBase.SelectBydatatypeAsync(_datatype);
        }

        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("GetBydatalengthSysfield")]
        public virtual async Task<ApiResponse<List<SysfieldVM.ItemSysfield>>> GetBydatalengthSysfield(string _datalength)
        {
            return await _managerBase.SelectBydatalengthAsync(_datalength);
        }

        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("GetByshowSysfield")]
        public virtual async Task<ApiResponse<List<SysfieldVM.ItemSysfield>>> GetByshowSysfield(string _show)
        {
            return await _managerBase.SelectByshowAsync(_show);
        }

        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("GetByfieldnameSysfield")]
        public virtual async Task<ApiResponse<List<SysfieldVM.ItemSysfield>>> GetByfieldnameSysfield(string _fieldname)
        {
            return await _managerBase.SelectByfieldnameAsync(_fieldname);
        }

        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("GetByfeatureclassSysfield")]
        public virtual async Task<ApiResponse<List<SysfieldVM.ItemSysfield>>> GetByfeatureclassSysfield(int? _featureclass)
        {
            return await _managerBase.SelectByfeatureclassAsync(_featureclass);
        }

        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("GetByconfigSysfield")]
        public virtual async Task<ApiResponse<List<SysfieldVM.ItemSysfield>>> GetByconfigSysfield(int? _config)
        {
            return await _managerBase.SelectByconfigAsync(_config);
        }

        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("GetByusercreatedSysfield")]
        public virtual async Task<ApiResponse<List<SysfieldVM.ItemSysfield>>> GetByusercreatedSysfield(int? _usercreated)
        {
            return await _managerBase.SelectByusercreatedAsync(_usercreated);
        }

        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("GetBydatecreatedSysfield")]
        public virtual async Task<ApiResponse<List<SysfieldVM.ItemSysfield>>> GetBydatecreatedSysfield(DateTime? _datecreated)
        {
            return await _managerBase.SelectBydatecreatedAsync(_datecreated);
        }

        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("GetBydatemodifiedSysfield")]
        public virtual async Task<ApiResponse<List<SysfieldVM.ItemSysfield>>> GetBydatemodifiedSysfield(DateTime? _datemodified)
        {
            return await _managerBase.SelectBydatemodifiedAsync(_datemodified);
        }

        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("GetByusermodifiedSysfield")]
        public virtual async Task<ApiResponse<List<SysfieldVM.ItemSysfield>>> GetByusermodifiedSysfield(int? _usermodified)
        {
            return await _managerBase.SelectByusermodifiedAsync(_usermodified);
        }

        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("GetByunitcodeSysfield")]
        public virtual async Task<ApiResponse<List<SysfieldVM.ItemSysfield>>> GetByunitcodeSysfield(string _unitcode)
        {
            return await _managerBase.SelectByunitcodeAsync(_unitcode);
        }

        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("GetBystatusSysfield")]
        public virtual async Task<ApiResponse<List<SysfieldVM.ItemSysfield>>> GetBystatusSysfield(int? _status)
        {
            return await _managerBase.SelectBystatusAsync(_status);
        }

        #endregion
    }
}


