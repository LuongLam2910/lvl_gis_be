using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.CongAnGis.Dal.EntityClasses;
using App.Core.Common;
using App.CongAnGis.Dal.DatabaseSpecific;
using Newtonsoft.Json;
using System.Data;
using NetTopologySuite.Features;
using NetTopologySuite.Geometries;
using NetTopologySuite.IO;
using DocumentFormat.OpenXml.EMMA;
using Npgsql;
using Microsoft.OpenApi.Any;
using SD.LLBLGen.Pro.ORMSupportClasses;
using App.CongAnGis.Services.Models;
using App.CongAnGis.Dal.Linq;
using App.CongAnGis.Services.Business.Reader;
using App.CongAnGis.Services.Business.ConverterProj.VN2000;
using App.CongAnGis.Services.Business.ConverterProj;
using System.Drawing;
using System.IO;
using System.Reflection;
using App.CongAnGis.Dal.HelperClasses;
using Microsoft.Extensions.Options;
using System.Text.RegularExpressions;
using DocumentFormat.OpenXml.Spreadsheet;
using System.Diagnostics.PerformanceData;
using App.CongAnGis.Dal.FactoryClasses;
using SD.LLBLGen.Pro.QuerySpec;
using DocumentFormat.OpenXml.Wordprocessing;
using SD.LLBLGen.Pro.QuerySpec.Adapter;
using SD.LLBLGen.Pro.ORMSupportClasses;
using System.Data.SqlClient;
using System.Data.Common;
using DocumentFormat.OpenXml.Office2010.ExcelAc;
using static App.CongAnGis.Services.Models.DataModel;
using System.Collections;
using App.CongAnGis.Services.Manager;
using App.CongAnGis.Services.Model;
using Nancy.Json;
using System.Net.Http.Json;
using System.Text.Json;
using Newtonsoft.Json.Linq;
using static GoogleMaps.LocationServices.Constants;
using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Drawing;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Engineering;
using static System.Runtime.InteropServices.JavaScript.JSType;
using SD.LLBLGen.Pro.QuerySpec;
using Microsoft.SqlServer.Server;
using AutoMapper;
using static App.Core.Common.Constants;
using DocumentFormat.OpenXml.Drawing.Charts;
using Microsoft.AspNetCore.JsonPatch.Internal;
using DataTable = System.Data.DataTable;
using Nancy;
using System.Text.Json.Nodes;
using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Drawing.Diagrams;

namespace App.CongAnGis.Services.Manager
{
    public interface IDataManager
    {
        Task<ApiResponse<string>> getDataByTable(string tableName, string format , string filter);
        Task<string> GetData(string tablename, string format, string filter);
        Task<ApiResponse> ImportData(ImportDataModel model);
        Task<ApiResponse<PageModelView<string>>> pagingData(DataSearchModel model);
        Task<ApiResponse> ImportDataExcel(ImportDataModel model);
        Task<ApiResponse<int>> InsertDataCustom(CreateDataModel model);
        Task<ApiResponse> ChangeStatusByTableName(string tablename, int key, int Id);
        Task<ApiResponse<IEnumerable<IconModel>>> getIconByTable(string tablename);
        Task<ApiResponse<int>> UpdateDataCustom(CreateOrUpdateDataModel model);
        Task<ApiResponse> DeleteData(string tablename, string key);
        public string ExportData(ExportDataModel model);
        public string ExportTemplate(ExportDataModel model);

        //Task<ApiResponse> insertCustom(List<nhatroModel> model);
        Task<ApiResponse<string>> selectOntShape(string tableName, int id);
        Task<ApiResponse<ThongKeModelResponse>> countData(ThongKeModel model);
        Task<ApiResponse<ThongKeModelResponse>> countDataPCCC(ThongKeModel model);
        Task<ApiResponse<ThongKeModelResponse>> countDataBieudo(ThongKeModel model);
    }


    public class DataManager : IDataManager
    {
        public readonly AppSettingModel _appSetting;
        public readonly ICurrentContext _currentContext;
        public DataManager(IOptionsSnapshot<AppSettingModel> appConfig, ICurrentContext currentContext)
        {
            _appSetting = appConfig.Value;
            _currentContext = currentContext;
        }

