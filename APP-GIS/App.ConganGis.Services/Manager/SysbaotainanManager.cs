
using App.ConganGis.Services.ManagerBase;
using App.CongAnGis.Dal.EntityClasses;
using App.CongAnGis.Dal.HelperClasses;
using App.CongAnGis.Services.Hubs;
using App.Core.Common;
using BingMapsRESTToolkit;
using Microsoft.AspNetCore.SignalR;
using SD.LLBLGen.Pro.ORMSupportClasses;
using SD.LLBLGen.Pro.QuerySpec;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using static App.ConganGis.Services.Model.SysBaoTaiNanModel;
using static App.CongAnGis.Services.Models.SysBaoChayModel;
using static App.Core.Common.Constants;

namespace App.CongAnGis.Services.Manager
{
    public interface ISysbaotainanManager
    {
        Task<ApiResponse<PageModelView<IEnumerable<SysbaotainanVM.ItemSysbaotainan>>>> Paging(SysbaotainanModel _model);
        Task<ApiResponse> UpdateAsync(SysbaotainanVM.ItemSysbaotainan _Model);
        Task<ApiResponse<IEnumerable<BaoTaiNanModel>>> getAllBaoTaiNan();
        Task<ApiResponse> InsertCustomAsync(BaoTaiNanModel _Model, IHubContext<SignalR> hubContext);
        string GetLngLat(string Address);

    }
    public class SysbaotainanManager : SysbaotainanManagerBase, ISysbaotainanManager
    {
        private readonly ICurrentContext _currentContext;
        public SysbaotainanManager(ICurrentContext currentContext)
        {
            _currentContext = currentContext;
        }

        public string GetLngLat(string Address)
        {
            var request = new GeocodeRequest();
            request.BingMapsKey = "AvZ5DAID8daT8zrXD66O4AoOrX0495feAd_sjSHTfMNHrrQhpfKZp4sfK-UPPuXP";

            request.Query = Address;

            var result = request.Execute();
            if (result.Result.StatusCode == 200)
            {
                var toolkitLocation = result?.Result.ResourceSets?.FirstOrDefault()
                        ?.Resources?.FirstOrDefault()
                    as Location;
                var latitude = toolkitLocation.Point.Coordinates[0];
                var longitude = toolkitLocation.Point.Coordinates[1];
                return "POINT (" + longitude + " " + latitude + ")";
            }
            else
            {
                return null;
            }

        }
        public async Task<ApiResponse> UpdateAsync(SysbaotainanVM.ItemSysbaotainan _Model)
        {
            try
            {
                return await Task.Run(() =>
                {
                    SysbaotainanEntity _SysbaotainanEntity = configVMtoEntity.CreateMapper().Map<SysbaotainanEntity>(_Model);
                    Update(_SysbaotainanEntity);
                    return GeneralCode.Success;
                });
            }
            catch (Exception ex)
            {
                return ApiResponse.Generate(GeneralCode.Error, ex.Message);
            }
        }

