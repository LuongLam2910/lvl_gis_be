using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Core.Common;
using App.QTHTGis.Dal.EntityClasses;
using App.QTHTGis.Dal.HelperClasses;
using SD.LLBLGen.Pro.ORMSupportClasses;
using SD.LLBLGen.Pro.QuerySpec;

namespace App.QTHTGis.Services.Manager;

public class DefaultManager<TEntity> where TEntity : CommonEntityBase, new()
{
    public async Task<ApiResponse<PageModelView<IEnumerable<object>>>> Paging(PageModel pageModel,
        Func<TEntity, object> selectModel, EntityField2 fieldCodition = null, SortClause sortBy = null,
        params EntityField2[] _params)
    {
        try
        {
            return await Task.Run(() =>
            {
                Predicate predicate = null;

                if (!string.IsNullOrEmpty(pageModel.Search) && _params.Length > 0)
                    foreach (var item in _params)
                        if (item.DataType != typeof(long))
                        {
                            var ascii = string.Concat(pageModel.Search.Normalize(NormalizationForm.FormD).Where(
                                c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark));
                            ascii = ascii.Replace("đ", "d").Replace("Đ", "D");
                            var searchField = new EntityField2("VnUnaccent",
                                new DbFunctionCall("\"appqtht\".vn_unaccent", new object[] { item }));
                            predicate = predicate | searchField.Contains(ascii.ToUpper()).CaseInsensitive();
                        }

                if (!string.IsNullOrEmpty(pageModel.Condition))
                    predicate = predicate & (fieldCodition == pageModel.Condition);

                var filter = new RelationPredicateBucket(predicate);

                using (var adapter = new DataAccessAdapterFactory().CreateAdapter())
                {
                    var data = new EntityCollection<TEntity>();
                    var totalPagesCount = 0;
                    var totalRecord = adapter.GetDbCount(data, null);
                    var recordsCount = adapter.GetDbCount(data, filter, null);

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

                    SortExpression sort = null;
                    if (sortBy != null) sort = new SortExpression(sortBy);

                    adapter.FetchEntityCollection(data, filter, 0, sort, pageModel.CurrentPage, pageModel.PageSize);
                    return new PageModelView<IEnumerable<object>>
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

    public async Task<ApiResponse<TEntity>> SelectOne(EntityField2 fieldSelect, int key)
    {
        using (var adapter = new DataAccessAdapterFactory().CreateAdapter())
        {
            try
            {
                return await Task.Run(() =>
                {
                    var entity = new EntityCollection<TEntity>();
                    adapter.FetchEntityCollection(entity, new RelationPredicateBucket(fieldSelect == key));
                    //adapter.FetchEntity(obj);
                    return entity[0];
                });
            }
            catch (ORMException ex)
            {
                return ApiResponse<TEntity>.Generate(null, GeneralCode.Error, ex.Message);
            }
        }
    }

    public async Task<ApiResponse<IEnumerable<object>>> SelectAll(Func<TEntity, object> modelSelectAll = null,
        SortClause sort = null)
    {
        try
        {
            return await Task.Run(() =>
            {
                using (var adapter = new DataAccessAdapterFactory().CreateAdapter())
                {
                    var fields = new ResultsetFields(3);
                    var collection = new EntityCollection<TEntity>();
                    SortExpression sortExpression = null;
                    if (sort != null) sortExpression = new SortExpression(sort);
                    adapter.FetchEntityCollection(collection, null, 0, sortExpression);
                    var result = modelSelectAll == null ? collection : collection.Select(modelSelectAll);
                    return ApiResponse<IEnumerable<object>>.Generate(result, GeneralCode.Success);
                }
            });
        }
        catch (ORMException ex)
        {
            return ApiResponse<IEnumerable<object>>.Generate(null, GeneralCode.Error, ex.Message);
        }
    }

    public async Task<ApiResponse> Insert(TEntity entity, Predicate predicate = null)
    {
        using (var adapter = new DataAccessAdapterFactory().CreateAdapter())
        {
            try
            {
                if (predicate != null)
                {
                    var collect = new EntityCollection<TEntity>();
                    adapter.FetchEntityCollection(collect, new RelationPredicateBucket(predicate));
                    if (collect.Count > 0) return GeneralCode.Duplicate;
                }

                await adapter.SaveEntityAsync(entity);
                return GeneralCode.Success;
            }
            catch (ORMException ex)
            {
                return ApiResponse.Generate(GeneralCode.Error, ex.Message);
            }
        }
    }

    public async Task<ApiResponse> Update(TEntity entity, Predicate predicate = null)
    {
        using (var adapter = new DataAccessAdapterFactory().CreateAdapter())
        {
            try
            {
                if (predicate != null)
                {
                    var collect = new EntityCollection<TEntity>();
                    adapter.FetchEntityCollection(collect, new RelationPredicateBucket(predicate));
                    if (collect.Count == 0) return GeneralCode.NotFound;
                }

                await adapter.SaveEntityAsync(entity);
                return GeneralCode.Success;
            }
            catch (ORMException ex)
            {
                return ApiResponse.Generate(GeneralCode.Error, ex.Message);
            }
        }
    }

    public async Task<ApiResponse> Delete(EntityField2 fieldKey, int key)
    {
        try
        {
            var Collection = new EntityCollection<TEntity>();
            using (var adapter = new DataAccessAdapterFactory().CreateAdapter())
            {
                adapter.FetchEntityCollection(Collection, new RelationPredicateBucket(fieldKey == key));
                await adapter.DeleteEntityAsync(Collection[0]);
            }

            return GeneralCode.Success;
        }
        catch (ORMEntityIsDeletedException ex)
        {
            return ApiResponse.Generate(GeneralCode.Error, ex.Message);
        }
    }
}