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
    public class SysmapUserControllerBase : ControllerBase
    {

        private readonly SysmapUserManagerBase _managerBase;
        private const string SysmapUserCacheKey = "SysmapUserKey";
        private IMemoryCache _cache;
        private ILogger<SysmapUserControllerBase> _logger;
        public SysmapUserControllerBase(IMemoryCache cache, ILogger<SysmapUserControllerBase> logger)
        {
            _managerBase = new SysmapUserManagerBase();
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        #region Base function 
        [HttpPost]
        [Route("PostPagingSysmapUser")]
        public virtual async Task<ApiResponse<PageModelView<IEnumerable<SysmapUserVM.ItemSysmapUser>>>> PostPagingSysmapUser([FromBody] SysmapUserVM.PageSysmapUser _model)
        {
            _logger.Log(LogLevel.Information, "PostPagingSysmapUser from cache.");
            if (_cache.TryGetValue(SysmapUserCacheKey + "_" + _model.strKey, out ApiResponse<PageModelView<IEnumerable<SysmapUserVM.ItemSysmapUser>>> toReturn))
            {
                _logger.Log(LogLevel.Information, "PostPagingSysmapUser find cache.");
            }
            else
            {
                try
                {
                    _logger.Log(LogLevel.Information, "PostPagingSysmapUser not fount cache. PostPagingSysmapUser from database.");
                    toReturn = await _managerBase.SelectPaging(_model);
                    var cacheEntryOptions = new MemoryCacheEntryOptions()
                            .SetSlidingExpiration(TimeSpan.FromSeconds(60))
                            .SetAbsoluteExpiration(TimeSpan.FromSeconds(3600))
                            .SetPriority(CacheItemPriority.Normal)
                            .SetSize(1024);
                    _cache.Set(SysmapUserCacheKey + "_" + _model.strKey, toReturn, cacheEntryOptions);
                }
                catch (Exception ex)
                {
                    ex.ToExceptionless().SetProperty("PostPagingSysmapUser", _model.strKey).Submit();
                }
            }
            return toReturn;
        }

        [HttpPost]
        [Route("PostInsertSysmapUser")]
        public virtual async Task<ApiResponse> PostInsertSysmapUser([FromBody] SysmapUserVM.ItemSysmapUser _Model)
        {
            return await _managerBase.InsertAsync(_Model);
        }

        [HttpPut]
        [Route("PostUpdateSysmapUser")]
        public virtual async Task<ApiResponse> PostUpdateSysmapUser([FromBody] SysmapUserVM.ItemSysmapUser _Model)
        {
            return await _managerBase.UpdateAsync(_Model);
        }

        [HttpDelete]
        [Route("PostDeleteSysmapUser/{_id}")]
        public virtual async Task<ApiResponse> PostDeleteSysmapUser(int _id)
        {
            return await _managerBase.DeleteAsync(_id);
        }


        [HttpGet]
        [Route("GetOneSysmapUser/{_id}")]
        public virtual async Task<ApiResponse<SysmapUserVM.ItemSysmapUser>> GetOneSysmapUser(int _id)
        {
            _logger.Log(LogLevel.Information, "GetOneSysmapUser from cache.");
            if (_cache.TryGetValue(SysmapUserCacheKey + "_" + _id, out ApiResponse<SysmapUserVM.ItemSysmapUser> toReturn))
            {
                _logger.Log(LogLevel.Information, "GetOneSysmapUser find cache.");
            }
            else
            {
                try
                {
                    _logger.Log(LogLevel.Information, "GetOneSysmapUser not fount cache. GetOneSysmapUser from database.");
                    toReturn = await _managerBase.SelectOneAsync(_id);
                    var cacheEntryOptions = new MemoryCacheEntryOptions()
                            .SetSlidingExpiration(TimeSpan.FromSeconds(60))
                            .SetAbsoluteExpiration(TimeSpan.FromSeconds(3600))
                            .SetPriority(CacheItemPriority.Normal)
                            .SetSize(1024);
                    _cache.Set(SysmapUserCacheKey + "_" + _id, toReturn, cacheEntryOptions);
                }
                catch (Exception ex)
                {
                    ex.ToExceptionless().SetProperty("GetOneSysmapUser", _id).Submit();
                }
            }
            return toReturn;
        }


        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("GetBymapidSysmapUser")]
        public virtual async Task<ApiResponse<List<SysmapUserVM.ItemSysmapUser>>> GetBymapidSysmapUser(int? _mapid)
        {
            return await _managerBase.SelectBymapidAsync(_mapid);
        }

        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("GetByusernameSysmapUser")]
        public virtual async Task<ApiResponse<List<SysmapUserVM.ItemSysmapUser>>> GetByusernameSysmapUser(string _username)
        {
            return await _managerBase.SelectByusernameAsync(_username);
        }

        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("GetByconfigSysmapUser")]
        public virtual async Task<ApiResponse<List<SysmapUserVM.ItemSysmapUser>>> GetByconfigSysmapUser(int? _config)
        {
            return await _managerBase.SelectByconfigAsync(_config);
        }

        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("GetByusercreatedSysmapUser")]
        public virtual async Task<ApiResponse<List<SysmapUserVM.ItemSysmapUser>>> GetByusercreatedSysmapUser(int? _usercreated)
        {
            return await _managerBase.SelectByusercreatedAsync(_usercreated);
        }

        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("GetBydatecreatedSysmapUser")]
        public virtual async Task<ApiResponse<List<SysmapUserVM.ItemSysmapUser>>> GetBydatecreatedSysmapUser(DateTime? _datecreated)
        {
            return await _managerBase.SelectBydatecreatedAsync(_datecreated);
        }

        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("GetBydatemodifiedSysmapUser")]
        public virtual async Task<ApiResponse<List<SysmapUserVM.ItemSysmapUser>>> GetBydatemodifiedSysmapUser(DateTime? _datemodified)
        {
            return await _managerBase.SelectBydatemodifiedAsync(_datemodified);
        }

        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("GetBygroupidSysmapUser")]
        public virtual async Task<ApiResponse<List<SysmapUserVM.ItemSysmapUser>>> GetBygroupidSysmapUser(int? _groupid)
        {
            return await _managerBase.SelectBygroupidAsync(_groupid);
        }

        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("GetBypermissionSysmapUser")]
        public virtual async Task<ApiResponse<List<SysmapUserVM.ItemSysmapUser>>> GetBypermissionSysmapUser(string _permission)
        {
            return await _managerBase.SelectBypermissionAsync(_permission);
        }

        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("GetByuseridSysmapUser")]
        public virtual async Task<ApiResponse<List<SysmapUserVM.ItemSysmapUser>>> GetByuseridSysmapUser(int? _userid)
        {
            return await _managerBase.SelectByuseridAsync(_userid);
        }

        #endregion
    }
}


