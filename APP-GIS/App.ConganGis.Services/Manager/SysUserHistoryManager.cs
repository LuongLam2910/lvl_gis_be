using App.CongAnGis.Dal.EntityClasses;
using App.CongAnGis.Dal.HelperClasses;
using App.CongAnGis.Dal.Linq;
using App.CongAnGis.Services;
using App.Core.Common;
using DocumentFormat.OpenXml.EMMA;
using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.JsonPatch.Internal;
using SD.LLBLGen.Pro.ORMSupportClasses;
using SD.LLBLGen.Pro.QuerySpec;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using static App.CongAnGis.Services.Models.SysCauHinhModel;
using static App.CongAnGis.Services.Models.SysUserHistory;

namespace App.ConganGis.Services.Manager
{
    public interface ISysUserHistoryManager
    {
        Task<ApiResponse> InsertAsync(SysUserHistoryModel model);
        Task<ApiResponse> InsertWait(SysUserHistoryModel model);
        Task<ApiResponse> Delete(int id);
        Task<ApiResponse<PageModelView<IEnumerable<SysUserHistoryModel>>>> Paging(SysUserHistoryPagingModel _model);
        Task<bool> createJob(string jobName);
        Task<bool> deleteJob(string jobName);
        bool createLineFromUserHistory(SysUserHistoryModel model);
        Task<ApiResponse<IEnumerable<SysUserHistoryModel>>> getOneByDate(int userId, string device, string date);
        Task<ApiResponse> getDataLine(int? userId, string device, int Id);
        Task<ApiResponse<IEnumerable<SysUserHistoryLineModel>>> getAllByUserhistoryLine(string userId, string device, string date);
        Task<ApiResponse<IEnumerable<SysUserHistoryModel>>> getAllByStatus();
    }
    public class SysUserHistoryManager : ISysUserHistoryManager
    {
        private readonly ISchedulerManager _schedulerManager;

        public SysUserHistoryManager(ISchedulerManager schedulerManager)
        {
            _schedulerManager = schedulerManager;
        }
        public async Task<bool> createJob(string jobName)
        {
            if (!string.IsNullOrEmpty(jobName))
            {
                await _schedulerManager.CreateUserHistoryJob(jobName);
            }
            return true;
        }

