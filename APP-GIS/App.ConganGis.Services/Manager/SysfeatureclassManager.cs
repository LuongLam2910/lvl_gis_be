
using App.CongAnGis.Dal.DatabaseSpecific;
using App.CongAnGis.Dal.EntityClasses;
using App.CongAnGis.Dal.FactoryClasses;
using App.CongAnGis.Dal.HelperClasses;
using App.CongAnGis.Dal.Linq;
using App.CongAnGis.Services.ManagerBase;
using App.Core.Common;
using DocumentFormat.OpenXml.EMMA;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Options;
using SD.LLBLGen.Pro.ORMSupportClasses;
using SD.LLBLGen.Pro.QuerySpec;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static App.CongAnGis.Services.Manager.SysfeatureclassManager.sysFeatureClassModel;
using static App.Core.Common.Constants;

namespace App.CongAnGis.Services.Manager
{
    public interface ISysfeatureclassManager
    {
        public Task<ApiResponse> InsertCustom(SysfeatureclassVM.ItemSysfeatureclass _Model);
        public Task<ApiResponse> DeleteMulti(int id);
        public Task<ApiResponse> DeleteData(int key);
        public Task<ApiResponse<List<SysfeatureclassVM.ItemSysfeatureclass>>> SelectAllAsync();
        Task<ApiResponse<PageModelView<IEnumerable<SysfeatureclassVM.ItemSysfeatureclass>>>> SelectPagingCustom(SysfeatureclassVM.PageSysfeatureclass _model);
    }

    public class SysfeatureclassManager : SysfeatureclassManagerBase, ISysfeatureclassManager
    {
        public class sysFeatureClassModel
        {
            public class FieldConstants
            {
                public const string STATUS = "status";
                public const string ICON = "icon";
                public const string DIEUKIEN = "dieukien";
                public const string CREATEDDATE = "createddate";
                public const string URL = "URL";
                public const string TypeData = "typedata";
                public const string MAHUYEN = "mahuyen";
                public const string MAXA = "maxa";
                public const string UNITCODE = "unitcode";

                public const string STATUS_DATA_TYPE = "int4 DEFAULT 1";
                public const string DIEUKIEN_TYPE = "int4 DEFAULT 0";
                public const string URL_DATA_TYPE = "text";
                public const string TypeData_DATA_TYPE = "int4 DEFAULT 0";
                public const string MAHUYEN_DATA_TYPE = "text";
                public const string CREATEDDATE_TYPE = "DATE DEFAULT now()";
                public const string ICON_DATA_TYPE = "varchar";

            }
        }
        private readonly SysfeatureclassManagerBase _managerBase;
        private readonly AppSettingModel _appSetting;
        private readonly ICurrentContext _currentContext;
        public SysfeatureclassManager(IOptionsSnapshot<AppSettingModel> appConfig, ICurrentContext currentContext)
        {
            _managerBase = new SysfeatureclassManagerBase();
            _appSetting = appConfig.Value;
            _currentContext = currentContext;
        }

        public async Task<ApiResponse<List<SysfeatureclassVM.ItemSysfeatureclass>>> SelectAllAsync()
        {
            try
            {
                return await Task.Run(() =>
                {

                    EntityCollection<SysfeatureclassEntity> _Collection = SelectAll();
                    List<SysfeatureclassVM.ItemSysfeatureclass> toReturn = _Collection.Select(c => configEntitytoVM.CreateMapper().Map<SysfeatureclassEntity, SysfeatureclassVM.ItemSysfeatureclass>(c)).ToList();

                    return ApiResponse<List<SysfeatureclassVM.ItemSysfeatureclass>>.Generate(toReturn, GeneralCode.Success);

                });

            }
            catch (ORMException ex)
            {
                return ApiResponse<List<SysfeatureclassVM.ItemSysfeatureclass>>.Generate(null, GeneralCode.Error, ex.Message);
            }
        }

