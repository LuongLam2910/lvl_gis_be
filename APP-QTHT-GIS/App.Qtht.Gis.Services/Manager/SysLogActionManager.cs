using App.Core.Common;
using App.QTHTGis.Dal.DatabaseSpecific;
using App.QTHTGis.Dal.EntityClasses;
using App.QTHTGis.Dal.HelperClasses;
using App.Qtht.Services.Models;
using SD.LLBLGen.Pro.ORMSupportClasses;
using SD.LLBLGen.Pro.QuerySpec;
using System;
using System.Collections.Generic;
using System.Linq;
using App.QTHTGis.Dal.Linq;
using System.Text;
using System.Threading.Tasks;
using static App.Qtht.Services.Models.SysUserModel;

namespace App.QTHTGis.Services.Manager
{
    public interface ISysLogactionManager
    {
        Task<ApiResponse<EntityCollection<SyslogactionEntity>>> GetAllAction();
        Task<ApiResponse<PageModelView<IEnumerable<object>>>> PagingLogin(PageModel pageModel, Func<SyslogactionEntity, object> selectModel, EntityField2 fieldCodition = null, SortClause sortBy = null, params EntityField2[] _params);
        Task<ApiResponse<PageModelView<IEnumerable<object>>>> PagingAction(PageModel pageModel, Func<SyslogactionEntity, object> selectModel, EntityField2 fieldCodition = null, SortClause sortBy = null, params EntityField2[] _params);
        Task<ApiResponse<PageModelView<IEnumerable<NhatKyHoatDongModel>>>> PagingHistory(PageModel pageModel, int key);
        Task<ApiResponse<IEnumerable<NhatKyHoatDongModel>>> HistoryLogin(int key);
    }
    public class SysLogactionManager : ISysLogactionManager
    {
        public async Task<ApiResponse<EntityCollection<SyslogactionEntity>>> GetAllAction ()
        {
            return await Task.Run(() =>
            {

                using (DataAccessAdapter adapter = new DataAccessAdapterFactory().CreateAdapter())
                {
                    EntityCollection<SyslogactionEntity> sysLogactions = new EntityCollection<SyslogactionEntity>();
                    adapter.FetchEntityCollection(sysLogactions, new RelationPredicateBucket());
                    return ApiResponse<EntityCollection<SyslogactionEntity>>.Generate(sysLogactions);
                }
            });
        }
        public async Task<ApiResponse<PageModelView<IEnumerable<object>>>> PagingLogin(PageModel pageModel, Func<SyslogactionEntity, object> selectModel, EntityField2 fieldCodition = null, SortClause sortBy = null, params EntityField2[] _params)
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
                            predicate = predicate | item.Contains(pageModel.Search.ToUpper()).CaseInsensitive();
                        }
                    }

                  
                        predicate = (predicate) & (Predicate)SyslogactionFields.Action.In("LogIn","LogOut");
                    

                    RelationPredicateBucket filter = new RelationPredicateBucket(predicate);

                    using (DataAccessAdapter adapter = new DataAccessAdapterFactory().CreateAdapter())
                    {
                        var data = new EntityCollection<SyslogactionEntity>();
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
        }
        public async Task<ApiResponse<PageModelView<IEnumerable<object>>>> PagingAction(PageModel pageModel, Func<SyslogactionEntity, object> selectModel, EntityField2 fieldCodition = null, SortClause sortBy = null, params EntityField2[] _params)
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
                            predicate = predicate | item.Contains(pageModel.Search.ToUpper()).CaseInsensitive();
                        }
                    }


                    predicate = (predicate) & (Predicate)SyslogactionFields.Action.In(Constants.UserAction.Sync, Constants.UserAction.Delete,Constants.UserAction.Update,Constants.UserAction.Insert);
                    RelationPredicateBucket filter = new RelationPredicateBucket(predicate);

                    using (DataAccessAdapter adapter = new DataAccessAdapterFactory().CreateAdapter())
                    {
                        var data = new EntityCollection<SyslogactionEntity>();
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
        }



        public async Task<ApiResponse<PageModelView<IEnumerable<NhatKyHoatDongModel>>>> PagingHistory(PageModel pageModel, int key)
        {
            try
            {
                return await Task.Run(() =>
                {

                    using (DataAccessAdapter adapter = new DataAccessAdapterFactory().CreateAdapter())
                    {
                        var data = new EntityCollection<SyslogactionEntity>();
                        int totalPagesCount = 0;
                        RelationPredicateBucket filter = new RelationPredicateBucket();
                        filter.PredicateExpression.AddWithAnd(SyslogactionFields.UserId == key);
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

                        SortExpression sort = new SortExpression(SyslogactionFields.DateCreate | SortOperator.Descending);

                        adapter.FetchEntityCollection(data, filter, 0, sort, pageModel.CurrentPage, pageModel.PageSize);
                        var dataNew = data.Select(c => new NhatKyHoatDongModel
                        {
                            userId = (int)c.UserId,
                            NgayTao = c.DateCreate,
                            NguoiTao = c.UserName,
                            HanhDong = c.Action,
                            Noidung = c.Note,
                            DiaChiIP = c.IpClient
                        });
                        return new PageModelView<IEnumerable<NhatKyHoatDongModel>>()
                        {
                            Data = dataNew,
                            CurrentPage = pageModel.CurrentPage,
                            PageSize = pageModel.PageSize,
                            TotalPage = totalPagesCount,
                            TotalRecord = recordsCount,
                            RecordsCount = recordsCount
                        };
                    }
                });
            }
            catch (ORMEntityValidationException ex)
            {
                return ApiResponse<PageModelView<IEnumerable<NhatKyHoatDongModel>>>.Generate(null, GeneralCode.Error, ex.Message);
            }
        }

        public async Task<ApiResponse<IEnumerable<NhatKyHoatDongModel>>> HistoryLogin(int key)
        {
            try
            {
                return await Task.Run(() =>
                {
                    using (DataAccessAdapterBase adapter = new DataAccessAdapterFactory().CreateAdapter())
                    {
                        EntityCollection<SyslogactionEntity> collect = new EntityCollection<SyslogactionEntity>();

                        RelationPredicateBucket filter = new RelationPredicateBucket();

                        adapter.FetchEntityCollection(collect, filter);

                        try
                        {
                            var data = collect.Where(x => x.UserId == key).Select(c => new NhatKyHoatDongModel
                            {
                                userId = (int)c.UserId,
                                NgayTao = c.DateCreate,
                                NguoiTao = c.UserName,
                                HanhDong = c.Action,
                                Noidung = c.Note,
                                DiaChiIP = c.IpClient
                            }).ToList();
                            return ApiResponse<IEnumerable<NhatKyHoatDongModel>>.Generate(data, GeneralCode.Success, null);
                        }
                        catch (Exception ex)
                        {
                            return ApiResponse<IEnumerable<NhatKyHoatDongModel>>.Generate(null, GeneralCode.Error, null);
                        }

                    }
                });
            }
            catch (Exception ex)
            {
                return ApiResponse<IEnumerable<NhatKyHoatDongModel>>.Generate(null, GeneralCode.Error, ex.Message);
            }
        }
    }
}