        public async Task<ApiResponse> DeleteData(string tablename, string key)
        {
            return await Task.Run(() =>
            {
                using (DataAccessAdapter adapter = new DataAccessAdapterFactory().CreateAdapter())
                {
                    UnitOfWork2 unitOfWork2 = new UnitOfWork2();
                    tablename = adapter.CatalogNameToUse +"."+ tablename;
                    string where = "where id = " + key;
                    string sql = string.Format("delete from {0} {1}", tablename, where);
                    try
                    {
                        adapter.ExecuteSQLAsync(sql);
                        RelationPredicateBucket filterDelete = new RelationPredicateBucket(SysfilemanagerFields.Idddata.Equal(key));
                        filterDelete.PredicateExpression.AddWithAnd(SysfilemanagerFields.Tablename == tablename);
                        unitOfWork2.AddDeleteEntitiesDirectlyCall(nameof(SysfilemanagerEntity), filterDelete);
                        unitOfWork2.CommitAsync(adapter);
                        LoggingGisEntity.LogCustom(entity: "Id data : " + key, userId: _currentContext.UserId, actionName: UserAction.Delete, comment: "Xóa " + tablename, IpClient: _currentContext.IpClient, userName: _currentContext.UserName, entittyName: tablename);
                        adapter.Commit();
                        return GeneralCode.Success;
                    }
                    catch (Exception ex)
                    {
                        return GeneralCode.Error;
                    }

                }
            });
        }

        public async Task<ApiResponse> ChangeStatusByTableName(string tablename, int key, int Id)
        {
            return await Task.Run(() =>
            {
                DataTable dt = new DataTable();
                using (DataAccessAdapter adapter = new DataAccessAdapterFactory().CreateAdapter())
                {
                    int status = key == 1 ? 0 : 1;
                    tablename = adapter.CatalogNameToUse + "." + tablename;
                    string sql = "Update " + tablename + " set status = " + status + " where id = " + Id;
                    try
                    {
                        adapter.StartTransaction(IsolationLevel.ReadCommitted, "update " + tablename);
                        adapter.ExecuteSQLAsync(sql);
                        adapter.Commit();
                        return GeneralCode.Success;
                    }
                    catch (Exception ex)
                    {
                        adapter.Rollback();
                        return GeneralCode.Error;
                    }
                }
            });
        }

        public async Task<ApiResponse<int>> UpdateDataCustom(CreateOrUpdateDataModel model)
        {

            using (DataAccessAdapter adapter = new DataAccessAdapterFactory().CreateAdapter())
            {
                UnitOfWork2 unitOfWork2 = new UnitOfWork2();
                string where = "where id = " + model.Id;
                model.TableName = adapter.CatalogNameToUse + "." + model.TableName;
                string set = "";
                for (int i = 0; i < model.ListData.Count; i++)
                {
                    if (string.IsNullOrEmpty(set)) set = "set ";
                    else set += ",";
                    if (string.IsNullOrEmpty(model.ListData[i].value))
                        set += model.ListData[i].key + " = NULL ";
                    else
                        set += model.ListData[i].key + " = N'" + model.ListData[i].value + "'";
                }
                string sql = string.Format("update {0} {1} {2}", model.TableName, set, where);

                try
                {
                    if (model.fileDelete.Count > 0)
                    {
                        RelationPredicateBucket filterDelete = new RelationPredicateBucket(SysfilemanagerFields.Id.In(model.fileDelete));
                        unitOfWork2.AddDeleteEntitiesDirectlyCall(nameof(SysfilemanagerEntity), filterDelete);
                        await unitOfWork2.CommitAsync(adapter);
                    }
                }
                catch (Exception ex)
                {
                    return ApiResponse<int>.Generate(GeneralCode.Error, ex.Message);
                }
                int id = model.Id;
                try
                {
                    LoggingGisEntity.LogCustom(entity: "Id data : " + id, userId: _currentContext.UserId, actionName: UserAction.Update, comment: "Cập nhật " + model.TableName, IpClient: _currentContext.IpClient, userName: _currentContext.UserName, entittyName: model.TableName);
                    adapter.ExecuteSQL(sql);
                }
                catch (Exception ex)
                {
                    return ApiResponse<int>.Generate(GeneralCode.Error, ex.Message);
                }
                return ApiResponse<int>.Generate(id, GeneralCode.Success);

            }
        }

