using App.CepLog.Dal.EntityClasses;
using App.Core.Common;
using App.Qtht.Dal.EntityClasses;
using App.Qtht.Services.Manager;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SD.LLBLGen.Pro.ORMSupportClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Qtht.Api.Controllers
{
    [Authorize]
    [Route("App/Qtht/api/[controller]")]
    public class CepLogController : Controller
    {
        private readonly IConfigEmailManager _configEmailManager;
        private readonly ICEPManager _cepManager;
        

        public CepLogController(IConfigEmailManager configEmailManager, ICEPManager cepManager)
        {
            _cepManager = cepManager;
            _configEmailManager = configEmailManager;
        }


        [HttpPost]
        [Route("ConfigEmai")]
        public Task<ApiResponse> ConfigEmai([FromBody] ConfigemailEntity model)
        {
            return _configEmailManager.UpdateConfig(model);
        }

        [HttpPost]
        [Route("TestConnect")]
        public Task<ApiResponse> TestConnect([FromBody] ConfigemailEntity model)
        {
            return _configEmailManager.TestConnect(model);
        }


        [HttpGet]
        [Route("GetConfig")]
        public Task<ApiResponse<ConfigemailEntity>> GetConfig()
        {
            return _configEmailManager.GetConfig();
        }

        [HttpPost]
        [Route("Paging")]
        public Task<ApiResponse<PageModelView<IEnumerable<object>>>> Paging([FromBody]PageModel model) {
            return _cepManager.Paging(model, SetSelectModelPaging(), SetFieldConditionPaging(), SetSortForPaging(), SetFilterPaging());
        }



        protected virtual SortClause SetSortForPaging()
        {
            EntityField2 fieldSort = SetFieldKeyUpdateOrDelete();
            if (!Object.Equals(fieldSort, null))
            {
                return fieldSort | SortOperator.Descending;
            }
            return null;
        }
        protected virtual EntityField2 SetFieldKeyUpdateOrDelete()
        {
            return null;
        }

        protected virtual EntityField2 SetFieldConditionPaging()
        {
            return null;
        }

        protected virtual Func<ApimlogmessageEntity, object> SetSelectModelPaging()
        {
            return null;
        }

        protected virtual EntityField2[] SetFilterPaging()
        {
            return null;
        }
    }
}
