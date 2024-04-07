using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Core.Common;
using App.QTHTGis.Dal;
using App.QTHTGis.Dal.EntityClasses;
using App.QTHTGis.Dal.HelperClasses;
using App.Qtht.Services.Models;
using SD.LLBLGen.Pro.ORMSupportClasses;
using SD.LLBLGen.Pro.QuerySpec;

namespace App.QTHTGis.Services.Manager;

public interface IDmChucVuManager
{
    Task<ApiResponse<PageModelView<IEnumerable<DmChucVuModel>>>> PostPaging(PageModel pageModel);

    Task<ApiResponse<DmChucVuModel>> GetDetails(int id);

    Task<ApiResponse<IEnumerable<DmChucVuModel.ChucVuSelectModel>>> GetChucVuListByUnitcode(string unitcode);
}

public class DmChucVuManager : IDmChucVuManager
{
    public async Task<ApiResponse<PageModelView<IEnumerable<DmChucVuModel>>>> PostPaging(PageModel pageModel)
    {
        try
        {
            return await Task.Run(() =>
            {
                Predicate predicate = null;
                var data = new List<DmChucVuModel>();

                // filter
                EntityField2[] filterFields =
                {
                    DmchucvuFields.Id,
                    DmchucvuFields.Ten
                };

                if (!string.IsNullOrEmpty(pageModel.Search) && filterFields.Length > 0)
                    foreach (var item in filterFields)
                        predicate = predicate | item.Contains(pageModel.Search.ToUpper()).CaseInsensitive();

                var filter = new RelationPredicateBucket(predicate);

                // sort
                SortExpression sort = null;

                if (!string.IsNullOrWhiteSpace(pageModel.SortColumn))
                {
                    var fieldSort = new DmchucvuEntity().Fields.FirstOrDefault(c => c.Name == pageModel.SortColumn);
                    var sortClause = new SortClause(fieldSort, new FieldPersistenceInfo(pageModel.SortColumn),
                        SortOperator.Descending);
                    sort = new SortExpression(sortClause);
                }

                // prefetch
                var path = new PrefetchPath2(EntityType.DmchucvuEntity);

                using (var adapter = new DataAccessAdapterFactory().CreateAdapter())
                {
                    var collect = new EntityCollection<DmchucvuEntity>();
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

                    data = collect.Select(c => new DmChucVuModel
                    {
                        Id = c.Id,
                        Ten = c.Ten,
                        TrangThai = c.Trangthai
                    }).ToList();

                    return ApiResponse<PageModelView<IEnumerable<DmChucVuModel>>>.Generate(
                        new PageModelView<IEnumerable<DmChucVuModel>>
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
            return ApiResponse<PageModelView<IEnumerable<DmChucVuModel>>>.Generate(null, GeneralCode.Error, ex.Message);
        }
    }

    public async Task<ApiResponse<DmChucVuModel>> GetDetails(int id)
    {
        try
        {
            return await Task.Run(() =>
            {
                using (var adapter = new DataAccessAdapterFactory().CreateAdapter())
                {
                    var entity = new DmchucvuEntity(id);
                    var path = new PrefetchPath2(EntityType.DmchucvuEntity);

                    adapter.FetchEntity(entity, path);

                    return ApiResponse<DmChucVuModel>.Generate(
                        new DmChucVuModel
                        {
                            Id = entity.Id,
                            Ten = entity.Ten,
                            TrangThai = entity.Trangthai
                        }, GeneralCode.Success, null);
                }
            });
        }
        catch (Exception ex)
        {
            return ApiResponse<DmChucVuModel>.Generate(null, GeneralCode.Error, ex.Message);
        }
    }

    public async Task<ApiResponse<IEnumerable<DmChucVuModel.ChucVuSelectModel>>> GetChucVuListByUnitcode(
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
                     * Get ToChucId by unitcodeId
                    */


                    // Get sysunit entity by unitcode
                    var entity = new SysunitEntity(unitcode);

                    var path = new PrefetchPath2(EntityType.SysunitEntity);

                    path.Add(SysunitEntity.PrefetchPathDmtochuc);

                    adapter.FetchEntity(entity, path);

                    // Check if entity isn't exists
                    if (string.IsNullOrEmpty(entity.Unitcode))
                        throw new Exception(Constants.SystemMessage.SystemHasError);

                    // Check if hucvu reference object isn't exists
                    //if (entity.Dmtochuc == null) throw new Exception(Constants.SystemMessage.SystemHasError);

                    // IdToChuc is ensure exists
                    //var IdToChuc = entity.Dmtochuc.Id;

                    /*
                     * Select ChucVu list by ToChucId
                    */

                    var collect = new EntityCollection<DmchucvuEntity>();

                    var filter = new RelationPredicateBucket();

                    filter.PredicateExpression.AddWithAnd(DmchucvuFields.Trangthai == 1);

                    adapter.FetchEntityCollection(collect, filter);

                    return collect.Select(c => new DmChucVuModel.ChucVuSelectModel
                    {
                        Id = c.Id,
                        Ten = c.Ten
                    }).ToList();
                }
            });
        }
        catch (Exception ex)
        {
            return ApiResponse<IEnumerable<DmChucVuModel.ChucVuSelectModel>>.Generate(null, GeneralCode.Error,
                ex.Message);
        }
    }
}