        public async Task<ApiResponse<IEnumerable<BaoTaiNanModel>>> getAllBaoTaiNan()
        {
            try
            {
                return await Task.Run(() =>
                {
                    EntityCollection<SysbaotainanEntity> _Collection = new EntityCollection<SysbaotainanEntity>();
                    RelationPredicateBucket filter = new RelationPredicateBucket();

                    filter.PredicateExpression.AddWithOr(SysbaotainanFields.Status == 1);
                    filter.PredicateExpression.AddWithOr(SysbaotainanFields.Status == 0);

                    using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
                    {
                        adapter.FetchEntityCollection(_Collection, filter);
                        var data = _Collection.Select(c => new BaoTaiNanModel
                        {
                            id = c.Id,
                            address = c.Address,
                            createby = c.Createby,
                            iddata = c.Iddata,
                            shape = c.Shape,
                            phonenumber = c.Phonenumber,
                            reasonfire = c.Reasonfire,
                            status = c.Status,
                            updateAt = c.Updateat,
                            tablename = c.Tablename,
                            createdate = c.Createdate
                        });
                        return ApiResponse<IEnumerable<BaoTaiNanModel>>.Generate(data, GeneralCode.Success, "SUCCESS");
                    };

                });
            }
            catch (Exception ex)
            {
                return ApiResponse<IEnumerable<BaoTaiNanModel>>.Generate(GeneralCode.Error, ex.Message);
            }
        }
        public async Task<ApiResponse> InsertCustomAsync(BaoTaiNanModel _Model, IHubContext<SignalR> hubContext)
        {
            try
            {
                return await Task.Run(() =>
                {
                    var data = new SysbaotainanEntity();
                    data.Iddata = _Model.iddata;

                    if (System.String.IsNullOrEmpty(_Model.shape))
                    {
                        //data.Shape = getLngLat(_Model.address);
                    }
                    else
                    {
                        data.Shape = _Model.shape;
                    }
                    data.Tablename = _Model.tablename;
                    data.Status = _Model.status;
                    data.Createdate = _Model.createdate;
                    data.Createby = _Model.createby;
                    data.Address = _Model.address;
                    data.Phonenumber = _Model.phonenumber;
                    data.Reasonfire = _Model.reasonfire;
                    data.Icon = _Model.icon;
                    data.Maxa = _Model.maxa;
                    data.Mahuyen = _Model.mahuyen;
                    data.Nguoitiepnhan = _Model.nguoitiepnhan;
                    if (_Model.id > 0)
                    {
                        data.Id = _Model.id;

                        data.Updateat = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        Update(data);
                        var info = new BaoTaiNanModel
                        {
                            id = data.Id,
                            status = data.Status,
                            address = data.Address,
                            updateAt = data.Updateat,
                            createdate = data.Createdate,
                            shape = data.Shape,
                            createby = data.Createby,
                            phonenumber = data.Phonenumber,
                            iddata = data.Iddata,
                            reasonfire = data.Reasonfire,
                            tablename = _Model.tablename,
                            icon = data.Icon,
                            mahuyen = data.Mahuyen,
                            maxa = data.Maxa,
                            nguoitiepnhan = data.Nguoitiepnhan
                        };
                        LoggingGisEntity.Log(entity: data, userId: _currentContext.UserId, actionName: UserAction.Update, comment: "Sửa bản ghi", IpClient: _currentContext.IpClient, userName: _currentContext.UserName, entittyName: "SysbaochayEntity");
                        hubContext.Clients.All.SendAsync("CapNhatBaoTaiNan", System.Text.Json.JsonSerializer.Serialize(info));
                    }
                    else
                    {
                        data.Createdate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        Insert(data);
                        var info = new BaoTaiNanModel
                        {
                            id = data.Id,
                            status = data.Status,
                            address = data.Address,
                            updateAt = data.Updateat,
                            createdate = data.Createdate,
                            shape = data.Shape,
                            createby = data.Createby,
                            phonenumber = data.Phonenumber,
                            iddata = data.Iddata,
                            reasonfire = data.Reasonfire,
                            tablename = _Model.tablename,
                            icon = data.Icon,
                            mahuyen = data.Mahuyen,
                            maxa = data.Maxa,
                            nguoitiepnhan = data.Nguoitiepnhan
                        };
                        LoggingGisEntity.Log(entity: data, userId: _currentContext.UserId, actionName: UserAction.Insert, comment: "Thêm mới bản ghi", IpClient: _currentContext.IpClient, userName: _currentContext.UserName, entittyName: "SysbaochayEntity");
                        hubContext.Clients.All.SendAsync("ThemBaoChay", System.Text.Json.JsonSerializer.Serialize(info));
                    }

                    return GeneralCode.Success;
                });

            }
            catch (Exception ex)
            {
                return ApiResponse.Generate(GeneralCode.Error, ex.Message);
            }
        }
        public async Task<ApiResponse<PageModelView<IEnumerable<SysbaotainanVM.ItemSysbaotainan>>>> Paging(SysbaotainanModel _model)
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
                        var data = new EntityCollection<SysbaotainanEntity>();
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
                        SortExpression sort = new SortExpression(SysbaotainanFields.Id | SortOperator.Descending);
                        if (_model.ListCondition?.status != null)
                        {
                            filter.PredicateExpression.AddWithAnd(SysbaotainanFields.Status == _model.ListCondition.status);
                            if (_model.ListCondition.time != null)
                            {
                                var pr = SortOperator.Descending;
                                if (_model.ListCondition.sort == 0)
                                {
                                    pr = SortOperator.Descending;
                                }
                                else
                                {
                                    pr = SortOperator.Ascending;
                                }
                                switch (_model.ListCondition.time)
                                {
                                    case 0:
                                        sort = new SortExpression(SysbaotainanFields.Updateat | pr);
                                        break;
                                    case 1:
                                        sort = new SortExpression(SysbaotainanFields.Createdate | pr);
                                        break;
                                    case 2:
                                        sort = new SortExpression(SysbaotainanFields.Updateat | pr);
                                        filter.PredicateExpression.AddWithAnd(SysbaotainanFields.Status == _model.ListCondition.time);
                                        break;
                                }
                            }
                            if (_model.ListCondition.time != null)
                            {
                                filter.PredicateExpression.AddWithAnd(SysbaotainanFields.Status == _model.ListCondition.status);
                            }
                        }
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
                            return ApiResponse<PageModelView<IEnumerable<SysbaotainanVM.ItemSysbaotainan>>>.Generate(null, GeneralCode.Error, ex.Message);
                        }
                        return new PageModelView<IEnumerable<SysbaotainanVM.ItemSysbaotainan>>()
                        {
                            Data = data.Select(c => configEntitytoVM.CreateMapper().Map<SysbaotainanEntity, SysbaotainanVM.ItemSysbaotainan>(c)).ToList(),
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
                return ApiResponse<PageModelView<IEnumerable<SysbaotainanVM.ItemSysbaotainan>>>.Generate(null, GeneralCode.Error, ex.Message);
            }
        }

    }
}

