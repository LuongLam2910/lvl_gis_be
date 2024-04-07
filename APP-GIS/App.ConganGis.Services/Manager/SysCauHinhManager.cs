using App.CongAnGis.Dal.EntityClasses;
using App.CongAnGis.Services.ManagerBase;
using App.Core.Common;
using SD.LLBLGen.Pro.ORMSupportClasses;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using App.CongAnGis.Dal.HelperClasses;
using static App.CongAnGis.Services.Models.SysCauHinhModel;
using System.Linq;
using DocumentFormat.OpenXml.Spreadsheet;
using Nancy.Json;
using System.Net.Http;
using System.Net.Http.Json;
using System.Net;
using System.Text.Json;
using System.Text.Json.Nodes;
using App.ConganGis.Services.Manager;
using DocumentFormat.OpenXml.EMMA;
using App.CongAnGis.Dal.Linq;
using DocumentFormat.OpenXml.VariantTypes;
using Quartz;

namespace App.CongAnGis.Services.Manager
{
    public interface ISysCauHinhManager
    {
        Task<ApiResponse> InsertAsync(CauHinhResponseModel model);
        Task<ApiResponse> Delete(int id);
        Task<ApiResponse<PageModelView<IEnumerable<CauHinhResponseModel>>>> Paging(CauHinhPagingModel _model);
        Task<ApiResponse> distancematrix(CauHinhResponseModel model);
        Task<IEnumerable<CauHinhResponseModel>> getAllCauHinhAsync();
        Task<CauHinhResponseModel> getOneById(int key);
    }
    public class SysCauHinhManager : ISysCauHinhManager
    {
        private readonly ISchedulerManager _schedulerManager;

        public SysCauHinhManager(ISchedulerManager schedulerManager) { 
            _schedulerManager = schedulerManager;   
        }

        public async Task<ApiResponse> Delete(int id)
        {
            try
            {
                using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
                {
                    SyscauhinhEntity syscauhinhEntity = new SyscauhinhEntity(id);
                    adapter.DeleteEntity(syscauhinhEntity);
                }
                return GeneralCode.Success;
            }
            catch (ORMEntityValidationException ex)
            {
                return ApiResponse.Generate(GeneralCode.Error, ex.Message);
            }
        }

        public async Task<ApiResponse> distancematrix(CauHinhResponseModel model)
        {
            string uri = "https://maps.googleapis.com/maps/api/distancematrix/json?origins="+ model.doanDuongTu +"&destinations="+ model.doanDuongDen +"&mode=driving&language=vi-VN&key=AIzaSyC_7EtZhIa_yO2R2n8ibXIAseY2LnbJRHk&departure_time=now";
            using (WebClient wc = new WebClient())
            {
                var json = wc.DownloadString(uri);
                var jsonObject = JsonNode.Parse(json).AsObject();
                using (DataAccessAdapterBase adapterBase = new DataAccessAdapterFactory().CreateAdapter())
                {
                    try
                    {
                        if (jsonObject["rows"][0]["elements"][0]["status"].ToString() != "NOT_FOUND")
                        {
                            var entities = new SyslinhsucanhbaoEntity();
                            entities.Doanduongden = jsonObject["destination_addresses"][0].ToString();
                            entities.Doanduongtu = jsonObject["origin_addresses"][0].ToString();
                            entities.Khoangcanh = jsonObject["rows"][0]["elements"][0]["distance"]["text"].ToString();
                            entities.Thoigian = jsonObject["rows"][0]["elements"][0]["duration"]["text"].ToString();
                            entities.Thoigiantt = jsonObject["rows"][0]["elements"][0]["duration_in_traffic"]["text"].ToString();
                            entities.Createdate = DateTime.Now.ToString();
                            adapterBase.SaveEntity(entities);
                            adapterBase.Commit();
                            return ApiResponse.Generate(GeneralCode.Success, "SUCCESS");
                        }
                        return ApiResponse.Generate(GeneralCode.Error, "ERROR");
                    }
                    catch (Exception ex)
                    {

                        return ApiResponse.Generate(GeneralCode.Error, ex.Message);
                    }
                   
                }
            }
        }

