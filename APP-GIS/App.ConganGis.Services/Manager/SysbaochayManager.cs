
using App.CongAnGis.Dal.EntityClasses;
using App.CongAnGis.Dal.FactoryClasses;
using App.CongAnGis.Dal.HelperClasses;
using App.CongAnGis.Services.ManagerBase;
using App.Core.Common;
using DocumentFormat.OpenXml.VariantTypes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using SD.LLBLGen.Pro.ORMSupportClasses;
using SD.LLBLGen.Pro.QuerySpec;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using static App.CongAnGis.Services.Models.SysBaoChayModel;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Microsoft.AspNetCore.SignalR;
using App.CongAnGis.Services.Hubs;
using Newtonsoft.Json;
using System.Data;
using System.Text;
using Microsoft.AspNet.SignalR.Json;
using System.Text.Json;
using DocumentFormat.OpenXml.EMMA;
using GoogleMaps.LocationServices;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using System.Net.Http;
using BingMapsRESTToolkit;
using static App.Core.Common.Constants;
using App.CongAnGis.Services.Models;

namespace App.CongAnGis.Services.Manager
{
    public interface ISysbaochayManager
    {
        Task<ApiResponse<PageModelView<IEnumerable<SysbaochayVM.ItemSysbaochay>>>> Paging(SysbaochayModel _model);
        Task<ApiResponse> UpdateAsync(SysbaochayVM.ItemSysbaochay _Model);
        Task<ApiResponse<IEnumerable<BaoChayModel>>> getAllBaoChay();
        Task<ApiResponse> InsertCustomAsync(BaoChayModel _Model, IHubContext<SignalR> hubContext);
        string getLngLat(string Address);

    }
    public class SysbaochayManager : SysbaochayManagerBase , ISysbaochayManager
    {
        private readonly ICurrentContext _currentContext;
        public SysbaochayManager(ICurrentContext currentContext)
        {
            _currentContext = currentContext;
        }

