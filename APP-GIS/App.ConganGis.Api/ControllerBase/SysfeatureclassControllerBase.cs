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
    public class SysfeatureclassControllerBase : ControllerBase
    {

        private readonly SysfeatureclassManagerBase _managerBase;
        private const string SysfeatureclasCacheKey = "SysfeatureclasKey";
        private IMemoryCache _cache;
        private ILogger<SysfeatureclassControllerBase> _logger;
        public SysfeatureclassControllerBase(IMemoryCache cache, ILogger<SysfeatureclassControllerBase> logger)
        {
            _managerBase = new SysfeatureclassManagerBase();
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        #region Base function 
        [HttpPost]
        [Route("PostPagingSysfeatureclas")]
        public virtual async Task<ApiResponse<PageModelView<IEnumerable<SysfeatureclassVM.ItemSysfeatureclass>>>> PostPagingSysfeatureclas([FromBody] SysfeatureclassVM.PageSysfeatureclass _model)
        {
            /*
            _logger.Log(LogLevel.Information, "PostPagingSysfeatureclas from cache.");
            if (_cache.TryGetValue(SysfeatureclasCacheKey + "_" + _model.strKey, out ApiResponse<PageModelView<IEnumerable<SysfeatureclassVM.ItemSysfeatureclass>>> toReturn))
            {
                _logger.Log(LogLevel.Information, "PostPagingSysfeatureclas find cache.");
            }
            else
            {
                try
                {
                    _logger.Log(LogLevel.Information, "PostPagingSysfeatureclas not fount cache. PostPagingSysfeatureclas from database.");
                    toReturn = await _managerBase.SelectPaging(_model);
                    var cacheEntryOptions = new MemoryCacheEntryOptions()
                            .SetSlidingExpiration(TimeSpan.FromSeconds(60))
                            .SetAbsoluteExpiration(TimeSpan.FromSeconds(3600))
                            .SetPriority(CacheItemPriority.Normal)
                            .SetSize(1024);
                    _cache.Set(SysfeatureclasCacheKey + "_" + _model.strKey, toReturn, cacheEntryOptions);
                }
                catch (Exception ex)
                {
                    ex.ToExceptionless().SetProperty("PostPagingSysfeatureclas", _model.strKey).Submit();
                }
            }
            return toReturn;*/
            return await _managerBase.SelectPaging(_model);
        }

        [HttpPost]
        [Route("PostInsertSysfeatureclas")]
        public virtual async Task<ApiResponse> PostInsertSysfeatureclas([FromBody] SysfeatureclassVM.ItemSysfeatureclass _Model)
        {
            return await _managerBase.InsertAsync(_Model);
        }

        [HttpPut]
        [Route("PostUpdateSysfeatureclas")]
        public virtual async Task<ApiResponse> PostUpdateSysfeatureclas([FromBody] SysfeatureclassVM.ItemSysfeatureclass _Model)
        {
            return await _managerBase.UpdateAsync(_Model);
        }

        [HttpDelete]
        [Route("PostDeleteSysfeatureclas/{_id}")]
        public virtual async Task<ApiResponse> PostDeleteSysfeatureclas(int _id)
        {
            return await _managerBase.DeleteAsync(_id);
        }


        [HttpGet]
        [Route("GetOneSysfeatureclas/{_id}")]
        public virtual async Task<ApiResponse<SysfeatureclassVM.ItemSysfeatureclass>> GetOneSysfeatureclas(int _id)
        {
            /*
            _logger.Log(LogLevel.Information, "GetOneSysfeatureclas from cache.");
            if (_cache.TryGetValue(SysfeatureclasCacheKey + "_" + _id, out ApiResponse<SysfeatureclassVM.ItemSysfeatureclass> toReturn))
            {
                _logger.Log(LogLevel.Information, "GetOneSysfeatureclas find cache.");
            }
            else
            {
                try
                {
                    _logger.Log(LogLevel.Information, "GetOneSysfeatureclas not fount cache. GetOneSysfeatureclas from database.");
                    toReturn = await _managerBase.SelectOneAsync(_id);
                    var cacheEntryOptions = new MemoryCacheEntryOptions()
                            .SetSlidingExpiration(TimeSpan.FromSeconds(60))
                            .SetAbsoluteExpiration(TimeSpan.FromSeconds(3600))
                            .SetPriority(CacheItemPriority.Normal)
                            .SetSize(1024);
                    _cache.Set(SysfeatureclasCacheKey + "_" + _id, toReturn, cacheEntryOptions);
                }
                catch (Exception ex)
                {
                    ex.ToExceptionless().SetProperty("GetOneSysfeatureclas", _id).Submit();
                }
            }
            return toReturn;*/
            return await _managerBase.SelectOneAsync(_id);
        }


        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = false)]
        [Route("GetBynameSysfeatureclas")]
        public virtual async Task<ApiResponse<List<SysfeatureclassVM.ItemSysfeatureclass>>> GetBynameSysfeatureclas(string _name)
        {
            return await _managerBase.SelectBynameAsync(_name);
        }

        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("GetBytablenameSysfeatureclas")]
        public virtual async Task<ApiResponse<List<SysfeatureclassVM.ItemSysfeatureclass>>> GetBytablenameSysfeatureclas(string _tablename)
        {
            return await _managerBase.SelectBytablenameAsync(_tablename);
        }

        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = false)]
        [Route("GetBydatasetidSysfeatureclas")]
        public virtual async Task<ApiResponse<List<SysfeatureclassVM.ItemSysfeatureclass>>> GetBydatasetidSysfeatureclas(int? _datasetid)
        {
            return await _managerBase.SelectBydatasetidAsync(_datasetid);
        }

        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("GetByconfigSysfeatureclas")]
        public virtual async Task<ApiResponse<List<SysfeatureclassVM.ItemSysfeatureclass>>> GetByconfigSysfeatureclas(int? _config)
        {
            return await _managerBase.SelectByconfigAsync(_config);
        }

        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("GetByusercreatedSysfeatureclas")]
        public virtual async Task<ApiResponse<List<SysfeatureclassVM.ItemSysfeatureclass>>> GetByusercreatedSysfeatureclas(int? _usercreated)
        {
            return await _managerBase.SelectByusercreatedAsync(_usercreated);
        }

        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("GetBydatacreatedSysfeatureclas")]
        public virtual async Task<ApiResponse<List<SysfeatureclassVM.ItemSysfeatureclass>>> GetBydatacreatedSysfeatureclas(DateTime? _datacreated)
        {
            return await _managerBase.SelectBydatacreatedAsync(_datacreated);
        }

        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("GetBydatemodifiedSysfeatureclas")]
        public virtual async Task<ApiResponse<List<SysfeatureclassVM.ItemSysfeatureclass>>> GetBydatemodifiedSysfeatureclas(DateTime? _datemodified)
        {
            return await _managerBase.SelectBydatemodifiedAsync(_datemodified);
        }

        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("GetByusermodifiedSysfeatureclas")]
        public virtual async Task<ApiResponse<List<SysfeatureclassVM.ItemSysfeatureclass>>> GetByusermodifiedSysfeatureclas(int? _usermodified)
        {
            return await _managerBase.SelectByusermodifiedAsync(_usermodified);
        }

        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("GetBydescriptionSysfeatureclas")]
        public virtual async Task<ApiResponse<List<SysfeatureclassVM.ItemSysfeatureclass>>> GetBydescriptionSysfeatureclas(string _description)
        {
            return await _managerBase.SelectBydescriptionAsync(_description);
        }

        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("GetByprjSysfeatureclas")]
        public virtual async Task<ApiResponse<List<SysfeatureclassVM.ItemSysfeatureclass>>> GetByprjSysfeatureclas(string _prj)
        {
            return await _managerBase.SelectByprjAsync(_prj);
        }

        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("GetBygeotypeSysfeatureclas")]
        public virtual async Task<ApiResponse<List<SysfeatureclassVM.ItemSysfeatureclass>>> GetBygeotypeSysfeatureclas(string _geotype)
        {
            return await _managerBase.SelectBygeotypeAsync(_geotype);
        }

        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("GetBystatusSysfeatureclas")]
        public virtual async Task<ApiResponse<List<SysfeatureclassVM.ItemSysfeatureclass>>> GetBystatusSysfeatureclas(int? _status)
        {
            return await _managerBase.SelectBystatusAsync(_status);
        }

        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("GetByunitcodeSysfeatureclas")]
        public virtual async Task<ApiResponse<List<SysfeatureclassVM.ItemSysfeatureclass>>> GetByunitcodeSysfeatureclas(string _unitcode)
        {
            return await _managerBase.SelectByunitcodeAsync(_unitcode);
        }

        #endregion
    }
}