        public async Task<ApiResponse<PageModelView<string>>> pagingData(DataSearchModel model)
        {
            using (DataAccessAdapter adapter = new DataAccessAdapterFactory().CreateAdapter())
            {
                try
                {
                    var metaData = new LinqMetaData(adapter);
                    var featureclassesId = metaData.Sysfeatureclass.FirstOrDefault(x => x.Tablename == model.TableName).Id;

                    var allField = metaData.Sysfield.Where(x => x.Featureclass == featureclassesId && x.Datatype.ToLower() == "text").ToList();
                    string where = " Where true";
                    var offset = (model.currentPage - 1) * model.pageSize;
                    if (model.textSearch != null)
                    {
                        if (model.fieldSelect != null)
                        {
                            where += " and " + model.fieldSelect + " like '%" + model.textSearch + "%'";
                        }
                        else
                        {
                            where += " and false ";
                            foreach (var item in allField)
                            {
                                where += " or " + item.Fieldname + " like '%" + model.textSearch + "%'";
                            }
                        }
                    }
                    if (model.Status != null)
                    {
                        where += " and status = " + model.Status;
                    } else
                    {
                        if (model.TableName == "sysbaochay") where += " and status != 2 ";
                    }

                    model.TableName = adapter.CatalogNameToUse + "." + model.TableName;
                    var TotalRecord_count = "Select count(*) from " + model.TableName;
                    var RecordsCount_count = "Select count(*) from " + model.TableName + where;
                    var TotalRecord = adapter.FetchQuery<Int16>(TotalRecord_count);
                    var RecordsCount = adapter.FetchQuery<Int16>(RecordsCount_count);
                    var TotalPage = 1;
                    if (RecordsCount[0] <= model.pageSize)
                    {
                        TotalPage = 1;
                        model.currentPage = TotalPage;
                    }
                    else
                    {
                        int remainder = 0;
                        TotalPage = Math.DivRem(RecordsCount[0], model.pageSize, out remainder);
                        if (remainder > 0)
                        {
                            TotalPage++;
                        }
                    }
                    var sql = "Select * from " + model.TableName + where + " order by createddate DESC OFFSET " + offset + " LIMIT " + model.pageSize;
                    DataTable dt = NpgsqManager.SelectDataBySql(sql, adapter.ConnectionString);
                    return new PageModelView<string>()
                    {
                        Data = ExportManager.DataTableToJSON(dt),
                        CurrentPage = model.currentPage,
                        PageSize = model.pageSize,
                        TotalPage = TotalPage,
                        TotalRecord = TotalRecord[0],
                        RecordsCount = RecordsCount[0]
                    };
                }
                catch (Exception ex)
                {
                    return ApiResponse<PageModelView<string>>.Generate(null, GeneralCode.Error, ex.Message);
                }
            }
        }
        public async Task<ApiResponse<string>> getDataByTable(string tableName, string format, string filter)
        {
            using (DataAccessAdapter adapter = new DataAccessAdapterFactory().CreateAdapter())
            {
                try
                {
                    tableName = adapter.CatalogNameToUse + "." + tableName ;
                    var sql = "Select * from " + tableName + filter;

                    DataTable dt = NpgsqManager.SelectDataBySql(sql, adapter.ConnectionString);

                    if (format == "geojson")
                        return ExportManager.DataTableToGeoJSON(dt);
                    return ExportManager.DataTableToJSON(dt);

                }
                catch (Exception ex)
                {
                    return ApiResponse<string>.Generate(GeneralCode.Error, ex.Message);
                }
            }
        }

