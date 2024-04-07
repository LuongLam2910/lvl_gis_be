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
    public class SysfilemanagerControllerBase : ControllerBase
    {

        private readonly SysfilemanagerManagerBase _managerBase;
        private const string SysfilemanagerCacheKey = "SysfilemanagerKey";
        private IMemoryCache _cache;
        private ILogger<SysfilemanagerControllerBase> _logger;
        public SysfilemanagerControllerBase(IMemoryCache cache, ILogger<SysfilemanagerControllerBase> logger)
        {
            _managerBase = new SysfilemanagerManagerBase();
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        #region Base function 
        [HttpPost]
        [Route("PostPagingSysfilemanager")]
        public virtual async Task<ApiResponse<PageModelView<IEnumerable<SysfilemanagerVM.ItemSysfilemanager>>>> PostPagingSysfilemanager([FromBody] SysfilemanagerVM.PageSysfilemanager _model)
        {
            _logger.Log(LogLevel.Information, "PostPagingSysfilemanager from cache.");
            if (_cache.TryGetValue(SysfilemanagerCacheKey + "_" + _model.strKey, out ApiResponse<PageModelView<IEnumerable<SysfilemanagerVM.ItemSysfilemanager>>> toReturn))
            {
                _logger.Log(LogLevel.Information, "PostPagingSysfilemanager find cache.");
            }
            else
            {
                try
                {
                    _logger.Log(LogLevel.Information, "PostPagingSysfilemanager not fount cache. PostPagingSysfilemanager from database.");
                    toReturn = await _managerBase.SelectPaging(_model);
                    var cacheEntryOptions = new MemoryCacheEntryOptions()
                            .SetSlidingExpiration(TimeSpan.FromSeconds(60))
                            .SetAbsoluteExpiration(TimeSpan.FromSeconds(3600))
                            .SetPriority(CacheItemPriority.Normal)
                            .SetSize(1024);
                    _cache.Set(SysfilemanagerCacheKey + "_" + _model.strKey, toReturn, cacheEntryOptions);
                }
                catch (Exception ex)
                {
                    ex.ToExceptionless().SetProperty("PostPagingSysfilemanager", _model.strKey).Submit();
                }
            }
            return toReturn;
        }

        [HttpPost]
        [Route("PostInsertSysfilemanager")]
        public virtual async Task<ApiResponse> PostInsertSysfilemanager([FromBody] SysfilemanagerVM.ItemSysfilemanager _Model)
        {
            return await _managerBase.InsertAsync(_Model);
        }

        [HttpPut]
        [Route("PostUpdateSysfilemanager")]
        public virtual async Task<ApiResponse> PostUpdateSysfilemanager([FromBody] SysfilemanagerVM.ItemSysfilemanager _Model)
        {
            return await _managerBase.UpdateAsync(_Model);
        }

        [HttpDelete]
        [Route("PostDeleteSysfilemanager/{_id}")]
        public virtual async Task<ApiResponse> PostDeleteSysfilemanager(int _id)
        {
            return await _managerBase.DeleteAsync(_id);
        }


        [HttpGet]
        [Route("GetOneSysfilemanager/{_id}")]
        public virtual async Task<ApiResponse<SysfilemanagerVM.ItemSysfilemanager>> GetOneSysfilemanager(int _id)
        {
            _logger.Log(LogLevel.Information, "GetOneSysfilemanager from cache.");
            if (_cache.TryGetValue(SysfilemanagerCacheKey + "_" + _id, out ApiResponse<SysfilemanagerVM.ItemSysfilemanager> toReturn))
            {
                _logger.Log(LogLevel.Information, "GetOneSysfilemanager find cache.");
            }
            else
            {
                try
                {
                    _logger.Log(LogLevel.Information, "GetOneSysfilemanager not fount cache. GetOneSysfilemanager from database.");
                    toReturn = await _managerBase.SelectOneAsync(_id);
                    var cacheEntryOptions = new MemoryCacheEntryOptions()
                            .SetSlidingExpiration(TimeSpan.FromSeconds(60))
                            .SetAbsoluteExpiration(TimeSpan.FromSeconds(3600))
                            .SetPriority(CacheItemPriority.Normal)
                            .SetSize(1024);
                    _cache.Set(SysfilemanagerCacheKey + "_" + _id, toReturn, cacheEntryOptions);
                }
                catch (Exception ex)
                {
                    ex.ToExceptionless().SetProperty("GetOneSysfilemanager", _id).Submit();
                }
            }
            return toReturn;
        }


        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("GetBytablenameSysfilemanager")]
        public virtual async Task<ApiResponse<List<SysfilemanagerVM.ItemSysfilemanager>>> GetBytablenameSysfilemanager(string _tablename)
        {
            return await _managerBase.SelectBytablenameAsync(_tablename);
        }

        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("GetByfilenameSysfilemanager")]
        public virtual async Task<ApiResponse<List<SysfilemanagerVM.ItemSysfilemanager>>> GetByfilenameSysfilemanager(string _filename)
        {
            return await _managerBase.SelectByfilenameAsync(_filename);
        }

        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("GetByfilepathSysfilemanager")]
        public virtual async Task<ApiResponse<List<SysfilemanagerVM.ItemSysfilemanager>>> GetByfilepathSysfilemanager(string _filepath)
        {
            return await _managerBase.SelectByfilepathAsync(_filepath);
        }

        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("GetBycreateddateSysfilemanager")]
        public virtual async Task<ApiResponse<List<SysfilemanagerVM.ItemSysfilemanager>>> GetBycreateddateSysfilemanager(DateTime? _createddate)
        {
            return await _managerBase.SelectBycreateddateAsync(_createddate);
        }

        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("GetBycreateuseridSysfilemanager")]
        public virtual async Task<ApiResponse<List<SysfilemanagerVM.ItemSysfilemanager>>> GetBycreateuseridSysfilemanager(int? _createuserid)
        {
            return await _managerBase.SelectBycreateuseridAsync(_createuserid);
        }

        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = false)]
        [Route("GetByidddataSysfilemanager")]
        public virtual async Task<ApiResponse<List<SysfilemanagerVM.ItemSysfilemanager>>> GetByidddataSysfilemanager(int? _idddata)
        {
            return await _managerBase.SelectByidddataAsync(_idddata);
        }

        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("GetBytypefileSysfilemanager")]
        public virtual async Task<ApiResponse<List<SysfilemanagerVM.ItemSysfilemanager>>> GetBytypefileSysfilemanager(string _typefile)
        {
            return await _managerBase.SelectBytypefileAsync(_typefile);
        }

        #endregion
    }
}


