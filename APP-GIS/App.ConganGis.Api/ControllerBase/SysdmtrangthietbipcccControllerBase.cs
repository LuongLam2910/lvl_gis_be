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
    public class SysdmtrangthietbipcccControllerBase : ControllerBase
    {

        private readonly SysdmtrangthietbipcccManagerBase _managerBase;
        private const string SysdmtrangthietbipcccCacheKey = "SysdmtrangthietbipcccKey";
        private IMemoryCache _cache;
        private ILogger<SysdmtrangthietbipcccControllerBase> _logger;
        public SysdmtrangthietbipcccControllerBase(IMemoryCache cache, ILogger<SysdmtrangthietbipcccControllerBase> logger)
        {
            _managerBase = new SysdmtrangthietbipcccManagerBase();
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        #region Base function 
        [HttpPost]
        [Route("PostPagingSysdmtrangthietbipccc")]
        public virtual async Task<ApiResponse<PageModelView<IEnumerable<SysdmtrangthietbipcccVM.ItemSysdmtrangthietbipccc>>>> PostPagingSysdmtrangthietbipccc([FromBody] SysdmtrangthietbipcccVM.PageSysdmtrangthietbipccc _model)
        {
            return await _managerBase.SelectPaging(_model);
            //_logger.Log(LogLevel.Information, "PostPagingSysdmtrangthietbipccc from cache.");
            //if (_cache.TryGetValue(SysdmtrangthietbipcccCacheKey + "_" + _model.strKey, out ApiResponse<PageModelView<IEnumerable<SysdmtrangthietbipcccVM.ItemSysdmtrangthietbipccc>>> toReturn))
            //{
            //    _logger.Log(LogLevel.Information, "PostPagingSysdmtrangthietbipccc find cache.");
            //}
            //else
            //{
            //    try
            //    {
            //        _logger.Log(LogLevel.Information, "PostPagingSysdmtrangthietbipccc not fount cache. PostPagingSysdmtrangthietbipccc from database.");
            //        toReturn = await _managerBase.SelectPaging(_model);
            //        var cacheEntryOptions = new MemoryCacheEntryOptions()
            //                .SetSlidingExpiration(TimeSpan.FromSeconds(60))
            //                .SetAbsoluteExpiration(TimeSpan.FromSeconds(3600))
            //                .SetPriority(CacheItemPriority.Normal)
            //                .SetSize(1024);
            //        _cache.Set(SysdmtrangthietbipcccCacheKey + "_" + _model.strKey, toReturn, cacheEntryOptions);
            //    }
            //    catch (Exception ex)
            //    {
            //        ex.ToExceptionless().SetProperty("PostPagingSysdmtrangthietbipccc", _model.strKey).Submit();
            //    }
            //}
            //return toReturn;
        }

        [HttpPost]
        [Route("PostInsertSysdmtrangthietbipccc")]
        public virtual async Task<ApiResponse> PostInsertSysdmtrangthietbipccc([FromBody] SysdmtrangthietbipcccVM.ItemSysdmtrangthietbipccc _Model)
        {
            return await _managerBase.InsertAsync(_Model);
        }

        [HttpPut]
        [Route("PostUpdateSysdmtrangthietbipccc")]
        public virtual async Task<ApiResponse> PostUpdateSysdmtrangthietbipccc([FromBody] SysdmtrangthietbipcccVM.ItemSysdmtrangthietbipccc _Model)
        {
            return await _managerBase.UpdateAsync(_Model);
        }

        [HttpDelete]
        [Route("PostDeleteSysdmtrangthietbipccc/{_id}")]
        public virtual async Task<ApiResponse> PostDeleteSysdmtrangthietbipccc(int _id)
        {
            return await _managerBase.DeleteAsync(_id);
        }


        [HttpGet]
        [Route("GetOneSysdmtrangthietbipccc/{_id}")]
        public virtual async Task<ApiResponse<SysdmtrangthietbipcccVM.ItemSysdmtrangthietbipccc>> GetOneSysdmtrangthietbipccc(int _id)
        {
            return await _managerBase.SelectOneAsync(_id);
            //_logger.Log(LogLevel.Information, "GetOneSysdmtrangthietbipccc from cache.");
            //if (_cache.TryGetValue(SysdmtrangthietbipcccCacheKey + "_" + _id, out ApiResponse<SysdmtrangthietbipcccVM.ItemSysdmtrangthietbipccc> toReturn))
            //{
            //    _logger.Log(LogLevel.Information, "GetOneSysdmtrangthietbipccc find cache.");
            //}
            //else
            //{
            //    try
            //    {
            //        _logger.Log(LogLevel.Information, "GetOneSysdmtrangthietbipccc not fount cache. GetOneSysdmtrangthietbipccc from database.");
            //        toReturn = await _managerBase.SelectOneAsync(_id);
            //        var cacheEntryOptions = new MemoryCacheEntryOptions()
            //                .SetSlidingExpiration(TimeSpan.FromSeconds(60))
            //                .SetAbsoluteExpiration(TimeSpan.FromSeconds(3600))
            //                .SetPriority(CacheItemPriority.Normal)
            //                .SetSize(1024);
            //        _cache.Set(SysdmtrangthietbipcccCacheKey + "_" + _id, toReturn, cacheEntryOptions);
            //    }
            //    catch (Exception ex)
            //    {
            //        ex.ToExceptionless().SetProperty("GetOneSysdmtrangthietbipccc", _id).Submit();
            //    }
            //}
            //return toReturn;
        }


        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("GetBytenthietbiSysdmtrangthietbipccc")]
        public virtual async Task<ApiResponse<List<SysdmtrangthietbipcccVM.ItemSysdmtrangthietbipccc>>> GetBytenthietbiSysdmtrangthietbipccc(string _tenthietbi)
        {
            return await _managerBase.SelectBytenthietbiAsync(_tenthietbi);
        }

        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("GetBymachaSysdmtrangthietbipccc")]
        public virtual async Task<ApiResponse<List<SysdmtrangthietbipcccVM.ItemSysdmtrangthietbipccc>>> GetBymachaSysdmtrangthietbipccc(int _macha)
        {
            return await _managerBase.SelectBymachaAsync(_macha);
        }

        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("GetBymotaSysdmtrangthietbipccc")]
        public virtual async Task<ApiResponse<List<SysdmtrangthietbipcccVM.ItemSysdmtrangthietbipccc>>> GetBymotaSysdmtrangthietbipccc(string _mota)
        {
            return await _managerBase.SelectBymotaAsync(_mota);
        }

        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("GetByloaithietbiSysdmtrangthietbipccc")]
        public virtual async Task<ApiResponse<List<SysdmtrangthietbipcccVM.ItemSysdmtrangthietbipccc>>> GetByloaithietbiSysdmtrangthietbipccc(int _loaithietbi)
        {
            return await _managerBase.SelectByloaithietbiAsync(_loaithietbi);
        }

        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("GetBytrangthaiSysdmtrangthietbipccc")]
        public virtual async Task<ApiResponse<List<SysdmtrangthietbipcccVM.ItemSysdmtrangthietbipccc>>> GetBytrangthaiSysdmtrangthietbipccc(int _trangthai)
        {
            return await _managerBase.SelectBytrangthaiAsync(_trangthai);
        }

        #endregion
    }
}