        public async Task<string> GetData(string tablename, string format, string filter)
        {
            using (DataAccessAdapter adapter = new DataAccessAdapterFactory().CreateAdapter())
            {
                
                var sql = "Select * from " + adapter.CatalogNameToUse + "." + tablename +" " + filter;

                DataTable dt = NpgsqManager.SelectDataBySql(sql, adapter.ConnectionString);
                if (format == "geojson")
                    return ExportManager.DataTableToGeoJSON(dt);
                return ExportManager.DataTableToJSON(dt);
            }
        }

        public async Task<ApiResponse> ImportData(ImportDataModel model)
        {
            List<string> fields = new List<string>();
            string connectString = "";
            DataTable dt = new DataTable();
            int number = 0;
            DateTime startTime = DateTime.Now;
            DateTime endTime = DateTime.Now;
            bool err = false;
            SysimportLogEntity log = new SysimportLogEntity();
            log.Tablename = model.TableName;
            try
            {
                using (DataAccessAdapter adapter = new DataAccessAdapterFactory().CreateAdapter())
                {
                    connectString = adapter.ConnectionString;
                    //truy vấn thông tin file
                    var metaData = new LinqMetaData(adapter);
                    var file = metaData.Sysfile.First(c => c.Id == model.FileId);

                    //////lấy danh sách trường của bảng
                    //EntityCollection<SysfieldEntity> collect = new EntityCollection<SysfieldEntity>();

                    //RelationPredicateBucket filter = new RelationPredicateBucket();

                    //adapter.FetchEntityCollection(collect, filter);
                    string pathFile = file.Path;// @"E:\FileProjectBT\Demo\demo.xlsx";
                    //var lstFields = collect.Where(x => x.Featureclassid == model.FeatureclassId).Select(x => x.Fieldname).ToList();
                    if (model.FileType.ToLower() == "shp")
                    {
                        //đọc dữ liệu
                        SHPReader reader = new SHPReader(pathFile);
                        dt = reader.GetData();
                    }
                    else if (model.FileType.ToLower() == "excel")
                    {
                        ExcelReader reader = new ExcelReader(pathFile);
                        dt = reader.GetData();
                    }
                    else if (model.FileType.ToLower() == "csv")
                    {
                        CSVReader reader = new CSVReader(pathFile);
                        dt = reader.GetData();
                    }
                }
                List<FieldMappingModel> lst = model.Fields;
                Dictionary<string, string> dicField = new Dictionary<string, string>();
                for (int i = 0; i < lst.Count; i++)
                {
                    if (lst[i].FieldDestination.Equals("ID", StringComparison.CurrentCultureIgnoreCase)) continue;
                    dicField.Add(lst[i].FieldDestination, lst[i].FieldSource);
                }
                using (DataAccessAdapter adapter = new DataAccessAdapterFactory().CreateAdapter())
                {
                    foreach (DataRow dataRow in dt.Rows)
                    {
                        string sql = string.Format("INSERT INTO " + adapter.CatalogNameToUse + "." + "{0} (shape,status) VALUES('{1}',1)", model.TableName, dataRow["SHAPE"]);

                        sql = "INSERT INTO " + adapter.CatalogNameToUse + "." + model.TableName + " (shape,status";
                        string SHAPE = dataRow["SHAPE"] + "";
                        string values = "VALUES(";

                        //chuyển đổi hệ quy chiếu
                        if (!string.IsNullOrEmpty(model.Prj) && model.Prj != "4326")
                        {
                            var wktWriter = new WKTWriter();
                            Geometry geom = VietnamCoordinateSystems.TransformWKTVN2000ToWGS84Geometry(SHAPE, ProjecttionHelper.ConvertCoordinateSystemsFromSrid(model.Prj));
                            SHAPE = wktWriter.Write(geom);
                        }

                        //xử lý riêng với trường hợp không gian
                        //int length = SHAPE.Length;
                        //if (length > 4000)
                        //{
                        //    int count = length / 4000;
                        //    int index = 0;
                        //    int start = 0;
                        //    int ch = 4000;
                        //    while (index <= count)
                        //    {
                        //        start = index * 4000;
                        //        if (start + ch > length)
                        //        {
                        //            ch = length - start;
                        //        }
                        //        string result = SHAPE.Substring(start, ch);
                        //        values += "to_clob('" + result + "')";
                        //        if (index + 1 <= count)
                        //            values += " ||";
                        //        index++;
                        //    }
                        //    values += ",1";
                        //}
                        //else
                        //{
                        values += "'" + SHAPE + "',1";
                        //}
                        foreach (var item in dicField)
                        {

                            sql += "," + item.Key;
                            values += ",'" + dataRow[item.Value].ToString().Replace("'", "''") + "'";
                        }
                        values += ")";
                        sql = sql + ")" + values;

                        number++;
                        adapter.ExecuteSQL(sql);
                        adapter.Commit();
                    }
                }
                endTime = DateTime.Now;
                //var responseData = GetData(model.TableName, "geojson", null).Result;
                //if (responseData != null)
                //{
                //    string filePath = _appSetting.FileLocation.Path;
                //    if (string.IsNullOrWhiteSpace(filePath))
                //    {
                //        string rootPath = Directory.GetDirectoryRoot(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location));
                //        filePath = rootPath + Core.Common.Constants.FileBase.File + @"\Export\geoJson\";
                //    }
                //    var nameJson = filePath + "/" + model.TableName + ".json";
                //    using StreamWriter sw = new StreamWriter(nameJson, false);
                //    sw.Write(responseData);
                //    sw.Close();
                //}
                log.Message = "Thành công";
            }
            catch (Exception ex)
            {
                err = true;
                log.Message = ex.Message;
            }
            try
            {
                using (DataAccessAdapter adapter = new DataAccessAdapterFactory().CreateAdapter())
                {
                    var collect = new EntityCollection<SysimportLogEntity>();
                    log.Starttime = startTime;
                    log.Endtime = endTime;
                    log.Count = number;
                    log.Status = err ? 0 : 1;
                    await adapter.SaveEntityAsync(log, true);
                }
            }
            catch (Exception)
            {
            }
            if (err)
                return ApiResponse.Generate(GeneralCode.Error, log.Message);
            return GeneralCode.Success;
        }