        public async Task<ApiResponse> InsertCustom(SysfeatureclassVM.ItemSysfeatureclass _Model)
        {
            using (DataAccessAdapter adapter = new DataAccessAdapterFactory().CreateAdapter())
            {
                try
                {
                    SysfeatureclassEntity _SysfeatureclassEntity = configVMtoEntity.CreateMapper().Map<SysfeatureclassEntity>(_Model);
                    
                    string sql = "create table appcongangis." + _Model.Tablename + " (id int4 PRIMARY KEY NOT NULL GENERATED ALWAYS AS IDENTITY,shape text)";
                    adapter.ExecuteSQL(sql);
                    _managerBase.Insert(_SysfeatureclassEntity);
                    LoggingGisEntity.LogCustom(entity: _Model.Tablename, userId: _currentContext.UserId, actionName: UserAction.Insert, comment: "Thêm mới bảng" + _Model.Tablename, IpClient: _currentContext.IpClient, userName: _currentContext.UserName, entittyName: _Model.Tablename);
                    LoggingGisEntity.Log(entity: _SysfeatureclassEntity, userId: _currentContext.UserId, actionName: UserAction.Insert, comment: "Thêm mới bản ghi", IpClient: _currentContext.IpClient, userName: _currentContext.UserName, entittyName: "SysfeatureclassEntity");
                    adapter.Commit();
                    CreateDefaultFields(_Model.Tablename, _SysfeatureclassEntity);


                    //Add default 2 field

                    return GeneralCode.Success;
                }
                catch (Exception ex)
                {
                    string message = ex.Message.Contains("already exists") ? 
                        "Bảng " + _Model.Tablename + " đã tồn tại vui lòng nhập tên bảng khác!"
                        :
                        ex.Message;

                    return ApiResponse.Generate(GeneralCode.Error, message);
                }
            }
        }
        public async Task<ApiResponse<PageModelView<IEnumerable<SysfeatureclassVM.ItemSysfeatureclass>>>> SelectPagingCustom(SysfeatureclassVM.PageSysfeatureclass _model)
        {
            try
            {
                return await Task.Run(() =>
                {
                    RelationPredicateBucket filter = new RelationPredicateBucket();
                    if (!string.IsNullOrWhiteSpace(_model.strKey))
                    {
                        _model.strKey = "%" + _model.strKey + "%";
                        var pred1 = (SysfeatureclassFields.Name).Like(_model.strKey)
                            .Or(SysfeatureclassFields.Tablename.Like(_model.strKey)).Or(SysfeatureclassFields.Description.Like(_model.strKey)).Or(SysfeatureclassFields.Prj.Like(_model.strKey)).Or(SysfeatureclassFields.Geotype.Like(_model.strKey)).Or(SysfeatureclassFields.Unitcode.Like(_model.strKey));
                        filter.PredicateExpression.Add(pred1);
                    }
                    filter.PredicateExpression.AddWithAnd(SysfeatureclassFields.Config != "2");
                    using (DataAccessAdapterBase adapter = new DataAccessAdapterFactory().CreateAdapter())
                    {
                        var data = new EntityCollection<SysfeatureclassEntity>();
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
                        SortExpression sort = new SortExpression(SysfeatureclassFields.Id | SortOperator.Descending);
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
                            return ApiResponse<PageModelView<IEnumerable<SysfeatureclassVM.ItemSysfeatureclass>>>.Generate(null, GeneralCode.Error, ex.Message);
                        }
                        return new PageModelView<IEnumerable<SysfeatureclassVM.ItemSysfeatureclass>>()
                        {
                            Data = data.Select(c => configEntitytoVM.CreateMapper().Map<SysfeatureclassEntity, SysfeatureclassVM.ItemSysfeatureclass>(c)).ToList(),
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
                return ApiResponse<PageModelView<IEnumerable<SysfeatureclassVM.ItemSysfeatureclass>>>.Generate(null, GeneralCode.Error, ex.Message);
            }
        }
        public async Task<ApiResponse> DeleteMulti(int id)
        {
            try
            {
                return await Task.Run(() =>
                {
                    using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
                    {
                        SysfeatureclassEntity _SysfeatureclassEntity = new SysfeatureclassEntity(id);
                        var toReturn = new EntityCollection<SysfieldEntity>();
                        
                       
                        var metaData = new LinqMetaData(adapter);
                        string app = adapter.CatalogNameToUse + ".";
                        var tableName = metaData.Sysfeatureclass.FirstOrDefault(x => x.Id == id).Tablename;
                        string sql = "DROP TABLE IF EXISTS " + app + tableName;
                        if (adapter.FetchEntity(_SysfeatureclassEntity))
                        {
                            LoggingGisEntity.Log(entity: _SysfeatureclassEntity, userId: _currentContext.UserId, actionName: UserAction.Delete, comment: "Xóa bản ghi", IpClient: _currentContext.IpClient, userName: _currentContext.UserName, entittyName: "SysfeatureclassEntity");
                            var parameters = new QueryParameters()
                            {
                                CollectionToFetch = toReturn,
                                FilterToUse = SysfieldFields.Featureclass == id
                            };
                            adapter.FetchEntityCollection(parameters);
                            foreach (var item in toReturn)
                            {
                                LoggingGisEntity.Log(entity: item, userId: _currentContext.UserId, actionName: UserAction.Delete, comment: "Xóa bản ghi", IpClient: _currentContext.IpClient, userName: _currentContext.UserName, entittyName: "SysfieldEntity");
                            };
                            LoggingGisEntity.LogCustom(entity: null, userId: _currentContext.UserId, actionName: UserAction.Delete, comment: "Xóa bảng" + tableName, IpClient: _currentContext.IpClient, userName: _currentContext.UserName, entittyName: tableName);
                            adapter.ExecuteSQL(sql);
                            adapter.DeleteEntity(_SysfeatureclassEntity);
                            adapter.DeleteEntityCollection(toReturn);
                            adapter.Commit();
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

        public async Task<ApiResponse> DeleteData(int key)
        {
            try
            {
                using (DataAccessAdapter adapter = new DataAccessAdapterFactory().CreateAdapter())
                {

                    string app = "\"appcongangis\".";
                    var metaData = new LinqMetaData(adapter);
                    var fe = metaData.Sysfeatureclass.First(c => c.Id == key);
                    string sql = "delete from " +app+ fe.Tablename;
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

        private void CreateDefaultFields(string tablename, SysfeatureclassEntity fe)
        {
            using (DataAccessAdapter adapter = new DataAccessAdapterFactory().CreateAdapter())
            {
                var uow = new UnitOfWork2();
                EntityCollection<SysfieldEntity> _Collection = new EntityCollection<SysfieldEntity>();


                //CreatedDate
                SysfieldEntity CreatedDate_field = new SysfieldEntity();
                CreatedDate_field.Featureclass = fe.Id;
                CreatedDate_field.Name = "Ngày tạo";
                CreatedDate_field.Config = null;
                CreatedDate_field.Fieldname = FieldConstants.CREATEDDATE;
                CreatedDate_field.Datatype = FieldConstants.CREATEDDATE_TYPE;
                CreatedDate_field.Datalength = 1 + "";
                CreatedDate_field.Show = 1000;
                CreatedDate_field.Usercreated = _currentContext.UserId;
                CreatedDate_field.Status = 0;
                LoggingGisEntity.Log(entity: CreatedDate_field, userId: _currentContext.UserId, actionName: UserAction.Insert, comment: "Thêm mới bản ghi", IpClient: _currentContext.IpClient, userName: _currentContext.UserName, entittyName: "SysfieldEntity");
                _Collection.Items.Add(CreatedDate_field);

                //Status
                SysfieldEntity status_field = new SysfieldEntity();
                status_field.Featureclass = fe.Id;
                status_field.Name = "Status";
                status_field.Config = null;
                status_field.Fieldname = FieldConstants.STATUS;
                status_field.Datatype = FieldConstants.STATUS_DATA_TYPE;
                status_field.Datalength = 255 + "";
                status_field.Show = 1000;
                status_field.Usercreated = _currentContext.UserId;
                status_field.Status = 0;
                LoggingGisEntity.Log(entity: status_field, userId: _currentContext.UserId, actionName: UserAction.Insert, comment: "Thêm mới bản ghi", IpClient: _currentContext.IpClient, userName: _currentContext.UserName, entittyName: "SysfieldEntity");
                _Collection.Items.Add(status_field);

                //TypeData
                SysfieldEntity TypeData_field = new SysfieldEntity();
                TypeData_field.Featureclass = fe.Id;
                TypeData_field.Name = "Loại dữ liệu";
                TypeData_field.Config = null;
                TypeData_field.Fieldname = FieldConstants.TypeData;
                TypeData_field.Datatype = FieldConstants.TypeData_DATA_TYPE;
                TypeData_field.Datalength = 255 + "";
                TypeData_field.Show = 1000;
                TypeData_field.Usercreated = _currentContext.UserId;
                TypeData_field.Status = 0;
                LoggingGisEntity.Log(entity: TypeData_field, userId: _currentContext.UserId, actionName: UserAction.Insert, comment: "Thêm mới bản ghi", IpClient: _currentContext.IpClient, userName: _currentContext.UserName, entittyName: "SysfieldEntity");
                _Collection.Items.Add(TypeData_field);

                //Icon
                SysfieldEntity icon_field = new SysfieldEntity();
                icon_field.Featureclass = fe.Id;
                icon_field.Name = "icon";
                icon_field.Config = null;
                icon_field.Fieldname = FieldConstants.ICON;
                icon_field.Datatype = FieldConstants.ICON_DATA_TYPE;
                icon_field.Datalength = 255 + "";
                icon_field.Show = 1000;
                icon_field.Usercreated = _currentContext.UserId;
                icon_field.Status = 0;
                LoggingGisEntity.Log(entity: icon_field, userId: _currentContext.UserId, actionName: UserAction.Insert, comment: "Thêm mới bản ghi", IpClient: _currentContext.IpClient, userName: _currentContext.UserName, entittyName: "SysfieldEntity");
                _Collection.Items.Add(icon_field);


                //Điều kiện
                SysfieldEntity dieukien_field = new SysfieldEntity();
                dieukien_field.Featureclass = fe.Id;
                dieukien_field.Name = "Điều kiện";
                dieukien_field.Config = null;
                dieukien_field.Fieldname = FieldConstants.DIEUKIEN;
                dieukien_field.Datatype = FieldConstants.DIEUKIEN_TYPE;
                dieukien_field.Datalength = 255 + "";
                dieukien_field.Show = 1000;
                dieukien_field.Usercreated = _currentContext.UserId;
                dieukien_field.Status = 0;
                LoggingGisEntity.Log(entity: dieukien_field, userId: _currentContext.UserId, actionName: UserAction.Insert, comment: "Thêm mới bản ghi", IpClient: _currentContext.IpClient, userName: _currentContext.UserName, entittyName: "SysfieldEntity");
                _Collection.Items.Add(dieukien_field);

                //Mã huyện
                SysfieldEntity mahuyen_field = new SysfieldEntity();
                mahuyen_field.Featureclass = fe.Id;
                mahuyen_field.Name = "Tên huyện";
                mahuyen_field.Config = null;
                mahuyen_field.Fieldname = FieldConstants.MAHUYEN;
                mahuyen_field.Datatype = FieldConstants.MAHUYEN_DATA_TYPE;
                mahuyen_field.Datalength = 255 + "";
                mahuyen_field.Show = 1000;
                mahuyen_field.Usercreated = _currentContext.UserId;
                mahuyen_field.Status = 0;
                LoggingGisEntity.Log(entity: mahuyen_field, userId: _currentContext.UserId, actionName: UserAction.Insert, comment: "Thêm mới bản ghi", IpClient: _currentContext.IpClient, userName: _currentContext.UserName, entittyName: "SysfieldEntity");
                _Collection.Items.Add(mahuyen_field);

                //Mã xã
                SysfieldEntity maxa_field = new SysfieldEntity();
                maxa_field.Featureclass = fe.Id;
                maxa_field.Name = "Tên xã";
                maxa_field.Config = null;
                maxa_field.Fieldname = FieldConstants.MAXA;
                maxa_field.Datatype = FieldConstants.MAHUYEN_DATA_TYPE;
                maxa_field.Datalength = 255 + "";
                maxa_field.Show = 1000;
                maxa_field.Usercreated = _currentContext.UserId;
                maxa_field.Status = 0;
                LoggingGisEntity.Log(entity: maxa_field, userId: _currentContext.UserId, actionName: UserAction.Insert, comment: "Thêm mới bản ghi", IpClient: _currentContext.IpClient, userName: _currentContext.UserName, entittyName: "SysfieldEntity");
                _Collection.Items.Add(maxa_field);

                //Unitcode
                SysfieldEntity unitcode_field = new SysfieldEntity();
                unitcode_field.Featureclass = fe.Id;
                unitcode_field.Name = "Mã đơn vị";
                unitcode_field.Config = null;
                unitcode_field.Fieldname = FieldConstants.UNITCODE;
                unitcode_field.Datatype = FieldConstants.MAHUYEN_DATA_TYPE;
                unitcode_field.Datalength = 255 + "";
                unitcode_field.Show = 1000;
                unitcode_field.Usercreated = _currentContext.UserId;
                unitcode_field.Status = 0;
                LoggingGisEntity.Log(entity: unitcode_field, userId: _currentContext.UserId, actionName: UserAction.Insert, comment: "Thêm mới bản ghi", IpClient: _currentContext.IpClient, userName: _currentContext.UserName, entittyName: "SysfieldEntity");
                _Collection.Items.Add(unitcode_field);

                uow.AddCollectionForSave(_Collection);
                //adapter.SaveEntityCollectionAsync(_Collection);
                var query = string.Format("ALTER TABLE appcongangis.{0} ADD COLUMN {1} {2}, ADD COLUMN {3} {4}, ADD COLUMN {5} {6},  ADD COLUMN {7} {8},ADD COLUMN {9} {10},ADD COLUMN {11} {12},ADD COLUMN {13} {14},ADD COLUMN {15} {16}", tablename, FieldConstants.STATUS, FieldConstants.STATUS_DATA_TYPE,FieldConstants.CREATEDDATE, FieldConstants.CREATEDDATE_TYPE, FieldConstants.ICON, FieldConstants.ICON_DATA_TYPE,FieldConstants.DIEUKIEN, FieldConstants.DIEUKIEN_TYPE, FieldConstants.TypeData, FieldConstants.TypeData_DATA_TYPE, FieldConstants.MAHUYEN, FieldConstants.MAHUYEN_DATA_TYPE, FieldConstants.MAXA, FieldConstants.MAHUYEN_DATA_TYPE, FieldConstants.UNITCODE, FieldConstants.MAHUYEN_DATA_TYPE);

                try
                {
                    adapter.ExecuteSQL(query);
                    uow.Commit(adapter, true);
                }
                catch
                {
                    adapter.Rollback();
                    throw;
                }

            }

        }
    }
}

