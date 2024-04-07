using App.CongAnGis.Services.Hubs;
using App.ConganGis.Services.ManagerBase;
using App.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static App.ConganGis.Services.Model.SysBaoTaiNanModel;
using App.ConganGis.Services.Model;
using SD.LLBLGen.Pro.ORMSupportClasses;
using App.CongAnGis.Services;
using App.CongAnGis.Dal.EntityClasses;
using DocumentFormat.OpenXml.EMMA;
using App.CongAnGis.Dal.HelperClasses;
using App.CongAnGis.Services.ManagerBase;
using SD.LLBLGen.Pro.QuerySpec;
using DocumentFormat.OpenXml.Vml.Office;

namespace App.ConganGis.Services.Manager
{
    public interface ISysCSGTManager
    {
        Task<ApiResponse<PageModelView<IEnumerable<CSGTModel>>>> Paging(CSGTModelPageModel model);
        Task<ApiResponse> UpdateAsync(CSGTModel model);
        Task<ApiResponse<IEnumerable<CSGTModel>>> getAll();
        Task<ApiResponse> InsertCustomAsync(CSGTModel model);
        Task<ApiResponse> Delete(int id);
    }
    public class SysCSGTManager : ISysCSGTManager
    {
        public async Task<ApiResponse> Delete(int id)
        {
            try
            {
                using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
                {
                    SyscsgtEntity _SyscsgtEntity = new SyscsgtEntity(id);
                    adapter.DeleteEntity(_SyscsgtEntity);
                }
                return GeneralCode.Success;
            }
            catch (ORMEntityValidationException ex)
            {
                return ApiResponse.Generate(GeneralCode.Error, ex.Message);
            }
        }

        public async Task<ApiResponse<IEnumerable<CSGTModel>>> getAll()
        {
            using (DataAccessAdapterBase adapterBase = new DataAccessAdapterFactory().CreateAdapter())
            {
                try
                {
                    var entity = new EntityCollection<SyscsgtEntity>();
                    adapterBase.FetchEntityCollection(entity, null);
                    var data = entity.Select(c => new CSGTModel
                    {
                        chucVu = c.Chucvu,
                        createDate = c.Createddate,
                        dienThoai = c.Sodienthoai,
                        donVi = c.Donvi,
                        id = c.Id,
                        maHuyen = c.Mahuyen,
                        maXa = c.Maxa,
                        name = c.Hovaten,
                        status = c.Status,
                        userId = c.Userid,
                        diaChi = c.Diachi
                    }).ToList();
                    return ApiResponse<IEnumerable<CSGTModel>>.Generate(data, GeneralCode.Success, "Thành công");
                }
                catch (Exception ex)
                {
                    return ApiResponse<IEnumerable<CSGTModel>>.Generate(GeneralCode.Error, ex.Message);
                }
            }
        }

        public async Task<ApiResponse> InsertCustomAsync(CSGTModel model)
        {
            using (DataAccessAdapterBase adapterBase = new DataAccessAdapterFactory().CreateAdapter())
            {
                try
                {
                    var entity = new SyscsgtEntity();
                    entity.Createddate = DateTime.Now;
                    entity.Hovaten = model.name;
                    entity.Donvi = model.donVi;
                    entity.Sodienthoai = model.dienThoai;
                    entity.Chucvu = model.chucVu;
                    entity.Mahuyen = model.maHuyen;
                    entity.Maxa = model.maXa;
                    entity.Userid = model.userId;
                    entity.Diachi = model.diaChi;
                    adapterBase.SaveEntity(entity);
                    adapterBase.Commit();
                    return GeneralCode.Success;
                }
                catch (Exception ex)
                {
                    return ApiResponse.Generate(GeneralCode.Error, ex.Message);
                }
            }
        }

        public async Task<ApiResponse<PageModelView<IEnumerable<CSGTModel>>>> Paging(CSGTModelPageModel _model)
        {
            try
            {
                return await Task.Run(() =>
                {
                    RelationPredicateBucket filter = new RelationPredicateBucket();
                    if (!string.IsNullOrWhiteSpace(_model.strKey))
                    {
                        _model.strKey = "%" + _model.strKey + "%";
                        var pred1 = (SyscsgtFields.Hovaten).Like(_model.strKey)
                            .Or(SyscsgtFields.Sodienthoai.Like(_model.strKey))
                            .Or(SyscsgtFields.Donvi.Like(_model.strKey))
                        .Or(SyscsgtFields.Chucvu.Like(_model.strKey));
                        filter.PredicateExpression.Add(pred1);
                    }
                    using (DataAccessAdapterBase adapter = new DataAccessAdapterFactory().CreateAdapter())
                    {
                        var data = new EntityCollection<SyscsgtEntity>();
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
                        SortExpression sort = new SortExpression(SyscsgtFields.Id | SortOperator.Descending);
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
                            return ApiResponse<PageModelView<IEnumerable<CSGTModel>>>.Generate(null, GeneralCode.Error, ex.Message);
                        }
                        return new PageModelView<IEnumerable<CSGTModel>>()
                        {
                            Data = data.Select(c => new CSGTModel
                            {
                                chucVu = c.Chucvu,
                                createDate = c.Createddate,
                                dienThoai = c.Sodienthoai,
                                donVi = c.Donvi,
                                id = c.Id,
                                maHuyen = c.Mahuyen,
                                maXa = c.Maxa,
                                name = c.Hovaten,
                                status = c.Status,
                                userId = c.Userid,
                                diaChi = c.Diachi
                            }).ToList(),
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
                return ApiResponse<PageModelView<IEnumerable<CSGTModel>>>.Generate(null, GeneralCode.Error, ex.Message);
            }
        }

        public async Task<ApiResponse> UpdateAsync(CSGTModel model)
        {
            using (DataAccessAdapterBase adapterBase = new DataAccessAdapterFactory().CreateAdapter())
            {
                try
                {
                    var entity = new SyscsgtEntity(model.id);
                    entity.Createddate = model.createDate;
                    entity.Hovaten = model.name;
                    entity.Donvi = model.donVi;
                    entity.Sodienthoai = model.dienThoai;
                    entity.Chucvu = model.chucVu;
                    entity.Mahuyen = model.maHuyen;
                    entity.Maxa = model.maXa;
                    entity.Userid = model.userId;
                    entity.Diachi = model.diaChi;
                    entity.IsNew = false;
                    adapterBase.SaveEntity(entity);
                    adapterBase.Commit();
                    return GeneralCode.Success;
                }
                catch (Exception ex)
                {
                    return ApiResponse.Generate(GeneralCode.Error, ex.Message);
                }
            }
        }
    }
}