        public async Task<ApiResponse> ImportDataExcel(ImportDataModel model)
        {
            int number = 0;
            DateTime startTime = DateTime.Now;
            DateTime endTime = DateTime.Now;
            bool err = false;
            SysimportLogEntity log = new SysimportLogEntity();
            log.Tablename = model.TableName;

            try
            {

                using (DataAccessAdapter adapter = new DataAccessAdapterFactory().CreateAdapter())
                {
                    model.TableName = adapter.CatalogNameToUse + "." + model.TableName;
                    List<FieldMappingModel> lst = model.Fields;
                    string sql = "";

                    foreach (var item in lst)
                    {
                        sql = string.Format("INSERT INTO " + model.TableName + " (" + item.FieldDestination + "status)" + " Values (" + item.FieldValue + "1)");
                        adapter.ExecuteSQL(sql);
                        number++;
                        adapter.Commit();
                    }


                    var collect = new EntityCollection<SysimportLogEntity>();
                    log.Starttime = startTime;
                    log.Endtime = endTime;
                    log.Count = number;
                    log.Status = err ? 0 : 1;
                    await adapter.SaveEntityAsync(log, true);
                }
            }
            catch (Exception ex)
            {
                return ApiResponse.Generate(GeneralCode.Error, ex.Message);
            }
            if (err)
                return ApiResponse.Generate(GeneralCode.Error, log.Message);
            return GeneralCode.Success;
        }

        public async Task<ApiResponse<IEnumerable<IconModel>>> getIconByTable(string tablename)
        {
            using (DataAccessAdapterBase adapter = new DataAccessAdapterFactory().CreateAdapter())
            {
                try
                {
                    var listTable = tablename.Split(";");
                    List<IconModel> data = new List<IconModel>();
                    foreach (var item in listTable)
                    {
                        if (!string.IsNullOrEmpty(item))
                        {

                            string tableName = adapter.CatalogNameToUse + "." + item;
                            var checkTable = string.Format("SELECT EXISTS ( SELECT FROM pg_tables WHERE schemaname = '{0}' AND tablename  = '{1}' )",
                                adapter.CatalogNameToUse, item);

                            var check = adapter.FetchQuery<bool>(checkTable);

                            if (check[0])
                            {
                                string sql = "select distinct(icon) from " + tableName;
                                var result = adapter.FetchQuery<string>(sql);

                                foreach (var i in result)
                                {
                                    var model = new IconModel();
                                    model.IconName = i;
                                    model.TableName = item;
                                    data.Add(model);
                                }
                            }
                        }
                    }
                    return ApiResponse<IEnumerable<IconModel>>.Generate(data, GeneralCode.Success, "SUCCESS");
                }
                catch (Exception ex)
                {

                    return ApiResponse<IEnumerable<IconModel>>.Generate(null, GeneralCode.Error, ex.Message);
                }

            }
        }

