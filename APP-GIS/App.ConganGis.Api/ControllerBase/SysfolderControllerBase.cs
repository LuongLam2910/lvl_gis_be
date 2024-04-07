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
    public class SysfolderControllerBase : ControllerBase
    {

        private readonly SysfolderManagerBase _managerBase;
        private const string SysfolderCacheKey = "SysfolderKey";
        private IMemoryCache _cache;
        private ILogger<SysfolderControllerBase> _logger;
        public SysfolderControllerBase(IMemoryCache cache, ILogger<SysfolderControllerBase> logger)
        {
            _managerBase = new SysfolderManagerBase();
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        #region Base function 
        [HttpPost]
        [Route("PostPagingSysfolder")]
        public virtual async Task<ApiResponse<PageModelView<IEnumerable<SysfolderVM.ItemSysfolder>>>> PostPagingSysfolder([FromBody] SysfolderVM.PageSysfolder _model)
        {
            _logger.Log(LogLevel.Information, "PostPagingSysfolder from cache.");
            if (_cache.TryGetValue(SysfolderCacheKey + "_" + _model.strKey, out ApiResponse<PageModelView<IEnumerable<SysfolderVM.ItemSysfolder>>> toReturn))
            {
                _logger.Log(LogLevel.Information, "PostPagingSysfolder find cache.");
            }
            else
            {
                try
                {
                    _logger.Log(LogLevel.Information, "PostPagingSysfolder not fount cache. PostPagingSysfolder from database.");
                    toReturn = await _managerBase.SelectPaging(_model);
                    var cacheEntryOptions = new MemoryCacheEntryOptions()
                            .SetSlidingExpiration(TimeSpan.FromSeconds(60))
                            .SetAbsoluteExpiration(TimeSpan.FromSeconds(3600))
                            .SetPriority(CacheItemPriority.Normal)
                            .SetSize(1024);
                    _cache.Set(SysfolderCacheKey + "_" + _model.strKey, toReturn, cacheEntryOptions);
                }
                catch (Exception ex)
                {
                    ex.ToExceptionless().SetProperty("PostPagingSysfolder", _model.strKey).Submit();
                }
            }
            return toReturn;
        }

        [HttpPost]
        [Route("PostInsertSysfolder")]
        public virtual async Task<ApiResponse> PostInsertSysfolder([FromBody] SysfolderVM.ItemSysfolder _Model)
        {
            return await _managerBase.InsertAsync(_Model);
        }

        [HttpPut]
        [Route("PostUpdateSysfolder")]
        public virtual async Task<ApiResponse> PostUpdateSysfolder([FromBody] SysfolderVM.ItemSysfolder _Model)
        {
            return await _managerBase.UpdateAsync(_Model);
        }

        [HttpDelete]
        [Route("PostDeleteSysfolder/{_id}")]
        public virtual async Task<ApiResponse> PostDeleteSysfolder(int _id)
        {
            return await _managerBase.DeleteAsync(_id);
        }


        [HttpGet]
        [Route("GetOneSysfolder/{_id}")]
        public virtual async Task<ApiResponse<SysfolderVM.ItemSysfolder>> GetOneSysfolder(int _id)
        {
            //_logger.Log(LogLevel.Information, "GetOneSysfolder from cache.");
            //if (_cache.TryGetValue(SysfolderCacheKey + "_" + _id, out ApiResponse<SysfolderVM.ItemSysfolder> toReturn))
            //{
            //    _logger.Log(LogLevel.Information, "GetOneSysfolder find cache.");
            //}
            //else
            //{
            //    try
            //    {
            //        _logger.Log(LogLevel.Information, "GetOneSysfolder not fount cache. GetOneSysfolder from database.");
            //        toReturn = await _managerBase.SelectOneAsync(_id);
            //        var cacheEntryOptions = new MemoryCacheEntryOptions()
            //                .SetSlidingExpiration(TimeSpan.FromSeconds(60))
            //                .SetAbsoluteExpiration(TimeSpan.FromSeconds(3600))
            //                .SetPriority(CacheItemPriority.Normal)
            //                .SetSize(1024);
            //        _cache.Set(SysfolderCacheKey + "_" + _id, toReturn, cacheEntryOptions);
            //    }
            //    catch (Exception ex)
            //    {
            //        ex.ToExceptionless().SetProperty("GetOneSysfolder", _id).Submit();
            //    }
            //}
            //return toReturn;
            return await _managerBase.SelectOneAsync(_id);
        }


        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("GetBynameSysfolder")]
        public virtual async Task<ApiResponse<List<SysfolderVM.ItemSysfolder>>> GetBynameSysfolder(string _name)
        {
            return await _managerBase.SelectBynameAsync(_name);
        }

        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("GetBypathSysfolder")]
        public virtual async Task<ApiResponse<List<SysfolderVM.ItemSysfolder>>> GetBypathSysfolder(string _path)
        {
            return await _managerBase.SelectBypathAsync(_path);
        }

        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("GetBycreatebySysfolder")]
        public virtual async Task<ApiResponse<List<SysfolderVM.ItemSysfolder>>> GetBycreatebySysfolder(string _createby)
        {
            return await _managerBase.SelectBycreatebyAsync(_createby);
        }

        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("GetBycreatedateSysfolder")]
        public virtual async Task<ApiResponse<List<SysfolderVM.ItemSysfolder>>> GetBycreatedateSysfolder(DateTime? _createdate)
        {
            return await _managerBase.SelectBycreatedateAsync(_createdate);
        }

        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("GetByparentidSysfolder")]
        public virtual async Task<ApiResponse<List<SysfolderVM.ItemSysfolder>>> GetByparentidSysfolder(int? _parentid)
        {
            return await _managerBase.SelectByparentidAsync(_parentid);
        }

        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("GetByunitcodeSysfolder")]
        public virtual async Task<ApiResponse<List<SysfolderVM.ItemSysfolder>>> GetByunitcodeSysfolder(string _unitcode)
        {
            return await _managerBase.SelectByunitcodeAsync(_unitcode);
        }

        #endregion
    }
}


