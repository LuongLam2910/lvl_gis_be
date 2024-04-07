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
    public class SysfileControllerBase : ControllerBase
    {

        private readonly SysfileManagerBase _managerBase;
        private const string SysfileCacheKey = "SysfileKey";
        private IMemoryCache _cache;
        private ILogger<SysfileControllerBase> _logger;
        public SysfileControllerBase(IMemoryCache cache, ILogger<SysfileControllerBase> logger)
        {
            _managerBase = new SysfileManagerBase();
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        #region Base function 
        [HttpPost]
        [Route("PostPagingSysfile")]
        public virtual async Task<ApiResponse<PageModelView<IEnumerable<SysfileVM.ItemSysfile>>>> PostPagingSysfile([FromBody] SysfileVM.PageSysfile _model)
        {
            _logger.Log(LogLevel.Information, "PostPagingSysfile from cache.");
            if (_cache.TryGetValue(SysfileCacheKey + "_" + _model.strKey, out ApiResponse<PageModelView<IEnumerable<SysfileVM.ItemSysfile>>> toReturn))
            {
                _logger.Log(LogLevel.Information, "PostPagingSysfile find cache.");
            }
            else
            {
                try
                {
                    _logger.Log(LogLevel.Information, "PostPagingSysfile not fount cache. PostPagingSysfile from database.");
                    toReturn = await _managerBase.SelectPaging(_model);
                    var cacheEntryOptions = new MemoryCacheEntryOptions()
                            .SetSlidingExpiration(TimeSpan.FromSeconds(60))
                            .SetAbsoluteExpiration(TimeSpan.FromSeconds(3600))
                            .SetPriority(CacheItemPriority.Normal)
                            .SetSize(1024);
                    _cache.Set(SysfileCacheKey + "_" + _model.strKey, toReturn, cacheEntryOptions);
                }
                catch (Exception ex)
                {
                    ex.ToExceptionless().SetProperty("PostPagingSysfile", _model.strKey).Submit();
                }
            }
            return toReturn;
        }

        [HttpPost]
        [Route("PostInsertSysfile")]
        public virtual async Task<ApiResponse> PostInsertSysfile([FromBody] SysfileVM.ItemSysfile _Model)
        {
            return await _managerBase.InsertAsync(_Model);
        }

        [HttpPut]
        [Route("PostUpdateSysfile")]
        public virtual async Task<ApiResponse> PostUpdateSysfile([FromBody] SysfileVM.ItemSysfile _Model)
        {
            return await _managerBase.UpdateAsync(_Model);
        }

        [HttpDelete]
        [Route("PostDeleteSysfile/{_id}")]
        public virtual async Task<ApiResponse> PostDeleteSysfile(int _id)
        {
            return await _managerBase.DeleteAsync(_id);
        }


        [HttpGet]
        [Route("GetOneSysfile/{_id}")]
        public virtual async Task<ApiResponse<SysfileVM.ItemSysfile>> GetOneSysfile(int _id)
        {
            _logger.Log(LogLevel.Information, "GetOneSysfile from cache.");
            if (_cache.TryGetValue(SysfileCacheKey + "_" + _id, out ApiResponse<SysfileVM.ItemSysfile> toReturn))
            {
                _logger.Log(LogLevel.Information, "GetOneSysfile find cache.");
            }
            else
            {
                try
                {
                    _logger.Log(LogLevel.Information, "GetOneSysfile not fount cache. GetOneSysfile from database.");
                    toReturn = await _managerBase.SelectOneAsync(_id);
                    var cacheEntryOptions = new MemoryCacheEntryOptions()
                            .SetSlidingExpiration(TimeSpan.FromSeconds(60))
                            .SetAbsoluteExpiration(TimeSpan.FromSeconds(3600))
                            .SetPriority(CacheItemPriority.Normal)
                            .SetSize(1024);
                    _cache.Set(SysfileCacheKey + "_" + _id, toReturn, cacheEntryOptions);
                }
                catch (Exception ex)
                {
                    ex.ToExceptionless().SetProperty("GetOneSysfile", _id).Submit();
                }
            }
            return toReturn;
        }


        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("GetBynameSysfile")]
        public virtual async Task<ApiResponse<List<SysfileVM.ItemSysfile>>> GetBynameSysfile(string _name)
        {
            return await _managerBase.SelectBynameAsync(_name);
        }

        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("GetByfolderSysfile")]
        public virtual async Task<ApiResponse<List<SysfileVM.ItemSysfile>>> GetByfolderSysfile(int? _folder)
        {
            return await _managerBase.SelectByfolderAsync(_folder);
        }

        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("GetBypathSysfile")]
        public virtual async Task<ApiResponse<List<SysfileVM.ItemSysfile>>> GetBypathSysfile(string _path)
        {
            return await _managerBase.SelectBypathAsync(_path);
        }

        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("GetBycreatebySysfile")]
        public virtual async Task<ApiResponse<List<SysfileVM.ItemSysfile>>> GetBycreatebySysfile(string _createby)
        {
            return await _managerBase.SelectBycreatebyAsync(_createby);
        }

        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("GetBylengthSysfile")]
        public virtual async Task<ApiResponse<List<SysfileVM.ItemSysfile>>> GetBylengthSysfile(int? _length)
        {
            return await _managerBase.SelectBylengthAsync(_length);
        }

        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("GetBytypeSysfile")]
        public virtual async Task<ApiResponse<List<SysfileVM.ItemSysfile>>> GetBytypeSysfile(int? _type)
        {
            return await _managerBase.SelectBytypeAsync(_type);
        }

        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("GetByunitcodeSysfile")]
        public virtual async Task<ApiResponse<List<SysfileVM.ItemSysfile>>> GetByunitcodeSysfile(string _unitcode)
        {
            return await _managerBase.SelectByunitcodeAsync(_unitcode);
        }

        #endregion
    }
}