        public string ExportData(ExportDataModel model)
        {
            DataTable dt = new DataTable();
            using (DataAccessAdapter adapter = new DataAccessAdapterFactory().CreateAdapter())
            {
                try
                {   
                    string tableName = adapter.CatalogNameToUse + '.' + model.TableName;
                    string filter = "";
                    if (!string.IsNullOrEmpty(model.Filter)) filter = "where " + model.Filter;
                    string sql = string.Format("select * from {0} {1}", tableName, filter);
                    dt = NpgsqManager.SelectDataBySql(sql, adapter.ConnectionString);
                }
                catch (Exception ex)
                {
                }
            }

            string folder = _appSetting.FileLocation.Path;
            if (string.IsNullOrWhiteSpace(folder))
            {
                string rootPath = Directory.GetDirectoryRoot(System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location));
                folder = rootPath + Core.Common.Constants.FileBase.File;
            }


            if (model.FileType.ToLower() == "json")
            {
                folder += @"\Export\geoJson\";
            }
            else
            {
                folder += @"\Export\" + model.TableName + DateTime.Now.ToString("ddMMyyyy");
            }
            string file = "";
            if (model.FileType.ToLower() == "shp")
            {
                file = ExportManager.ExportSHP(dt, folder, model.TableName, model.Prj);
            }
            else if (model.FileType.ToLower() == "excel")
            {
                using (DataAccessAdapter adapter = new DataAccessAdapterFactory().CreateAdapter())
                {
                    var metadata = new LinqMetaData(adapter);
                    var fe = metadata.Sysfeatureclass.FirstOrDefault(x => x.Tablename == model.TableName);
                    EntityCollection<SysfieldEntity> sysfieldEntities = new EntityCollection<SysfieldEntity>();
                    RelationPredicateBucket filter = new RelationPredicateBucket();
                    filter.PredicateExpression.AddWithAnd(SysfieldFields.Featureclass == fe.Id);
                    filter.PredicateExpression.AddWithAnd(SysfieldFields.Status == 1);
                    adapter.FetchEntityCollection(sysfieldEntities, filter);
                    file = ExportManager.ExportExcel(fe.Name, sysfieldEntities, dt, folder, model.TableName, model.Prj);
                }
            }
            else if (model.FileType.ToLower() == "json")
            {
                file = ExportManager.ExportGeoJson(dt, folder, model.TableName);
            }
            else file = ExportManager.ExportCSV(dt, folder, model.TableName, model.Prj);
            return file;
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
            using (DataAccessAdapter adapter = new DataAccessAdapterFactory().CreateAdapter())
            {
                try
                {
                    var metadata = new LinqMetaData(adapter);
                    EntityCollection<SysfieldEntity> data = new EntityCollection<SysfieldEntity>();
                    RelationPredicateBucket filter = new RelationPredicateBucket();
                    var fe = metadata.Sysfeatureclass.FirstOrDefault(x => x.Tablename == model.TableName);
                    filter.PredicateExpression.AddWithAnd(SysfieldFields.Featureclass == fe.Id);
                    filter.PredicateExpression.AddWithAnd(SysfieldFields.Status == 1);
                    adapter.FetchEntityCollection(data, filter);
                    string file = "";
                    file = ExportManager.ExportTemplateExcel(data, folder, model.TableName, fe.Name);
                    return file;
                }
                catch (Exception ex)
                {
                    return ex.ToString();
                }
            }
            
        }

