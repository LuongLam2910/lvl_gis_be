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
    public class SysdatasetUserControllerBase : ControllerBase
    {

        private readonly SysdatasetUserManagerBase _managerBase;
        private const string SysdatasetUserCacheKey = "SysdatasetUserKey";
        private IMemoryCache _cache;
        private ILogger<SysdatasetUserControllerBase> _logger;
        public SysdatasetUserControllerBase(IMemoryCache cache, ILogger<SysdatasetUserControllerBase> logger)
        {
            _managerBase = new SysdatasetUserManagerBase();
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        #region Base function 
        [HttpPost]
        [Route("PostPagingSysdatasetUser")]
        public virtual async Task<ApiResponse<PageModelView<IEnumerable<SysdatasetUserVM.ItemSysdatasetUser>>>> PostPagingSysdatasetUser([FromBody] SysdatasetUserVM.PageSysdatasetUser _model)
        {
            _logger.Log(LogLevel.Information, "PostPagingSysdatasetUser from cache.");
            if (_cache.TryGetValue(SysdatasetUserCacheKey + "_" + _model.strKey, out ApiResponse<PageModelView<IEnumerable<SysdatasetUserVM.ItemSysdatasetUser>>> toReturn))
            {
                _logger.Log(LogLevel.Information, "PostPagingSysdatasetUser find cache.");
            }
            else
            {
                try
                {
                    _logger.Log(LogLevel.Information, "PostPagingSysdatasetUser not fount cache. PostPagingSysdatasetUser from database.");
                    toReturn = await _managerBase.SelectPaging(_model);
                    var cacheEntryOptions = new MemoryCacheEntryOptions()
                            .SetSlidingExpiration(TimeSpan.FromSeconds(60))
                            .SetAbsoluteExpiration(TimeSpan.FromSeconds(3600))
                            .SetPriority(CacheItemPriority.Normal)
                            .SetSize(1024);
                    _cache.Set(SysdatasetUserCacheKey + "_" + _model.strKey, toReturn, cacheEntryOptions);
                }
                catch (Exception ex)
                {
                    ex.ToExceptionless().SetProperty("PostPagingSysdatasetUser", _model.strKey).Submit();
                }
            }
            return toReturn;
        }

        [HttpPost]
        [Route("PostInsertSysdatasetUser")]
        public virtual async Task<ApiResponse> PostInsertSysdatasetUser([FromBody] SysdatasetUserVM.ItemSysdatasetUser _Model)
        {
            return await _managerBase.InsertAsync(_Model);
        }

        [HttpPut]
        [Route("PostUpdateSysdatasetUser")]
        public virtual async Task<ApiResponse> PostUpdateSysdatasetUser([FromBody] SysdatasetUserVM.ItemSysdatasetUser _Model)
        {
            return await _managerBase.UpdateAsync(_Model);
        }

        [HttpDelete]
        [Route("PostDeleteSysdatasetUser/{_id}")]
        public virtual async Task<ApiResponse> PostDeleteSysdatasetUser(int _id)
        {
            return await _managerBase.DeleteAsync(_id);
        }


        [HttpGet]
        [Route("GetOneSysdatasetUser/{_id}")]
        public virtual async Task<ApiResponse<SysdatasetUserVM.ItemSysdatasetUser>> GetOneSysdatasetUser(int _id)
        {
            _logger.Log(LogLevel.Information, "GetOneSysdatasetUser from cache.");
            if (_cache.TryGetValue(SysdatasetUserCacheKey + "_" + _id, out ApiResponse<SysdatasetUserVM.ItemSysdatasetUser> toReturn))
            {
                _logger.Log(LogLevel.Information, "GetOneSysdatasetUser find cache.");
            }
            else
            {
                try
                {
                    _logger.Log(LogLevel.Information, "GetOneSysdatasetUser not fount cache. GetOneSysdatasetUser from database.");
                    toReturn = await _managerBase.SelectOneAsync(_id);
                    var cacheEntryOptions = new MemoryCacheEntryOptions()
                            .SetSlidingExpiration(TimeSpan.FromSeconds(60))
                            .SetAbsoluteExpiration(TimeSpan.FromSeconds(3600))
                            .SetPriority(CacheItemPriority.Normal)
                            .SetSize(1024);
                    _cache.Set(SysdatasetUserCacheKey + "_" + _id, toReturn, cacheEntryOptions);
                }
                catch (Exception ex)
                {
                    ex.ToExceptionless().SetProperty("GetOneSysdatasetUser", _id).Submit();
                }
            }
            return toReturn;
        }


        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("GetBydatasetidSysdatasetUser")]
        public virtual async Task<ApiResponse<List<SysdatasetUserVM.ItemSysdatasetUser>>> GetBydatasetidSysdatasetUser(int? _datasetid)
        {
            return await _managerBase.SelectBydatasetidAsync(_datasetid);
        }

        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("GetByusernameSysdatasetUser")]
        public virtual async Task<ApiResponse<List<SysdatasetUserVM.ItemSysdatasetUser>>> GetByusernameSysdatasetUser(string _username)
        {
            return await _managerBase.SelectByusernameAsync(_username);
        }

        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("GetByconfigSysdatasetUser")]
        public virtual async Task<ApiResponse<List<SysdatasetUserVM.ItemSysdatasetUser>>> GetByconfigSysdatasetUser(int? _config)
        {
            return await _managerBase.SelectByconfigAsync(_config);
        }

        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("GetByusercreatedSysdatasetUser")]
        public virtual async Task<ApiResponse<List<SysdatasetUserVM.ItemSysdatasetUser>>> GetByusercreatedSysdatasetUser(int? _usercreated)
        {
            return await _managerBase.SelectByusercreatedAsync(_usercreated);
        }

        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("GetBydatecreatedSysdatasetUser")]
        public virtual async Task<ApiResponse<List<SysdatasetUserVM.ItemSysdatasetUser>>> GetBydatecreatedSysdatasetUser(DateTime? _datecreated)
        {
            return await _managerBase.SelectBydatecreatedAsync(_datecreated);
        }

        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("GetBydatemodifiedSysdatasetUser")]
        public virtual async Task<ApiResponse<List<SysdatasetUserVM.ItemSysdatasetUser>>> GetBydatemodifiedSysdatasetUser(DateTime? _datemodified)
        {
            return await _managerBase.SelectBydatemodifiedAsync(_datemodified);
        }

        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("GetBygroupidSysdatasetUser")]
        public virtual async Task<ApiResponse<List<SysdatasetUserVM.ItemSysdatasetUser>>> GetBygroupidSysdatasetUser(int? _groupid)
        {
            return await _managerBase.SelectBygroupidAsync(_groupid);
        }

        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("GetBypermissionSysdatasetUser")]
        public virtual async Task<ApiResponse<List<SysdatasetUserVM.ItemSysdatasetUser>>> GetBypermissionSysdatasetUser(string _permission)
        {
            return await _managerBase.SelectBypermissionAsync(_permission);
        }

        #endregion
    }
}


