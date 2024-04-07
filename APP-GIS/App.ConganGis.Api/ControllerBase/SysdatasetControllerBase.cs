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
    public class SysdatasetControllerBase : ControllerBase
    {

        private readonly SysdatasetManagerBase _managerBase;
        private const string SysdatasetCacheKey = "SysdatasetKey";
        private IMemoryCache _cache;
        private ILogger<SysdatasetControllerBase> _logger;
        public SysdatasetControllerBase(IMemoryCache cache, ILogger<SysdatasetControllerBase> logger)
        {
            _managerBase = new SysdatasetManagerBase();
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        #region Base function 
        [HttpPost]
        [Route("PostPagingSysdataset")]
        public virtual async Task<ApiResponse<PageModelView<IEnumerable<SysdatasetVM.ItemSysdataset>>>> PostPagingSysdataset([FromBody] SysdatasetVM.PageSysdataset _model)
        {
            /* _logger.Log(LogLevel.Information, "PostPagingSysdataset from cache.");
            if (_cache.TryGetValue(SysdatasetCacheKey + "_" + _model.strKey, out ApiResponse<PageModelView<IEnumerable<SysdatasetVM.ItemSysdataset>>> toReturn))
            {
                _logger.Log(LogLevel.Information, "PostPagingSysdataset find cache.");
            }
            else
            {
                try
                {
                    _logger.Log(LogLevel.Information, "PostPagingSysdataset not fount cache. PostPagingSysdataset from database.");
                    toReturn = await _managerBase.SelectPaging(_model);
                    var cacheEntryOptions = new MemoryCacheEntryOptions()
                            .SetSlidingExpiration(TimeSpan.FromSeconds(60))
                            .SetAbsoluteExpiration(TimeSpan.FromSeconds(3600))
                            .SetPriority(CacheItemPriority.Normal)
                            .SetSize(1024);
                    _cache.Set(SysdatasetCacheKey + "_" + _model.strKey, toReturn, cacheEntryOptions);
                }
                catch (Exception ex)
                {
                    ex.ToExceptionless().SetProperty("PostPagingSysdataset", _model.strKey).Submit();
                }
            }
            return toReturn;*/
            return await _managerBase.SelectPaging(_model);
        }

        [HttpPost]
        [Route("PostInsertSysdataset")]
        public virtual async Task<ApiResponse> PostInsertSysdataset([FromBody] SysdatasetVM.ItemSysdataset _Model)
        {
            return await _managerBase.InsertAsync(_Model);
        }

        [HttpPut]
        [Route("PostUpdateSysdataset")]
        public virtual async Task<ApiResponse> PostUpdateSysdataset([FromBody] SysdatasetVM.ItemSysdataset _Model)
        {
            return await _managerBase.UpdateAsync(_Model);
        }

        [HttpDelete]
        [Route("PostDeleteSysdataset/{_id}")]
        public virtual async Task<ApiResponse> PostDeleteSysdataset(int _id)
        {
            return await _managerBase.DeleteAsync(_id);
        }


        [HttpGet]
        [Route("GetOneSysdataset/{_id}")]
        public virtual async Task<ApiResponse<SysdatasetVM.ItemSysdataset>> GetOneSysdataset(int _id)
        {
            //_logger.Log(LogLevel.Information, "GetOneSysdataset from cache.");
            //if (_cache.TryGetValue(SysdatasetCacheKey + "_" + _id, out ApiResponse<SysdatasetVM.ItemSysdataset> toReturn))
            //{
            //    _logger.Log(LogLevel.Information, "GetOneSysdataset find cache.");
            //}
            //else
            //{
            //    try
            //    {
            //        _logger.Log(LogLevel.Information, "GetOneSysdataset not fount cache. GetOneSysdataset from database.");
            //        toReturn = await _managerBase.SelectOneAsync(_id);
            //        var cacheEntryOptions = new MemoryCacheEntryOptions()
            //                .SetSlidingExpiration(TimeSpan.FromSeconds(60))
            //                .SetAbsoluteExpiration(TimeSpan.FromSeconds(3600))
            //                .SetPriority(CacheItemPriority.Normal)
            //                .SetSize(1024);
            //        _cache.Set(SysdatasetCacheKey + "_" + _id, toReturn, cacheEntryOptions);
            //    }
            //    catch (Exception ex)
            //    {
            //        ex.ToExceptionless().SetProperty("GetOneSysdataset", _id).Submit();
            //    }
            //}
            return await _managerBase.SelectOneAsync(_id);
        }


        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("GetBynameSysdataset")]
        public virtual async Task<ApiResponse<List<SysdatasetVM.ItemSysdataset>>> GetBynameSysdataset(string _name)
        {
            return await _managerBase.SelectBynameAsync(_name);
        }

        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("GetByparentidSysdataset")]
        public virtual async Task<ApiResponse<List<SysdatasetVM.ItemSysdataset>>> GetByparentidSysdataset(int? _parentid)
        {
            return await _managerBase.SelectByparentidAsync(_parentid);
        }


        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("GetByusercreatedSysdataset")]
        public virtual async Task<ApiResponse<List<SysdatasetVM.ItemSysdataset>>> GetByusercreatedSysdataset(int? _usercreated)
        {
            return await _managerBase.SelectByusercreatedAsync(_usercreated);
        }

        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("GetBydatecreatedSysdataset")]
        public virtual async Task<ApiResponse<List<SysdatasetVM.ItemSysdataset>>> GetBydatecreatedSysdataset(DateTime? _datecreated)
        {
            return await _managerBase.SelectBydatecreatedAsync(_datecreated);
        }

        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("GetBydatemodifiedSysdataset")]
        public virtual async Task<ApiResponse<List<SysdatasetVM.ItemSysdataset>>> GetBydatemodifiedSysdataset(DateTime? _datemodified)
        {
            return await _managerBase.SelectBydatemodifiedAsync(_datemodified);
        }

        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("GetByusermodifiedSysdataset")]
        public virtual async Task<ApiResponse<List<SysdatasetVM.ItemSysdataset>>> GetByusermodifiedSysdataset(int? _usermodified)
        {
            return await _managerBase.SelectByusermodifiedAsync(_usermodified);
        }

        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("GetBydescriptionSysdataset")]
        public virtual async Task<ApiResponse<List<SysdatasetVM.ItemSysdataset>>> GetBydescriptionSysdataset(string _description)
        {
            return await _managerBase.SelectBydescriptionAsync(_description);
        }

        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("GetByunitcodeSysdataset")]
        public virtual async Task<ApiResponse<List<SysdatasetVM.ItemSysdataset>>> GetByunitcodeSysdataset(string _unitcode)
        {
            return await _managerBase.SelectByunitcodeAsync(_unitcode);
        }

        #endregion
    }
}