        public async Task<ApiResponse<int>> InsertDataCustom(CreateDataModel model)
        {
            using (DataAccessAdapter adapter = new DataAccessAdapterFactory().CreateAdapter())
            {
                try
                {

                    model.TableName = adapter.CatalogNameToUse + "." + model.TableName;
                    string sql = "with rows as (";
                    sql += string.Format("INSERT INTO " + model.TableName + " (" + model.Data.FieldDestination + "status)" + " Values (" + model.Data.FieldValue + "1) RETURNING id");
                    sql += ") SELECT id FROM rows";
                    var data = adapter.FetchQuery<int>(sql);
                    LoggingGisEntity.LogCustom(entity: "Id data : " + data[0].ToString(), userId: _currentContext.UserId, actionName: UserAction.Insert, comment: "Thêm mới " + model.TableName, IpClient: _currentContext.IpClient, userName: _currentContext.UserName, entittyName: model.TableName);
                    adapter.Commit();
                    return ApiResponse<int>.Generate(data[0], GeneralCode.Success, "SUCCESS");
                }
                catch (Exception ex)
                {

                    return ApiResponse<int>.Generate(GeneralCode.Error, ex.Message);
                }

            }
        }

        public async Task<ApiResponse<string>> selectOntShape(string tableName, int id)
        {
            using (DataAccessAdapter adapter = new DataAccessAdapterFactory().CreateAdapter())
            {
                try
                {
                    tableName = adapter.CatalogNameToUse + "." + tableName;
                    string sql = "Select shape from " + tableName + " where id = " + id;
                    var data = adapter.FetchQuery<string>(sql);
                    adapter.Commit();
                    return ApiResponse<string>.Generate(data[0], GeneralCode.Success, "SUCCESS");
                }
                catch (Exception ex)
                {
                    return ApiResponse<string>.Generate(GeneralCode.Error, ex.Message);
                }
            }
        }

        public async Task<ApiResponse<ThongKeModelResponse>> countData(ThongKeModel model)
        {
            using (DataAccessAdapterBase adapterBase = new DataAccessAdapterFactory().CreateAdapter())
            {
                var where = "";
                if (!string.IsNullOrEmpty(model.maHuyen))
                {
                    where += " where mahuyen = '" + model.maHuyen + "' ";
                } 
                if (!string.IsNullOrEmpty(model.maXa))
                {
                    where += !string.IsNullOrEmpty(model.maHuyen) ? "and maxa = '" + model.maXa + "'" : " where maxa = '" + model.maXa + "'";
                }
                if (!string.IsNullOrEmpty(model.startDate))
                {
                    where += !string.IsNullOrEmpty(model.maXa) ? " and createddate >= '" + model.startDate + "' and createddate <= '" + model.enDate + "'" : " where createddate >= '" + model.startDate + "' and createddate <= '" + model.enDate + "'";
                }
                var lst = new ThongKeModelResponse();
                lst.lstFeatures = new List<DataUpdateModel>();
                foreach (var item in model.lstFeatures)
                {
                    var feature = new DataUpdateModel();
                    string sql = "Select count(id) from " + adapterBase.CatalogNameToUse + "." + item.value + where;
                    var data = adapterBase.FetchQuery<int>(sql);
                    feature.key = item.key;
                    feature.value = data[0].ToString();
                    lst.lstFeatures.Add(feature);
                }
                return ApiResponse<ThongKeModelResponse>.Generate(lst,GeneralCode.Success,"SUCCESS");
            }
        }