        public bool createLineFromUserHistory(SysUserHistoryModel model)
        {
            using (DataAccessAdapterBase adapterBase = new DataAccessAdapterFactory().CreateAdapter())
            {
                try
                {
                    var entity = new SyshistoryuserlineEntity();
                    entity.Shape = model.shape;
                    entity.Status = 1;
                    entity.Createddate = DateTime.Now;
                    entity.Username = model.userName;
                    entity.Userid = model.userId.ToString();
                    entity.Deviceinfo = model.deviceInfo;
                    entity.Device = model.model;
                    adapterBase.SaveEntity(entity);
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }

        public async Task<ApiResponse> Delete(int id)
        {
            try
            {
                using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
                {
                    SysuserhistoryEntity _SysuserhistoryEntity = new SysuserhistoryEntity(id);
                    adapter.DeleteEntity(_SysuserhistoryEntity);
                }
                return GeneralCode.Success;
            }
            catch (ORMEntityValidationException ex)
            {
                return ApiResponse.Generate(GeneralCode.Error, ex.Message);
            }
        }

        public async Task<bool> deleteJob(string jobName)
        {
            if (!string.IsNullOrEmpty(jobName))
            {
                await _schedulerManager.StopJob(jobName, "userGroup");
            }
            return true;
        }

        public async Task<ApiResponse<IEnumerable<SysUserHistoryModel>>> getAllByStatus()
        {
            using (DataAccessAdapterBase adapterBase = new DataAccessAdapterFactory().CreateAdapter())
            {
                try
                {
                    var entity = new EntityCollection<SysuserhistoryEntity>();
                    RelationPredicateBucket filter = new RelationPredicateBucket();
                    filter.PredicateExpression.AddWithAnd(SysuserhistoryFields.Status == 1);
                    adapterBase.FetchEntityCollection(entity, filter);
                    var data = entity.Select(c => new SysUserHistoryModel
                    {
                        id = c.Id,
                        createDate = c.Createdate,
                        deviceInfo = c.Deviceinfo,
                        icon = c.Icon,
                        model = c.Model,
                        shape = c.Shape,
                        status = c.Status,
                        userId = c.Userid,
                        userName = c.Username
                    }).ToList();
                    return ApiResponse<IEnumerable<SysUserHistoryModel>>.Generate(data, GeneralCode.Success, "SUCCESS");
                }
                catch (Exception ex)
                {
                    return ApiResponse<IEnumerable<SysUserHistoryModel>>.Generate(GeneralCode.Error, ex.Message);
                }

            }
        }

        public async Task<ApiResponse<IEnumerable<SysUserHistoryModel>>> getOneByDate(int userId, string device, string date)
        {
            using (DataAccessAdapterBase adapterBase = new DataAccessAdapterFactory().CreateAdapter())
            {
                try
                {
                    RelationPredicateBucket filter = new RelationPredicateBucket();
                    var data = new EntityCollection<SysuserhistoryEntity>();
                    var search = "%" + date + "%";
                    var pred1 = (SysuserhistoryFields.Userid).Equal(userId).And(SysuserhistoryFields.Model == device).And(SysuserhistoryFields.Createdate.Like(search));
                    filter.PredicateExpression.Add(pred1);
                    adapterBase.FetchEntityCollection(data, filter);
                    var list = data.Select(c => new SysUserHistoryModel
                    {
                        id = c.Id,
                        shape = c.Shape,
                        userId = c.Userid,
                        userName = c.Username,
                        status = c.Status,
                        createDate = c.Createdate,
                        icon = c.Icon,
                        deviceInfo = c.Deviceinfo,
                        model = c.Model
                    }).ToList();
                    return ApiResponse<IEnumerable<SysUserHistoryModel>>.Generate(list, GeneralCode.Success, "SUCCESS");
                }
                catch (Exception ex)
                {
                    return ApiResponse<IEnumerable<SysUserHistoryModel>>.Generate(GeneralCode.Error, ex.Message);
                }
                
            }
        }

        public async Task<ApiResponse<IEnumerable<SysUserHistoryLineModel>>> getAllByUserhistoryLine(string userId, string device, string date)
        {
            using (DataAccessAdapterBase adapter = new DataAccessAdapterFactory().CreateAdapter())
            {
                try
                {
                   RelationPredicateBucket filter = new RelationPredicateBucket();
                    var data = new EntityCollection<SyshistoryuserlineEntity>();
                    DateTime enteredDate = DateTime.Parse(date);
                    var pred1 = (SyshistoryuserlineFields.Userid).Equal(userId).And(SyshistoryuserlineFields.Device == device).And(SyshistoryuserlineFields.Createddate.Equal(enteredDate));
                    filter.PredicateExpression.Add(pred1);
                    adapter.FetchEntityCollection(data, filter);
                    var list = data.Select(x => new SysUserHistoryLineModel
                    {
                        id = x.Id,
                        deviceInfo = x.Deviceinfo,
                        shape = x.Shape,
                        status = x.Status,
                        createDate = x.Createddate,
                        icon = x.Icon,
                        userName = x.Username,
                        model = x.Device,
                        userId = x.Userid,
                        mahuyen = x.Mahuyen,
                        maxa = x.Maxa,
                    }).ToList();
                    return ApiResponse<IEnumerable<SysUserHistoryLineModel>>.Generate(list, GeneralCode.Success, "SUCCESS");
 ;
                }
                catch (Exception ex)
                {
                    return ApiResponse<IEnumerable<SysUserHistoryLineModel>>.Generate(GeneralCode.Error, ex.Message);
                }
            }
        }

        public async Task<ApiResponse> getDataLine(int? userId, string device, int Id)
        {
            using (DataAccessAdapterBase adapterBase = new DataAccessAdapterFactory().CreateAdapter())
            {
                try
                {
                    RelationPredicateBucket filter = new RelationPredicateBucket();
                    var data = new EntityCollection<SysuserhistoryEntity>();
                    var pred1 = (SysuserhistoryFields.Userid).Equal(userId).And(SysuserhistoryFields.Model == device).And(SysuserhistoryFields.Id <= Id).And(SysuserhistoryFields.Status != 3);
                    filter.PredicateExpression.Add(pred1);
                    adapterBase.FetchEntityCollection(data, filter);
                    var model = new SysUserHistoryModel();
                    model.userId = data[0].Userid;
                    model.userName = data[0].Username;
                    model.model = data[0].Model;
                    model.deviceInfo = data[0].Deviceinfo;
                    var shape = "LINESTRING ( " + data[0].Shape.Replace(",", " ");
                    SysuserhistoryEntity entity = new SysuserhistoryEntity(data[0].Id);
                    if (adapterBase.FetchEntity(entity))
                    {
                        entity.Status = 3;
                        adapterBase.SaveEntity(entity);
                    }
                    for (int i = 1; i < data.Count; i++)
                    {
                        SysuserhistoryEntity _SysfieldEntity = new SysuserhistoryEntity(data[i].Id);
                        if (adapterBase.FetchEntity(_SysfieldEntity))
                        {
                            _SysfieldEntity.Status = 3;
                            adapterBase.SaveEntity(_SysfieldEntity);
                        }
                        var check = data[i].Shape == data[i - 1].Shape ? true : false;
                        if (!check)
                        {
                            shape += ", " + data[i].Shape.Replace(",", " ");
                        }
                    }
                    shape += ")";
                    model.shape = shape;
                    var checkc = createLineFromUserHistory(model);
                    adapterBase.Commit();
                    return ApiResponse.Generate(GeneralCode.Success, "SUCCESS");
                }
                catch (Exception ex)
                {
                    return ApiResponse.Generate(GeneralCode.Error, ex.Message);
                }

            }
        }
        public async Task<ApiResponse> InsertAsync(SysUserHistoryModel model)
        {
            try
            {
                using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
                {
                    LinqMetaData metadata = new LinqMetaData(adapter);
                    var _SysUserHistoryEntity = new SysuserhistoryEntity();
                    _SysUserHistoryEntity.Userid = model.userId;
                    _SysUserHistoryEntity.Username = model.userName;
                    _SysUserHistoryEntity.Createdate = DateTime.Now.ToString();
                    _SysUserHistoryEntity.Shape = model.shape;
                    _SysUserHistoryEntity.Status = model.status;
                    _SysUserHistoryEntity.Deviceinfo = model.deviceInfo;
                    _SysUserHistoryEntity.Model = model.model;
                    adapter.SaveEntity(_SysUserHistoryEntity, true);
                    return GeneralCode.Success;
                }
            }
            catch (ORMEntityValidationException ex)
            {
                return ApiResponse.Generate(GeneralCode.Error, ex.Message);
            }
        }

        public async Task<ApiResponse> InsertWait(SysUserHistoryModel model)
        {
            try
            {
                using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
                {
                    LinqMetaData metadata = new LinqMetaData(adapter);
                    var _SysUserHistoryEntity = new SysuserhistoryEntity();
                    _SysUserHistoryEntity.Userid = model.userId;
                    _SysUserHistoryEntity.Username = model.userName;
                    _SysUserHistoryEntity.Createdate = DateTime.Now.ToString();
                    _SysUserHistoryEntity.Shape = model.shape;
                    _SysUserHistoryEntity.Status = model.status;
                    _SysUserHistoryEntity.Deviceinfo = model.deviceInfo;
                    _SysUserHistoryEntity.Model = model.model;
                    adapter.SaveEntity(_SysUserHistoryEntity, true);
                    if (model.status == 2)
                    {
                        var jobName = model.userName + "-" + model.userId;
                        getDataLine(model.userId, model.model, _SysUserHistoryEntity.Id);
                        deleteJob(jobName);
                    }
                    return GeneralCode.Success;
                }
            }
            catch (ORMEntityValidationException ex)
            {
                return ApiResponse.Generate(GeneralCode.Error, ex.Message);
            }
        }

        public async Task<ApiResponse<PageModelView<IEnumerable<SysUserHistoryModel>>>> Paging(SysUserHistoryPagingModel _model)
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
                        var data = new EntityCollection<SysuserhistoryEntity>();
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
                        SortExpression sort = new SortExpression(SysuserhistoryFields.Id | SortOperator.Descending);
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
                            return ApiResponse<PageModelView<IEnumerable<SysUserHistoryModel>>>.Generate(null, GeneralCode.Error, ex.Message);
                        }
                        return new PageModelView<IEnumerable<SysUserHistoryModel>>()
                        {
                            Data = data.Select(c => new SysUserHistoryModel
                            {
                                id = c.Id,
                                shape = c.Shape,
                                userId = c.Userid,
                                userName = c.Username,
                                status = c.Status,
                                createDate = c.Createdate,
                                deviceInfo = c.Deviceinfo,
                                model = c.Model,
                                icon = c.Icon
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
                return ApiResponse<PageModelView<IEnumerable<SysUserHistoryModel>>>.Generate(null, GeneralCode.Error, ex.Message);
            }
        }
    }
}