        public async Task<ApiResponse> InsertAsync(CauHinhResponseModel model)
        {
            try
            {
                using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
                {
                    //tạo ra các ID số tăng dần
                    //DataAccessAdapter.SetOracleCompatibilityLevel(OracleCompatibilityLevel.Oracle12c);
                    var _SyscauhinhEntity = new SyscauhinhEntity();
                    _SyscauhinhEntity.Doanduongden = model.doanDuongDen;
                    _SyscauhinhEntity.Doanduongtu = model.doanDuongTu;
                    _SyscauhinhEntity.Starttime = model.startTime;
                    _SyscauhinhEntity.Endtime = model.endTime;
                    _SyscauhinhEntity.Status = model.status;
                    _SyscauhinhEntity.Tansuat = model.tuanSuat;
                    adapter.SaveEntity(_SyscauhinhEntity, true);
                }
                return GeneralCode.Success;
            }
            catch (ORMEntityValidationException ex)
            {
                return ApiResponse.Generate(GeneralCode.Error,ex.Message);
            }
        }

        public async Task<ApiResponse<PageModelView<IEnumerable<CauHinhResponseModel>>>> Paging(CauHinhPagingModel _model)
        {
            try
            {
                return await Task.Run(() =>
                {
                    RelationPredicateBucket filter = new RelationPredicateBucket();
                    //if (!string.IsNullOrWhiteSpace(_model.strKey))
                    //{
                    //    _model.strKey = "%" + _model.strKey + "%";
                    //    var pred1 = (SysbaochayFields.Iddata).Equal(_model.strKey)
                    //        .Or(SysbaochayFields.Address.Like(_model.strKey)).Or(SysbaochayFields.Phonenumber.Equal(_model.strKey)).Or(SysbaochayFields.Tablename.Like(_model.strKey)).Or(SysbaochayFields.Lnglat.Like(_model.strKey));
                    //    filter.PredicateExpression.Add(pred1);
                    //}
                    using (DataAccessAdapterBase adapter = new DataAccessAdapterFactory().CreateAdapter())
                    {
                        var data = new EntityCollection<SyscauhinhEntity>();
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
                        SortExpression sort = new SortExpression(SyscauhinhFields.Id | SortOperator.Descending);
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
                            return ApiResponse<PageModelView<IEnumerable<CauHinhResponseModel>>>.Generate(null, GeneralCode.Error, ex.Message);
                        }
                        return new PageModelView<IEnumerable<CauHinhResponseModel>>()
                        {
                            Data = data.Select(c => new CauHinhResponseModel
                            {
                                id = c.Id,
                                doanDuongDen = c.Doanduongden,
                                doanDuongTu = c.Doanduongtu,
                                endTime = c.Endtime,
                                startTime = c.Starttime,
                                status = c.Status,
                                tuanSuat = c.Tansuat
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
                return ApiResponse<PageModelView<IEnumerable<CauHinhResponseModel>>>.Generate(null, GeneralCode.Error, ex.Message);
            }
        }


        public async Task<IEnumerable<CauHinhResponseModel>> getAllCauHinhAsync()
        {
            using (DataAccessAdapterBase adapterBase = new DataAccessAdapterFactory().CreateAdapter())
            {
                EntityCollection<SyscauhinhEntity> data = new EntityCollection<SyscauhinhEntity>();
                adapterBase.FetchEntityCollection(data, null);
                var Data = data.Select(c => new CauHinhResponseModel
                {
                    id = c.Id,
                    doanDuongDen = c.Doanduongden,
                    doanDuongTu = c.Doanduongtu,
                    endTime = c.Endtime,
                    startTime = c.Starttime,
                    status = c.Status,
                    tuanSuat = c.Tansuat
                }).ToList();
                foreach (var item in Data)
                {
                    await _schedulerManager.CreateJob(Data);
                }
                return Data;
            }
        }

        public async Task<CauHinhResponseModel> getOneById(int key)
        {
            using (DataAccessAdapterBase adapterBase = new DataAccessAdapterFactory().CreateAdapter())
            {
                LinqMetaData metaData = new LinqMetaData(adapterBase);
                var firt = metaData.Syscauhinh.FirstOrDefault(x => x.Id == key);
                var Data = new CauHinhResponseModel();
                Data.doanDuongDen = firt.Doanduongden;
                Data.doanDuongTu = firt.Doanduongtu;
                Data.startTime = firt.Starttime;
                Data.status = firt.Status;
                Data.endTime = firt.Endtime;
                Data.id = firt.Id;
                Data.tuanSuat = firt.Tansuat;
                return Data;
            }
        }
        
    }
}