        public string getLngLat(string Address)
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
                return "POINT (" + longitude +" "+ latitude + ")";
            } else
            {
                return null;
            }

        }
        public async Task<ApiResponse> UpdateAsync(SysbaochayVM.ItemSysbaochay _Model)
        {
            try
            {
                return await Task.Run(() =>
                {
                    SysbaochayEntity _SysbaochayEntity = configVMtoEntity.CreateMapper().Map<SysbaochayEntity>(_Model);
                    Update(_SysbaochayEntity);
                    return GeneralCode.Success;
                });
            }
            catch (Exception ex)
            {
                return ApiResponse.Generate(GeneralCode.Error, ex.Message);
            }
        }

        public async Task<ApiResponse<IEnumerable<BaoChayModel>>> getAllBaoChay()
        {
            try
            {
                return await Task.Run(() =>
                {
                    EntityCollection<SysbaochayEntity> _Collection = new EntityCollection<SysbaochayEntity>();
                    RelationPredicateBucket filter = new RelationPredicateBucket();

                    filter.PredicateExpression.AddWithOr(SysbaochayFields.Status == 1);
                    filter.PredicateExpression.AddWithOr(SysbaochayFields.Status == 0);

                    using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
                    {
                        adapter.FetchEntityCollection(_Collection, filter);
                        var data = _Collection.Select(c => new BaoChayModel
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
                        return ApiResponse<IEnumerable<BaoChayModel>>.Generate(data, GeneralCode.Success, "SUCCESS");
                    };

                });
            }
            catch (Exception ex)
            {
                return ApiResponse<IEnumerable<BaoChayModel>>.Generate(GeneralCode.Error, ex.Message);
            }
        }

        public async Task<ApiResponse> InsertCustomAsync(BaoChayModel _Model, IHubContext<SignalR> hubContext)
        {
            try
            {
                return await Task.Run(() =>
                {
                    var data = new SysbaochayEntity();
                    data.Iddata = _Model.iddata;
                    
                    if (System.String.IsNullOrEmpty(_Model.shape))
                    {
                        //data.Shape = getLngLat(_Model.address);
                    } else
                    {
                        data.Shape = _Model.shape;
                    }
                    
                    data.Tablename = _Model.tablename;
                    data.Status = _Model.status;
                    data.Createby = _Model.createby;
                    data.Address = _Model.address;
                    data.Phonenumber = _Model.phonenumber;
                    data.Reasonfire = _Model.reasonfire;
                    data.Icon = _Model.icon;
                    
                    if (_Model.id > 0)
                    {
                        data.Id = _Model.id;
                        
                        data.Updateat = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        data.Createdate = _Model.createdate;
                        Update(data);
                        if (data.Iddata != null && data.Status == 2)
                        {
                            using (DataAccessAdapterBase adapterBase = new DataAccessAdapterFactory().CreateAdapter())
                            {
                                string sql = "update " + adapterBase.CatalogNameToUse + _Model.tablename + " set status = 1 where id = " + data.Iddata;
                                try
                                {
                                    adapterBase.ExecuteSQLAsync(sql);
                                    adapterBase.Commit();
                                }
                                catch (Exception)
                                {
                                    Console.WriteLine(sql);
                                }
                                
                            }
                        }
                        var info = new BaoChayModel
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
                        };
                        LoggingGisEntity.Log(entity: data, userId: _currentContext.UserId, actionName: UserAction.Update, comment: "Sửa bản ghi", IpClient: _currentContext.IpClient, userName: _currentContext.UserName, entittyName: "SysbaochayEntity");
                        hubContext.Clients.All.SendAsync("CapNhatBaoChay", System.Text.Json.JsonSerializer.Serialize(info));
                    }
                    else
                    {
                        data.Createdate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        Insert(data);
                        if (data.Iddata != null)
                        {
                            using (DataAccessAdapterBase adapterBase = new DataAccessAdapterFactory().CreateAdapter())
                            {
                                string sql = "update " + adapterBase.CatalogNameToUse +"."+ _Model.tablename + " set status = 0 where id = " + data.Iddata;
                                adapterBase.ExecuteSQLAsync(sql);
                                adapterBase.Commit();
                            }
                        }

                        var info = new BaoChayModel
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
        public async Task<ApiResponse<PageModelView<IEnumerable<SysbaochayVM.ItemSysbaochay>>>> Paging(SysbaochayModel _model)
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
                        var data = new EntityCollection<SysbaochayEntity>();
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
                        SortExpression sort = new SortExpression(SysbaochayFields.Id | SortOperator.Descending);
                        if (_model.ListCondition?.status != null)
                        {
                            filter.PredicateExpression.AddWithAnd(SysbaochayFields.Status == _model.ListCondition.status);
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
                                        sort = new SortExpression(SysbaochayFields.Updateat | pr);
                                        break;
                                    case 1:
                                        sort = new SortExpression(SysbaochayFields.Createdate | pr);
                                        break;
                                    case 2:
                                        sort = new SortExpression(SysbaochayFields.Updateat | pr);
                                        filter.PredicateExpression.AddWithAnd(SysbaochayFields.Status == _model.ListCondition.time);
                                        break;
                                }
                            }
                            if (_model.ListCondition.time != null)
                            {
                                filter.PredicateExpression.AddWithAnd(SysbaochayFields.Status == _model.ListCondition.status);
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
                            return ApiResponse<PageModelView<IEnumerable<SysbaochayVM.ItemSysbaochay>>>.Generate(null, GeneralCode.Error, ex.Message);
                        }
                        return new PageModelView<IEnumerable<SysbaochayVM.ItemSysbaochay>>()
                        {
                            Data = data.Select(c => configEntitytoVM.CreateMapper().Map<SysbaochayEntity, SysbaochayVM.ItemSysbaochay>(c)).ToList(),
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
                return ApiResponse<PageModelView<IEnumerable<SysbaochayVM.ItemSysbaochay>>>.Generate(null, GeneralCode.Error, ex.Message);
            }
        }
    }
}

