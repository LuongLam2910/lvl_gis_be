using App.Core.Common;
using App.Qtht.Services.Models;
using App.QTHTGis.Dal.EntityClasses;
using App.QTHTGis.Dal.HelperClasses;
using App.QTHTGis.Services.Manager;
using DocumentFormat.OpenXml.ExtendedProperties;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SD.LLBLGen.Pro.ORMSupportClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.QTHTGis.Api.Controllers
{
    [EnableCors("AllowOrigin")]
    [Authorize]
    [Route("App/QthtGis/api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [ApiController]
    public class SysLogActionController : DefaultController<SyslogactionEntity>
    {
        private readonly ISysLogactionManager _logactionManager;
        public SysLogActionController(ISysLogactionManager logactionManager)
        {
            _logactionManager = logactionManager;
        }
        [HttpPost]
        [Route("PagingLogin")]
        public async virtual Task<ApiResponse<PageModelView<IEnumerable<object>>>> PagingLogin([FromBody] PageModel pageModel)
        {
            return await _logactionManager.PagingLogin(pageModel, SetSelectModelPaging(), SetFieldConditionPaging(), SetSortForPaging(), SetFilterPaging());
        }
        [HttpPost]
        [Route("PagingAction")]
        public async virtual Task<ApiResponse<PageModelView<IEnumerable<object>>>> PagingAction([FromBody] PageModel pageModel)
        {
            return await _logactionManager.PagingAction(pageModel, SetSelectModelPaging(), SetFieldConditionPaging(), SetSortForPaging(), SetFilterPaging());
        }

        [HttpPost]
        [Route("PagingHistory")]
        public async virtual Task<ApiResponse<PageModelView<IEnumerable<NhatKyHoatDongModel>>>> PagingHistory([FromBody] PageModel pageModel, int key)
        {
            return await _logactionManager.PagingHistory(pageModel,key);
        }

        [HttpGet]
        [Route("HistoryLogin/{key}")]
        public async Task<ApiResponse<IEnumerable<NhatKyHoatDongModel>>> HistoryLogin([FromRoute] int key )
        {
            return await _logactionManager.HistoryLogin(key);
        }

        protected override EntityField2 SetFieldKeyUpdateOrDelete()
        {
            return SyslogactionFields.Id;
        }
        protected override EntityField2[] SetFilterPaging()
        {
            return new EntityField2[] { SyslogactionFields.Action, SyslogactionFields.Note, SyslogactionFields.UserId, SyslogactionFields.UserName };
        }
    }
}
