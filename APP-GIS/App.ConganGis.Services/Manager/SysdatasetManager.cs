
using App.CongAnGis.Dal.EntityClasses;
using App.CongAnGis.Dal.HelperClasses;
using App.CongAnGis.Dal.Linq;
using App.CongAnGis.Services.ManagerBase;
using App.Core.Common;
using Microsoft.Extensions.Options;
using SD.LLBLGen.Pro.ORMSupportClasses;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using static App.CongAnGis.Services.Models.DataModel;
using static App.Core.Common.Constants;

namespace App.CongAnGis.Services.Manager
{
    public interface ISysdatasetManager
    {
        Task<ApiResponse<SysdatasetEntity>> InsertAsyncCusTom(SysdatasetVM.ItemSysdataset _Model);
        Task<ApiResponse> DeleteAsyncCusTom(int id);
        Task<ApiResponse> UpdateAsyncCusTom(SysdatasetVM.ItemSysdataset _Model);
        public string ExportTemplate(ExportDataModel model);
    }
    public class SysdatasetManager : SysdatasetManagerBase , ISysdatasetManager
    {

        public readonly AppSettingModel _appSetting;
        private readonly ICurrentContext _currentContext;
        public SysdatasetManager(ICurrentContext currentContext, IOptionsSnapshot<AppSettingModel> appConfig)
        {
            _currentContext = currentContext;
            _appSetting = appConfig.Value;
        }
        public string ExportTemplate(ExportDataModel model)
        {
            string folder = _appSetting.FileLocation.Path;
            if (string.IsNullOrWhiteSpace(folder))
            {
                string rootPath = Directory.GetDirectoryRoot(System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location));
                folder = rootPath + Core.Common.Constants.FileBase.File;
            }

            folder += @"\Export\" + DateTime.Now.ToString("ddMMyyyy");
            using (DataAccessAdapterBase adapter = new DataAccessAdapterFactory().CreateAdapter())
            {
                try
                {
                    var metadata = new LinqMetaData(adapter);
                    EntityCollection<SysfeatureclassEntity> data = new EntityCollection<SysfeatureclassEntity>();
                    RelationPredicateBucket filter = new RelationPredicateBucket();
                    filter.PredicateExpression.AddWithAnd(SysfeatureclassFields.Datasetid == model.FeatureclassId);
                    adapter.FetchEntityCollection(data, filter);
                    string file = "";
                    file = ExportManager.ExportDataSetExcel(data, folder, model.TableName, model.TableName);
                    return file;
                }
                catch (Exception ex)
                {
                    return ex.ToString();
                }
            }

        }
        public async Task<ApiResponse> UpdateAsyncCusTom(SysdatasetVM.ItemSysdataset _Model)
        {
            try
            {
                return await Task.Run(() =>
                {
                    SysdatasetEntity _SysdatasetEntity = configVMtoEntity.CreateMapper().Map<SysdatasetEntity>(_Model);
                    _SysdatasetEntity.Unitcode = _currentContext.UnitCode;
                    Update(_SysdatasetEntity);
                    LoggingGisEntity.Log(entity: _SysdatasetEntity, userId: _currentContext.UserId, actionName: UserAction.Update, comment: "Sửa bản ghi", IpClient: _currentContext.IpClient, userName: _currentContext.UserName, entittyName: "SysdatasetEntity");
                    return GeneralCode.Success;
                });
            }
            catch (Exception ex)
            {
                return ApiResponse.Generate(GeneralCode.Error, ex.Message);
            }
        }
        public async Task<ApiResponse<SysdatasetEntity>> InsertAsyncCusTom(SysdatasetVM.ItemSysdataset _Model)
        {
            try
            {
                return await Task.Run(() =>
                {
                    SysdatasetEntity _SysdatasetEntity = configVMtoEntity.CreateMapper().Map<SysdatasetEntity>(_Model);
                    _SysdatasetEntity.Unitcode = _currentContext.UnitCode;
                    Insert(_SysdatasetEntity);
                    LoggingGisEntity.Log(entity : _SysdatasetEntity, userId: _currentContext.UserId, actionName: UserAction.Insert, comment: "Thêm mới bản ghi", IpClient: _currentContext.IpClient, userName: _currentContext.UserName, entittyName : "SysdatasetEntity");
                    return ApiResponse<SysdatasetEntity>.Generate(_SysdatasetEntity,GeneralCode.Success,"SUCCESS");
                });

            }
            catch (Exception ex)
            {
                return ApiResponse<SysdatasetEntity>.Generate(GeneralCode.Error, ex.Message);
            }
        }

        public async Task<ApiResponse> DeleteAsyncCusTom(int id)
        {
            try
            {
                return await Task.Run(() =>
                {
                    using (DataAccessAdapterBase adapter = (new DataAccessAdapterFactory()).CreateAdapter())
                    {
                        SysdatasetEntity _SysdatasetEntity = new SysdatasetEntity(id);
                        if (adapter.FetchEntity(_SysdatasetEntity))
                        {
                            LoggingGisEntity.Log(entity: _SysdatasetEntity, userId: _currentContext.UserId, actionName: UserAction.Delete, comment: "Xóa bản ghi", IpClient: _currentContext.IpClient, userName: _currentContext.UserName, entittyName: "SysdatasetEntity");
                            adapter.DeleteEntity(_SysdatasetEntity);
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
    }
}

