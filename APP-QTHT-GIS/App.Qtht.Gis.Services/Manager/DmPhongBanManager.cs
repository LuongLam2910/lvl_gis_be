using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Core.Common;
using App.QTHTGis.Dal;
using App.QTHTGis.Dal.EntityClasses;
using App.Qtht.Services.Models;
using SD.LLBLGen.Pro.ORMSupportClasses;
using SD.LLBLGen.Pro.QuerySpec;
using App.QTHTGis.Dal.HelperClasses;

namespace App.QTHTGis.Services.Manager;

public interface IDmPhongBanManager
{
    Task<ApiResponse<PageModelView<IEnumerable<DmPhongBanModel>>>> PostPaging(PageModel pageModel);

    Task<ApiResponse<DmPhongBanModel>> GetDetails(int id);

    Task<ApiResponse<IEnumerable<DmPhongBanModel.PhongBanSelectModel>>> GetPhongBanListByUnitcode(string unitcode);
}

public class DmPhongBanManager : IDmPhongBanManager
{
    public async Task<ApiResponse<PageModelView<IEnumerable<DmPhongBanModel>>>> PostPaging(PageModel pageModel)
    {
        try
        {
            return await Task.Run(() =>
            {
                Predicate predicate = null;
                var data = new List<DmPhongBanModel>();

                // filter
                EntityField2[] filterFields =
                {
                    DmphongbanFields.Id,
                    DmphongbanFields.Ten,
                    DmphongbanFields.Unitcode
                };

                if (!string.IsNullOrEmpty(pageModel.Search) && filterFields.Length > 0)
                    foreach (var item in filterFields)
                        predicate = predicate | item.Contains(pageModel.Search.ToUpper()).CaseInsensitive();

                var filter = new RelationPredicateBucket(predicate);

                // sort
                SortExpression sort = null;

                if (!string.IsNullOrWhiteSpace(pageModel.SortColumn))
                {
                    var fieldSort = new DmphongbanEntity().Fields.FirstOrDefault(c => c.Name == pageModel.SortColumn);
                    var sortClause = new SortClause(fieldSort, new FieldPersistenceInfo(pageModel.SortColumn),
                        SortOperator.Descending);
                    sort = new SortExpression(sortClause);
                }

                // prefetch
                var path = new PrefetchPath2(EntityType.DmphongbanEntity);
                path.Add(DmphongbanEntity.PrefetchPathSysunit);

                using (var adapter = new DataAccessAdapterFactory().CreateAdapter())
                {
                    var collect = new EntityCollection<DmphongbanEntity>();
                    var totalPagesCount = 0;
                    var totalRecord = adapter.GetDbCount(collect, null);
                    var recordsCount = adapter.GetDbCount(collect, filter, null);

                    if (recordsCount <= pageModel.PageSize)
                    {
                        totalPagesCount = 1;
                        pageModel.CurrentPage = totalPagesCount;
                    }
                    else
                    {
                        var remainder = 0;
                        totalPagesCount = Math.DivRem(recordsCount, pageModel.PageSize, out remainder);
                        if (remainder > 0) totalPagesCount++;
                    }

                    adapter.FetchEntityCollection(collect, filter, 0, sort, path, pageModel.CurrentPage,
                        pageModel.PageSize);

                    data = collect.Select(c => new DmPhongBanModel
                    {
                        Id = c.Id,
                        Ten = c.Ten,
                        UnitCode = c.Unitcode,
                        UnitName = c?.Sysunit?.Tendonvi,
                        TrangThai = c.Trangthai
                    }).ToList();

                    return ApiResponse<PageModelView<IEnumerable<DmPhongBanModel>>>.Generate(
                        new PageModelView<IEnumerable<DmPhongBanModel>>
                        {
                            Data = data,
                            CurrentPage = pageModel.CurrentPage,
                            PageSize = pageModel.PageSize,
                            TotalPage = totalPagesCount,
                            TotalRecord = totalRecord,
                            RecordsCount = recordsCount
                        }, GeneralCode.Success, null);
                }
            });
        }
        catch (Exception ex)
        {
            return ApiResponse<PageModelView<IEnumerable<DmPhongBanModel>>>.Generate(null, GeneralCode.Error,
                ex.Message);
        }
    }

    public async Task<ApiResponse<DmPhongBanModel>> GetDetails(int id)
    {
        try
        {
            return await Task.Run(() =>
            {
                using (var adapter = new DataAccessAdapterFactory().CreateAdapter())
                {
                    var entity = new DmphongbanEntity(id);
                    var path = new PrefetchPath2(EntityType.DmphongbanEntity);
                    path.Add(DmphongbanEntity.PrefetchPathSysunit);

                    adapter.FetchEntity(entity, path);

                    return ApiResponse<DmPhongBanModel>.Generate(
                        new DmPhongBanModel
                        {
                            Id = entity.Id,
                            Ten = entity.Ten,
                            UnitCode = entity.Unitcode,
                            UnitName = entity?.Sysunit?.Tendonvi,
                            TrangThai = entity.Trangthai
                        }, GeneralCode.Success, null);
                }
            });
        }
        catch (Exception ex)
        {
            return ApiResponse<DmPhongBanModel>.Generate(null, GeneralCode.Error, ex.Message);
        }
    }

    public async Task<ApiResponse<IEnumerable<DmPhongBanModel.PhongBanSelectModel>>> GetPhongBanListByUnitcode(
        string unitcode)
    {
        try
        {
            if (string.IsNullOrEmpty(unitcode)) throw new Exception(Constants.SystemMessage.SystemHasError);


            return await Task.Run(() =>
            {
                using (var adapter = new DataAccessAdapterFactory().CreateAdapter())
                {
                    /*
                     * Filter PhongBan list by ToChucId
                    */

                    var collect = new EntityCollection<DmphongbanEntity>();

                    var filter = new RelationPredicateBucket();

                    filter.PredicateExpression.Add(DmphongbanFields.Unitcode == unitcode);
                    filter.PredicateExpression.AddWithAnd(DmphongbanFields.Trangthai == 1);

                    adapter.FetchEntityCollection(collect, filter);

                    return collect.Select(c => new DmPhongBanModel.PhongBanSelectModel
                    {
                        Id = c.Id,
                        Ten = c.Ten
                    }).ToList();
                }
            });
        }
        catch (Exception ex)
        {
            return ApiResponse<IEnumerable<DmPhongBanModel.PhongBanSelectModel>>.Generate(null, GeneralCode.Error,
                ex.Message);
        }
    }
}