        public async Task<ApiResponse<ThongKeModelResponse>> countDataPCCC(ThongKeModel model)
        {
            using (DataAccessAdapterBase adapterBase = new DataAccessAdapterFactory().CreateAdapter())
            {
                var where = "";
                var sqlpc = "";
                if (!string.IsNullOrEmpty(model.maHuyen))
                {
                    where += " where mahuyen = '" + model.maHuyen + "' ";
                }
                if (!string.IsNullOrEmpty(model.maXa))
                {
                    where += !string.IsNullOrEmpty(model.maHuyen) ? "and maxa = '" + model.maXa + "'" : " where maxa = '" + model.maXa + "'";
                }
                if (!string.IsNullOrEmpty(model.startDate))
                {
                    where += !string.IsNullOrEmpty(model.maXa) ? " and createddate >= '" + model.startDate + "' and createddate <= '" + model.enDate + "'" : " where createddate >= '" + model.startDate + "' and createddate <= '" + model.enDate + "'";
                }
                var lst = new ThongKeModelResponse();
                lst.lstFeatures = new List<DataUpdateModel>();
                foreach (var item in model.lstFeatures)
                {
                    var feature = new DataUpdateModel();
                    string sql = "Select count(id) from " + adapterBase.CatalogNameToUse + "." + item.value + where;
                    if (!string.IsNullOrEmpty(where))
                    {
                        sqlpc = "Select count(id) from " + adapterBase.CatalogNameToUse + "." + item.value + where + "and status = 1";
                    }
                    if (string.IsNullOrEmpty(where))
                    {
                        sqlpc = "Select count(id) from " + adapterBase.CatalogNameToUse + "." + item.value + " where status = 1";
                    }
                    var data = adapterBase.FetchQuery<int>(sql);
                    var datapc = adapterBase.FetchQuery<int>(sqlpc);
                    feature.key = item.key;
                    feature.value = data[0].ToString();
                    feature.status = datapc[0].ToString();
                    lst.lstFeatures.Add(feature);
                }
                return ApiResponse<ThongKeModelResponse>.Generate(lst, GeneralCode.Success, "SUCCESS");
            }
        }

        public async Task<ApiResponse<ThongKeModelResponse>> countDataBieudo(ThongKeModel model)
        {
            using (DataAccessAdapterBase adapterBase = new DataAccessAdapterFactory().CreateAdapter())
            {
                var where = "";
                if (!string.IsNullOrEmpty(model.maHuyen))
                {
                    where += " where mahuyen = '" + model.maHuyen + "' ";
                }
                if (!string.IsNullOrEmpty(model.maXa))
                {
                    where += !string.IsNullOrEmpty(model.maHuyen) ? "and maxa = '" + model.maXa + "'" : " where maxa = '" + model.maXa + "'";
                }
                if (!string.IsNullOrEmpty(model.startDate))
                {
                    where += !string.IsNullOrEmpty(model.maXa) ? " and createddate >= '" + model.startDate + "' and createddate <= '" + model.enDate + "'" : " where createddate >= '" + model.startDate + "' and createddate <= '" + model.enDate + "'";
                }
                var lst = new ThongKeModelResponse();
                lst.lstFeatures = new List<DataUpdateModel>();
                foreach (var item in model.lstFeatures)
                {
                    var feature = new DataUpdateModel();
                    string sql = "with Months as (";
                    sql += string.Format("Select generate_series(" + model.months + ") AS Month), ");
                    sql += string.Format("MonthsWithData AS (SELECT DISTINCT EXTRACT(MONTH FROM createddate) AS DataMonth FROM " + adapterBase.CatalogNameToUse + "." + item.value + " WHERE EXTRACT(YEAR FROM createddate) = " + model.years +")");
                    sql += string.Format(" SELECT m.Month," + "COALESCE(COUNT(r.createddate), 0) AS RecordCount FROM Months m ");
                    sql += string.Format("LEFT JOIN " + adapterBase.CatalogNameToUse + "." + item.value + " r ON EXTRACT(MONTH FROM r.createddate) = m.Month AND EXTRACT(YEAR FROM r.createddate) = " + model.years);
                    sql += " LEFT JOIN MonthsWithData d ON m.Month = d.DataMonth";
                    sql += " GROUP BY m.Month" +" "+"ORDER BY m.Month";
                    DataTable dt = NpgsqManager.SelectDataBySql(sql, adapterBase.ConnectionString);

                    var data =  ExportManager.DataTableToJSON(dt);
                    feature.key = item.key;
                    feature.value = data;
                    lst.lstFeatures.Add(feature);
                 }
                return ApiResponse<ThongKeModelResponse>.Generate(lst, GeneralCode.Success, "SUCCESS");
            }
        }
    }
}
