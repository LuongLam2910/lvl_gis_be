using App.CepLog.Dal.DatabaseSpecific;
using App.CepLog.Dal.EntityClasses;
using App.CepLog.Dal.HelperClasses;
using App.CepLog.Dal.Linq;
using App.Core.Common;
using SD.LLBLGen.Pro.LinqSupportClasses;
using SD.LLBLGen.Pro.ORMSupportClasses;
using SD.LLBLGen.Pro.QuerySpec;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Qtht.Services.Manager
{
    public interface ICEPManager
    {
        Task<ApiResponse<PageModelView<IEnumerable<object>>>> Paging(PageModel pageModel, Func<ApimlogmessageEntity, object> selectModel, EntityField2 fieldCodition = null, SortClause sortBy = null, params EntityField2[] _params);
    }
    public class CEPManager : ICEPManager
    {
        public async Task<ApiResponse<PageModelView<IEnumerable<object>>>> Paging(PageModel pageModel, Func<ApimlogmessageEntity, object> selectModel, EntityField2 fieldCodition = null, SortClause sortBy = null, params EntityField2[] _params)
        {
            try
            {
                return await Task.Run(() =>
                {
                    Predicate predicate = null;

                    if (!string.IsNullOrEmpty(pageModel.Search) && _params.Length > 0)
                    {
                        foreach (var item in _params)
                        {
                            if (item.DataType != typeof(long))
                                predicate = predicate | item.Contains(pageModel.Search.ToUpper()).CaseInsensitive();
                        }
                    }

                    if (!string.IsNullOrEmpty(pageModel.Condition))
                    {
                        predicate = (predicate) & fieldCodition == pageModel.Condition;
                    }

                    RelationPredicateBucket filter = new RelationPredicateBucket(predicate);

                    using (DataAccessAdapter adapter = new DataAccessAdapterFactory().CEPCreateAdapter())
                    {
                        var data = new EntityCollection<ApimlogmessageEntity>();
                        int totalPagesCount = 0;
                        int totalRecord = (int)adapter.GetDbCount(data, null);
                        int recordsCount = (int)adapter.GetDbCount(data, filter, null);

                        if (recordsCount <= pageModel.PageSize)
                        {
                            totalPagesCount = 1;
                            pageModel.CurrentPage = totalPagesCount;
                        }
                        else
                        {
                            int remainder = 0;
                            totalPagesCount = Math.DivRem(recordsCount, pageModel.PageSize, out remainder);
                            if (remainder > 0)
                            {
                                totalPagesCount++;
                            }
                        }

                        SortExpression sort = null;
                        if (sortBy != null)
                        {
                            sort = new SortExpression(sortBy);
                        }

                        adapter.FetchEntityCollection(data, filter, 0, sort, pageModel.CurrentPage, pageModel.PageSize);
                        return new PageModelView<IEnumerable<object>>()
                        {
                            Data = selectModel == null ? data : data.Select(selectModel),
                            CurrentPage = pageModel.CurrentPage,
                            PageSize = pageModel.PageSize,
                            TotalPage = totalPagesCount,
                            TotalRecord = totalRecord,
                            RecordsCount = recordsCount
                        };
                    }
                });
            }
            catch (ORMEntityValidationException ex)
            {
                return ApiResponse<PageModelView<IEnumerable<object>>>.Generate(null, GeneralCode.Error, ex.Message);
            }
            //using (DataAccessAdapter adapter = new DataAccessAdapterFactory().CEPCreateAdapter())
            //{
            //    var metaData = new LinqMetaData(adapter);
            //    return await metaData.Apimlogmessage.Select(c => new {
            //        c.Id,
            //        c.CorrelationActivityId,
            //        c.MessageDirection,
            //        c.MetaRequestUrl,
            //        c.OperationName,
            //        c.ServiceName,
            //        c.SoapBody,
            //        c.SoapHeader,
            //        c.Status,
            //        c.Timestamp,
            //        c.Username
            //    }).ToListAsync();
            //}
        }
    }
}
