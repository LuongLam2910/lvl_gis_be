
using App.CongAnGis.Dal.DatabaseSpecific;
using App.CongAnGis.Dal.EntityClasses;
using App.CongAnGis.Dal.HelperClasses;
using App.CongAnGis.Dal.Linq;
using App.CongAnGis.Services.ManagerBase;
using App.Core.Common;
using SD.LLBLGen.Pro.ORMSupportClasses;
using SD.LLBLGen.Pro.QuerySpec;
using SD.LLBLGen.Pro.QuerySpec.Adapter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static App.ConganGis.Services.Model.TrangThietBiModel;

namespace App.CongAnGis.Services.Manager
{
    public class SystrangthietbidoituongManager : SystrangthietbidoituongManagerBase
    {
        private readonly SystrangthietbidoituongManager _manager;
        public SystrangthietbidoituongManager()
        { }


        public async Task<ApiResponse> UpdateTrangThaiTrangThietBi(int iddata, string tablename, int trangthai)
        {
            try
            {
                using (DataAccessAdapter adapter = new DataAccessAdapterFactory().CreateAdapter())
                {

                    string app = adapter.CatalogNameToUse;
                    var metaData = new LinqMetaData(adapter);
                    string sql = "UPDATE " + app + "." + tablename + " set dieukien = " + trangthai + " where id = " + iddata;
                    adapter.ExecuteSQL(sql);
                    adapter.Commit();

                }
                return GeneralCode.Success;
            }
            catch (Exception ex)
            {
                return ApiResponse.Generate(GeneralCode.Error, ex.Message);
            }
        }

        public async Task<Boolean> GetTrangThaiTrangThietBi(int iddata, string tablename)
        {
            try
            {
                using (DataAccessAdapter adapter = new DataAccessAdapterFactory().CreateAdapter())
                {

                    string app = adapter.CatalogNameToUse;
                    string sql = "SELECT dieukien from " + app + "." + tablename + " where id = " + iddata;
                    int dieukien = adapter.FetchQuery<int>(sql)[0];
                    adapter.Commit();
                    return dieukien == 1 ? true : false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<ApiResponse<PageModelView<IEnumerable<TTBJoinModel>>>> pagingCustom(PageSystrangthietbi _model)
        {
            try
            {
                return await Task.Run(() =>
                {
                    RelationPredicateBucket filter = new RelationPredicateBucket();
                    if (!string.IsNullOrWhiteSpace(_model.strKey))
                    {
                        _model.strKey = "%" + _model.strKey + "%";
                        var pred1 = (SystrangthietbidoituongFields.Iddoituong).Like(_model.strKey)
                            .Or(SystrangthietbidoituongFields.Ngaytao.Like(_model.strKey)).Or(SystrangthietbidoituongFields.Tablename.Like(_model.strKey));
                        filter.PredicateExpression.Add(pred1);
                    }
                    using (DataAccessAdapterBase adapter = new DataAccessAdapterFactory().CreateAdapter())
                    {
                        var data = new EntityCollection<SystrangthietbidoituongEntity>();
                        filter.PredicateExpression.AddWithAnd(SystrangthietbidoituongFields.Iddoituong == _model.idDoiTuong);
                        filter.PredicateExpression.AddWithAnd(SystrangthietbidoituongFields.Tablename == _model.tableName);

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
                        SortExpression sort = new SortExpression(SystrangthietbidoituongFields.Id | SortOperator.Descending);
                        try
                        {
                            LinqMetaData lmd = new LinqMetaData(adapter);
                            var query = (from tb in lmd.Systrangthietbidoituong
                                         join dm in lmd.Sysdmtrangthietbipccc on tb.Idthietbi equals dm.Id
                                         where tb.Iddoituong == _model.idDoiTuong && tb.Tablename == _model.tableName
                                         select new TTBJoinModel
                                         {
                                             _Id = tb.Id,
                                             _iddoituong = tb.Iddoituong,
                                             _idthietbi = tb.Idthietbi,
                                             _moTa = dm.Mota,
                                             _ngaytao = tb.Ngaytao,
                                             _soluong = tb.Soluong,
                                             _tablename = tb.Tablename,
                                             _tenThietBi = dm.Tenthietbi
                                         }
                                        ).ToList();
                            //adapter.FetchEntityCollection(data, filter, 0, sort, null, _model.currentPage, _model.pageSize);
                            return new PageModelView<IEnumerable<TTBJoinModel>>()
                        {
                            Data = query,
                            CurrentPage = _model.currentPage,
                            PageSize = _model.pageSize,
                            TotalPage = totalPagesCount,
                            TotalRecord = totalRecord,
                            RecordsCount = recordsCount
                        };
                        }
                        catch (Exception ex)
                        {
                            return ApiResponse<PageModelView<IEnumerable<TTBJoinModel>>>.Generate(null, GeneralCode.Error, ex.Message);
                        }
                        
                    }
                });
            }
            catch (ORMEntityValidationException ex)
            {
                return ApiResponse<PageModelView<IEnumerable<TTBJoinModel>>>.Generate(null, GeneralCode.Error, ex.Message);
            }
        }
    }
}

