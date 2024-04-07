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
    public class SysfeatureclassUserControllerBase : ControllerBase
    {

        private readonly SysfeatureclassUserManagerBase _managerBase;
        private const string SysfeatureclassUserCacheKey = "SysfeatureclassUserKey";
        private IMemoryCache _cache;
        private ILogger<SysfeatureclassUserControllerBase> _logger;
        public SysfeatureclassUserControllerBase(IMemoryCache cache, ILogger<SysfeatureclassUserControllerBase> logger)
        {
            _managerBase = new SysfeatureclassUserManagerBase();
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        #region Base function 
        [HttpPost]
        [Route("PostPagingSysfeatureclassUser")]
        public virtual async Task<ApiResponse<PageModelView<IEnumerable<SysfeatureclassUserVM.ItemSysfeatureclassUser>>>> PostPagingSysfeatureclassUser([FromBody] SysfeatureclassUserVM.PageSysfeatureclassUser _model)
        {
            _logger.Log(LogLevel.Information, "PostPagingSysfeatureclassUser from cache.");
            if (_cache.TryGetValue(SysfeatureclassUserCacheKey + "_" + _model.strKey, out ApiResponse<PageModelView<IEnumerable<SysfeatureclassUserVM.ItemSysfeatureclassUser>>> toReturn))
            {
                _logger.Log(LogLevel.Information, "PostPagingSysfeatureclassUser find cache.");
            }
            else
            {
                try
                {
                    _logger.Log(LogLevel.Information, "PostPagingSysfeatureclassUser not fount cache. PostPagingSysfeatureclassUser from database.");
                    toReturn = await _managerBase.SelectPaging(_model);
                    var cacheEntryOptions = new MemoryCacheEntryOptions()
                            .SetSlidingExpiration(TimeSpan.FromSeconds(60))
                            .SetAbsoluteExpiration(TimeSpan.FromSeconds(3600))
                            .SetPriority(CacheItemPriority.Normal)
                            .SetSize(1024);
                    _cache.Set(SysfeatureclassUserCacheKey + "_" + _model.strKey, toReturn, cacheEntryOptions);
                }
                catch (Exception ex)
                {
                    ex.ToExceptionless().SetProperty("PostPagingSysfeatureclassUser", _model.strKey).Submit();
                }
            }
            return toReturn;
        }

        [HttpPost]
        [Route("PostInsertSysfeatureclassUser")]
        public virtual async Task<ApiResponse> PostInsertSysfeatureclassUser([FromBody] SysfeatureclassUserVM.ItemSysfeatureclassUser _Model)
        {
            return await _managerBase.InsertAsync(_Model);
        }

        [HttpPut]
        [Route("PostUpdateSysfeatureclassUser")]
        public virtual async Task<ApiResponse> PostUpdateSysfeatureclassUser([FromBody] SysfeatureclassUserVM.ItemSysfeatureclassUser _Model)
        {
            return await _managerBase.UpdateAsync(_Model);
        }

        [HttpDelete]
        [Route("PostDeleteSysfeatureclassUser/{_id}")]
        public virtual async Task<ApiResponse> PostDeleteSysfeatureclassUser(int _id)
        {
            return await _managerBase.DeleteAsync(_id);
        }


        [HttpGet]
        [Route("GetOneSysfeatureclassUser/{_id}")]
        public virtual async Task<ApiResponse<SysfeatureclassUserVM.ItemSysfeatureclassUser>> GetOneSysfeatureclassUser(int _id)
        {
            _logger.Log(LogLevel.Information, "GetOneSysfeatureclassUser from cache.");
            if (_cache.TryGetValue(SysfeatureclassUserCacheKey + "_" + _id, out ApiResponse<SysfeatureclassUserVM.ItemSysfeatureclassUser> toReturn))
            {
                _logger.Log(LogLevel.Information, "GetOneSysfeatureclassUser find cache.");
            }
            else
            {
                try
                {
                    _logger.Log(LogLevel.Information, "GetOneSysfeatureclassUser not fount cache. GetOneSysfeatureclassUser from database.");
                    toReturn = await _managerBase.SelectOneAsync(_id);
                    var cacheEntryOptions = new MemoryCacheEntryOptions()
                            .SetSlidingExpiration(TimeSpan.FromSeconds(60))
                            .SetAbsoluteExpiration(TimeSpan.FromSeconds(3600))
                            .SetPriority(CacheItemPriority.Normal)
                            .SetSize(1024);
                    _cache.Set(SysfeatureclassUserCacheKey + "_" + _id, toReturn, cacheEntryOptions);
                }
                catch (Exception ex)
                {
                    ex.ToExceptionless().SetProperty("GetOneSysfeatureclassUser", _id).Submit();
                }
            }
            return toReturn;
        }


        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("GetByfeatureclassidSysfeatureclassUser")]
        public virtual async Task<ApiResponse<List<SysfeatureclassUserVM.ItemSysfeatureclassUser>>> GetByfeatureclassidSysfeatureclassUser(int? _featureclassid)
        {
            return await _managerBase.SelectByfeatureclassidAsync(_featureclassid);
        }

        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("GetByusernameSysfeatureclassUser")]
        public virtual async Task<ApiResponse<List<SysfeatureclassUserVM.ItemSysfeatureclassUser>>> GetByusernameSysfeatureclassUser(object? _username)
        {
            return await _managerBase.SelectByusernameAsync(_username);
        }

        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("GetByconfigSysfeatureclassUser")]
        public virtual async Task<ApiResponse<List<SysfeatureclassUserVM.ItemSysfeatureclassUser>>> GetByconfigSysfeatureclassUser(int? _config)
        {
            return await _managerBase.SelectByconfigAsync(_config);
        }

        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("GetByusercreatedSysfeatureclassUser")]
        public virtual async Task<ApiResponse<List<SysfeatureclassUserVM.ItemSysfeatureclassUser>>> GetByusercreatedSysfeatureclassUser(int? _usercreated)
        {
            return await _managerBase.SelectByusercreatedAsync(_usercreated);
        }

        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("GetBydatecreatedSysfeatureclassUser")]
        public virtual async Task<ApiResponse<List<SysfeatureclassUserVM.ItemSysfeatureclassUser>>> GetBydatecreatedSysfeatureclassUser(DateTime? _datecreated)
        {
            return await _managerBase.SelectBydatecreatedAsync(_datecreated);
        }

        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("GetBydatemodifiedSysfeatureclassUser")]
        public virtual async Task<ApiResponse<List<SysfeatureclassUserVM.ItemSysfeatureclassUser>>> GetBydatemodifiedSysfeatureclassUser(DateTime? _datemodified)
        {
            return await _managerBase.SelectBydatemodifiedAsync(_datemodified);
        }

        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("GetBygroupidSysfeatureclassUser")]
        public virtual async Task<ApiResponse<List<SysfeatureclassUserVM.ItemSysfeatureclassUser>>> GetBygroupidSysfeatureclassUser(int? _groupid)
        {
            return await _managerBase.SelectBygroupidAsync(_groupid);
        }

        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("GetBypermissionSysfeatureclassUser")]
        public virtual async Task<ApiResponse<List<SysfeatureclassUserVM.ItemSysfeatureclassUser>>> GetBypermissionSysfeatureclassUser(string _permission)
        {
            return await _managerBase.SelectBypermissionAsync(_permission);
        }

        #endregion
    }
}


