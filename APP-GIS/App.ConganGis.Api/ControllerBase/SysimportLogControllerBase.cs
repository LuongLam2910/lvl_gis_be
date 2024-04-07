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
    public class SysimportLogsControllerBase : ControllerBase
    {

        private readonly SysimportLogsManagerBase _managerBase;
        private const string SysimportLogCacheKey = "SysimportLogKey";
        private IMemoryCache _cache;
        private ILogger<SysimportLogsControllerBase> _logger;
        public SysimportLogsControllerBase(IMemoryCache cache, ILogger<SysimportLogsControllerBase> logger)
        {
            _managerBase = new SysimportLogsManagerBase();
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        #region Base function 
        [HttpPost]
        [Route("PostPagingSysimportLog")]
        public virtual async Task<ApiResponse<PageModelView<IEnumerable<SysimportLogsVM.ItemSysimportLogs>>>> PostPagingSysimportLog([FromBody] SysimportLogsVM.PageSysimportLogs _model)
        {
            _logger.Log(LogLevel.Information, "PostPagingSysimportLog from cache.");
            if (_cache.TryGetValue(SysimportLogCacheKey + "_" + _model.strKey, out ApiResponse<PageModelView<IEnumerable<SysimportLogsVM.ItemSysimportLogs>>> toReturn))
            {
                _logger.Log(LogLevel.Information, "PostPagingSysimportLog find cache.");
            }
            else
            {
                try
                {
                    _logger.Log(LogLevel.Information, "PostPagingSysimportLog not fount cache. PostPagingSysimportLog from database.");
                    toReturn = await _managerBase.SelectPaging(_model);
                    var cacheEntryOptions = new MemoryCacheEntryOptions()
                            .SetSlidingExpiration(TimeSpan.FromSeconds(60))
                            .SetAbsoluteExpiration(TimeSpan.FromSeconds(3600))
                            .SetPriority(CacheItemPriority.Normal)
                            .SetSize(1024);
                    _cache.Set(SysimportLogCacheKey + "_" + _model.strKey, toReturn, cacheEntryOptions);
                }
                catch (Exception ex)
                {
                    ex.ToExceptionless().SetProperty("PostPagingSysimportLog", _model.strKey).Submit();
                }
            }
            return toReturn;
        }

        [HttpPost]
        [Route("PostInsertSysimportLog")]
        public virtual async Task<ApiResponse> PostInsertSysimportLog([FromBody] SysimportLogsVM.ItemSysimportLogs _Model)
        {
            return await _managerBase.InsertAsync(_Model);
        }

        [HttpPut]
        [Route("PostUpdateSysimportLog")]
        public virtual async Task<ApiResponse> PostUpdateSysimportLog([FromBody] SysimportLogsVM.ItemSysimportLogs _Model)
        {
            return await _managerBase.UpdateAsync(_Model);
        }

        [HttpDelete]
        [Route("PostDeleteSysimportLog/{_id}")]
        public virtual async Task<ApiResponse> PostDeleteSysimportLog(int _id)
        {
            return await _managerBase.DeleteAsync(_id);
        }


        [HttpGet]
        [Route("GetOneSysimportLog/{_id}")]
        public virtual async Task<ApiResponse<SysimportLogsVM.ItemSysimportLogs>> GetOneSysimportLog(int _id)
        {
            _logger.Log(LogLevel.Information, "GetOneSysimportLog from cache.");
            if (_cache.TryGetValue(SysimportLogCacheKey + "_" + _id, out ApiResponse<SysimportLogsVM.ItemSysimportLogs> toReturn))
            {
                _logger.Log(LogLevel.Information, "GetOneSysimportLog find cache.");
            }
            else
            {
                try
                {
                    _logger.Log(LogLevel.Information, "GetOneSysimportLog not fount cache. GetOneSysimportLog from database.");
                    toReturn = await _managerBase.SelectOneAsync(_id);
                    var cacheEntryOptions = new MemoryCacheEntryOptions()
                            .SetSlidingExpiration(TimeSpan.FromSeconds(60))
                            .SetAbsoluteExpiration(TimeSpan.FromSeconds(3600))
                            .SetPriority(CacheItemPriority.Normal)
                            .SetSize(1024);
                    _cache.Set(SysimportLogCacheKey + "_" + _id, toReturn, cacheEntryOptions);
                }
                catch (Exception ex)
                {
                    ex.ToExceptionless().SetProperty("GetOneSysimportLog", _id).Submit();
                }
            }
            return toReturn;
        }


        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("GetBystarttimeSysimportLog")]
        public virtual async Task<ApiResponse<List<SysimportLogsVM.ItemSysimportLogs>>> GetBystarttimeSysimportLog(DateTime? _starttime)
        {
            return await _managerBase.SelectBystarttimeAsync(_starttime);
        }

        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("GetByendtimeSysimportLog")]
        public virtual async Task<ApiResponse<List<SysimportLogsVM.ItemSysimportLogs>>> GetByendtimeSysimportLog(DateTime? _endtime)
        {
            return await _managerBase.SelectByendtimeAsync(_endtime);
        }

        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("GetBystatusSysimportLog")]
        public virtual async Task<ApiResponse<List<SysimportLogsVM.ItemSysimportLogs>>> GetBystatusSysimportLog(int? _status)
        {
            return await _managerBase.SelectBystatusAsync(_status);
        }

        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("GetBycountSysimportLog")]
        public virtual async Task<ApiResponse<List<SysimportLogsVM.ItemSysimportLogs>>> GetBycountSysimportLog(int? _count)
        {
            return await _managerBase.SelectBycountAsync(_count);
        }

        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("GetBymessageSysimportLog")]
        public virtual async Task<ApiResponse<List<SysimportLogsVM.ItemSysimportLogs>>> GetBymessageSysimportLog(string _message)
        {
            return await _managerBase.SelectBymessageAsync(_message);
        }

        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("GetByconfigSysimportLog")]
        public virtual async Task<ApiResponse<List<SysimportLogsVM.ItemSysimportLogs>>> GetByconfigSysimportLog(string _config)
        {
            return await _managerBase.SelectByconfigAsync(_config);
        }

        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("GetBytablenameSysimportLog")]
        public virtual async Task<ApiResponse<List<SysimportLogsVM.ItemSysimportLogs>>> GetBytablenameSysimportLog(string _tablename)
        {
            return await _managerBase.SelectBytablenameAsync(_tablename);
        }

        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("GetByunitcodeSysimportLog")]
        public virtual async Task<ApiResponse<List<SysimportLogsVM.ItemSysimportLogs>>> GetByunitcodeSysimportLog(string _unitcode)
        {
            return await _managerBase.SelectByunitcodeAsync(_unitcode);
        }

        #endregion
    }
}


