using System;
using App.Core.Common;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.AspNetCore.Mvc;
using Exceptionless;
using App.CongAnGis.Services.ManagerBase;

namespace App.ConganGis.Api.Controllers
{
    public class SystrangthietbidoituongControllerBase : ControllerBase
    {

        private readonly SystrangthietbidoituongManagerBase _managerBase;
        private const string SystrangthietbidoituongCacheKey = "SystrangthietbidoituongKey";
        private IMemoryCache _cache;
        private ILogger<SystrangthietbidoituongControllerBase> _logger;
        public SystrangthietbidoituongControllerBase(IMemoryCache cache, ILogger<SystrangthietbidoituongControllerBase> logger)
        {
            _managerBase = new SystrangthietbidoituongManagerBase();
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        #region Base function 
        [HttpPost]
        [Route("PostPagingSystrangthietbidoituong")]
        public virtual async Task<ApiResponse<PageModelView<IEnumerable<SystrangthietbidoituongVM.ItemSystrangthietbidoituong>>>> PostPagingSystrangthietbidoituong([FromBody] SystrangthietbidoituongVM.PageSystrangthietbidoituong _model)
        {
            _logger.Log(LogLevel.Information, "PostPagingSystrangthietbidoituong from cache.");
            if (_cache.TryGetValue(SystrangthietbidoituongCacheKey + "_" + _model.strKey, out ApiResponse<PageModelView<IEnumerable<SystrangthietbidoituongVM.ItemSystrangthietbidoituong>>> toReturn))
            {
                _logger.Log(LogLevel.Information, "PostPagingSystrangthietbidoituong find cache.");
            }
            else
            {
                try
                {
                    _logger.Log(LogLevel.Information, "PostPagingSystrangthietbidoituong not fount cache. PostPagingSystrangthietbidoituong from database.");
                    toReturn = await _managerBase.SelectPaging(_model);
                    var cacheEntryOptions = new MemoryCacheEntryOptions()
                            .SetSlidingExpiration(TimeSpan.FromSeconds(60))
                            .SetAbsoluteExpiration(TimeSpan.FromSeconds(3600))
                            .SetPriority(CacheItemPriority.Normal)
                            .SetSize(1024);
                    _cache.Set(SystrangthietbidoituongCacheKey + "_" + _model.strKey, toReturn, cacheEntryOptions);
                }
                catch (Exception ex)
                {
                    ex.ToExceptionless().SetProperty("PostPagingSystrangthietbidoituong", _model.strKey).Submit();
                }
            }
            return toReturn;
        }

        [HttpPost]
        [Route("PostInsertSystrangthietbidoituong")]
        public virtual async Task<ApiResponse> PostInsertSystrangthietbidoituong([FromBody] SystrangthietbidoituongVM.ItemSystrangthietbidoituong _Model)
        {
            return await _managerBase.InsertAsync(_Model);
        }

        [HttpPut]
        [Route("PostUpdateSystrangthietbidoituong")]
        public virtual async Task<ApiResponse> PostUpdateSystrangthietbidoituong([FromBody] SystrangthietbidoituongVM.ItemSystrangthietbidoituong _Model)
        {
            return await _managerBase.UpdateAsync(_Model);
        }

        [HttpDelete]
        [Route("PostDeleteSystrangthietbidoituong/{_id}")]
        public virtual async Task<ApiResponse> PostDeleteSystrangthietbidoituong(int _id)
        {
            return await _managerBase.DeleteAsync(_id);
        }


        [HttpGet]
        [Route("GetOneSystrangthietbidoituong/{_id}")]
        public virtual async Task<ApiResponse<SystrangthietbidoituongVM.ItemSystrangthietbidoituong>> GetOneSystrangthietbidoituong(int _id)
        {
            _logger.Log(LogLevel.Information, "GetOneSystrangthietbidoituong from cache.");
            if (_cache.TryGetValue(SystrangthietbidoituongCacheKey + "_" + _id, out ApiResponse<SystrangthietbidoituongVM.ItemSystrangthietbidoituong> toReturn))
            {
                _logger.Log(LogLevel.Information, "GetOneSystrangthietbidoituong find cache.");
            }
            else
            {
                try
                {
                    _logger.Log(LogLevel.Information, "GetOneSystrangthietbidoituong not fount cache. GetOneSystrangthietbidoituong from database.");
                    toReturn = await _managerBase.SelectOneAsync(_id);
                    var cacheEntryOptions = new MemoryCacheEntryOptions()
                            .SetSlidingExpiration(TimeSpan.FromSeconds(60))
                            .SetAbsoluteExpiration(TimeSpan.FromSeconds(3600))
                            .SetPriority(CacheItemPriority.Normal)
                            .SetSize(1024);
                    _cache.Set(SystrangthietbidoituongCacheKey + "_" + _id, toReturn, cacheEntryOptions);
                }
                catch (Exception ex)
                {
                    ex.ToExceptionless().SetProperty("GetOneSystrangthietbidoituong", _id).Submit();
                }
            }
            return toReturn;
        }


        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("GetByiddoituongSystrangthietbidoituong")]
        public virtual async Task<ApiResponse<List<SystrangthietbidoituongVM.ItemSystrangthietbidoituong>>> GetByiddoituongSystrangthietbidoituong(int _iddoituong)
        {
            return await _managerBase.SelectByiddoituongAsync(_iddoituong);
        }

        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("GetByidthietbiSystrangthietbidoituong")]
        public virtual async Task<ApiResponse<List<SystrangthietbidoituongVM.ItemSystrangthietbidoituong>>> GetByidthietbiSystrangthietbidoituong(int? _idthietbi)
        {
            return await _managerBase.SelectByidthietbiAsync(_idthietbi);
        }

        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("GetBysoluongSystrangthietbidoituong")]
        public virtual async Task<ApiResponse<List<SystrangthietbidoituongVM.ItemSystrangthietbidoituong>>> GetBysoluongSystrangthietbidoituong(int? _soluong)
        {
            return await _managerBase.SelectBysoluongAsync(_soluong);
        }

        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("GetByngaytaoSystrangthietbidoituong")]
        public virtual async Task<ApiResponse<List<SystrangthietbidoituongVM.ItemSystrangthietbidoituong>>> GetByngaytaoSystrangthietbidoituong(string _ngaytao)
        {
            return await _managerBase.SelectByngaytaoAsync(_ngaytao);
        }

        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("GetByngaycapnhatSystrangthietbidoituong")]
        public virtual async Task<ApiResponse<List<SystrangthietbidoituongVM.ItemSystrangthietbidoituong>>> GetByngaycapnhatSystrangthietbidoituong(int? _ngaycapnhat)
        {
            return await _managerBase.SelectByngaycapnhatAsync(_ngaycapnhat);
        }

        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("GetBytablenameSystrangthietbidoituong")]
        public virtual async Task<ApiResponse<List<SystrangthietbidoituongVM.ItemSystrangthietbidoituong>>> GetBytablenameSystrangthietbidoituong(string _tablename)
        {
            return await _managerBase.SelectBytablenameAsync(_tablename);
        }

        #endregion
    }
}


