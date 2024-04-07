
using App.CongAnGis.Dal.DatabaseSpecific;
using App.CongAnGis.Dal.EntityClasses;
using App.CongAnGis.Dal.FactoryClasses;
using App.CongAnGis.Dal.HelperClasses;
using App.CongAnGis.Services.ManagerBase;
using App.Core.Common;
using SD.LLBLGen.Pro.ORMSupportClasses;
using SD.LLBLGen.Pro.QuerySpec;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.CongAnGis.Services.Manager
{
    public interface ISysMapManagerCustom
    {
        Task<ApiResponse<List<SysmapVM.ItemSysmap>>> SelectAllAsyncCustom();
        Task<ApiResponse<PageModelView<IEnumerable<SysmapVM.ItemSysmap>>>> SelectPagingCustom(SysmapVM.PageSysmap _model);
        Task<ApiResponse> InsertAsyncCustom(SysmapVM.ItemSysmap _Model);
        Task<ApiResponse> UpdateAsyncCustom(SysmapVM.ItemSysmap _Model);
        Task<ApiResponse> DeleteAsyncCustom(int id);
    }
    public class SysmapManager : SysmapManagerBase, ISysMapManagerCustom
    {
        public SysmapManager()
        {
        }

        public async Task<ApiResponse<List<SysmapVM.ItemSysmap>>> SelectAllAsyncCustom()
        {
            try
            {
                return await Task.Run(() =>
                {

                    EntityCollection<SysmapEntity> _Collection = SelectAll();
                    List<SysmapVM.ItemSysmap> toReturn = _Collection.Select(c => configEntitytoVM.CreateMapper().Map<SysmapEntity, SysmapVM.ItemSysmap>(c)).ToList();

                    return ApiResponse<List<SysmapVM.ItemSysmap>>.Generate(toReturn, GeneralCode.Success);

                });

            }
            catch (ORMException ex)
            {
                return ApiResponse<List<SysmapVM.ItemSysmap>>.Generate(null, GeneralCode.Error, ex.Message);
            }
        }



        public async Task<ApiResponse<PageModelView<IEnumerable<SysmapVM.ItemSysmap>>>> SelectPagingCustom(SysmapVM.PageSysmap _model)
        {
            try
            {
                return await Task.Run(() =>
                {
                    RelationPredicateBucket filter = new RelationPredicateBucket();
                    if (!string.IsNullOrWhiteSpace(_model.strKey))
                    {
                        _model.strKey = "%" + _model.strKey + "%";
                        var pred1 = (SysmapFields.Name).Like(_model.strKey)
                            .Or(SysmapFields.Status.Like(_model.strKey)).Or(SysmapFields.Iconhome.Like(_model.strKey)).Or(SysmapFields.Unitcode.Like(_model.strKey));
                        filter.PredicateExpression.Add(pred1);
                    }
                    using (DataAccessAdapterBase adapter = new DataAccessAdapterFactory().CreateAdapter())
                    {
                        var data = new EntityCollection<SysmapEntity>();
                        int totalPagesCount = 0;
                        int totalRecord = (int)adapter.GetDbCount(data, null);
                        int recordsCount = (int)adapter.GetDbCount(data, filter, null);
                        if (recordsCount <= _model.pageSize)
                        {
                            totalPagesCount = 1;
                            _model.currentPage = totalPagesCount;
                        }
                        else
                        {
                            int remainder = 0;
                            totalPagesCount = Math.DivRem(recordsCount, _model.pageSize, out remainder);
                            if (remainder > 0)
                            {
                                totalPagesCount++;
                            }
                        }
                        SortExpression sort = new SortExpression(SysmapFields.Id | SortOperator.Descending);
                        try
                        {
                            var parameters = new QueryParameters(_model.currentPage, _model.pageSize, _model.pageSize, filter)
                            {
                                CollectionToFetch = data,
                                CacheResultset = true,
                                SorterToUse = sort,
                                CacheDuration = new TimeSpan(0, 0, 10)  // cache for 10 seconds
                            };
                            adapter.FetchEntityCollection(parameters);
                            //adapter.FetchEntityCollection(data, filter, 0, sort, null, _model.currentPage, _model.pageSize);
                        }
                        catch (Exception ex)
                        {
                            return ApiResponse<PageModelView<IEnumerable<SysmapVM.ItemSysmap>>>.Generate(null, GeneralCode.Error, ex.Message);
                        }
                        return new PageModelView<IEnumerable<SysmapVM.ItemSysmap>>()
                        {
                            Data = data.Select(c => configEntitytoVM.CreateMapper().Map<SysmapEntity, SysmapVM.ItemSysmap>(c)).ToList(),
                            CurrentPage = _model.currentPage,
                            PageSize = _model.pageSize,
                            TotalPage = totalPagesCount,
                            TotalRecord = totalRecord,
                            RecordsCount = recordsCount
                        };
                    }
                });
            }
            catch (ORMEntityValidationException ex)
            {
                return ApiResponse<PageModelView<IEnumerable<SysmapVM.ItemSysmap>>>.Generate(null, GeneralCode.Error, ex.Message);
            }
        }

        public async Task<ApiResponse> DeleteAsyncCustom(int id)
        {
            try
            {
                return await Task.Run(() =>
                {
                    using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
                    {
                        SysmapEntity _SysmapEntity = new SysmapEntity(id);
                        if (adapter.FetchEntity(_SysmapEntity))
                        {
                            adapter.DeleteEntity(_SysmapEntity);
                        }
                        return GeneralCode.Success;
                    }
                });
            }
            catch (ORMEntityIsDeletedException ex)
            {
                return ApiResponse.Generate(GeneralCode.Error, ex.Message);
            }
        }

        public async Task<ApiResponse> UpdateAsyncCustom(SysmapVM.ItemSysmap _Model)
        {
            try
            {
                return await Task.Run(() =>
                {
                    SysmapEntity _SysmapEntity = configVMtoEntity.CreateMapper().Map<SysmapEntity>(_Model);
                    Update(_SysmapEntity);
                    return GeneralCode.Success;
                });
            }
            catch (Exception ex)
            {
                return ApiResponse.Generate(GeneralCode.Error, ex.Message);
            }
        }



        public async Task<ApiResponse> InsertAsyncCustom(SysmapVM.ItemSysmap _Model)
        {
            try
            {
                return await Task.Run(() =>
                {
                    SysmapEntity _SysmapEntity = configVMtoEntity.CreateMapper().Map<SysmapEntity>(_Model);
                    Insert(_SysmapEntity);
                    return GeneralCode.Success;
                });

            }
            catch (Exception ex)
            {
                return ApiResponse.Generate(GeneralCode.Error, ex.Message);
            }
        }

    }
}

