
using App.CongAnGis.Dal.EntityClasses;
using App.CongAnGis.Dal.HelperClasses;
using App.CongAnGis.Dal.Linq;
using App.CongAnGis.Services.Business.Reader;
using App.CongAnGis.Services.ManagerBase;
using App.CongAnGis.Services.Models;
using App.Core.Common;
using DocumentFormat.OpenXml.EMMA;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using SD.LLBLGen.Pro.ORMSupportClasses;
using SD.LLBLGen.Pro.QuerySpec;
using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using static App.CongAnGis.Services.Model.SysFileModel;
using static App.CongAnGis.Services.Models.FileAttachment;
using static App.Core.Common.Constants;

namespace App.CongAnGis.Services.Manager
{

    public interface ISysfileManager
    {
        public Task<ApiResponse<PageModelView<IEnumerable<SysfileVM.ItemSysfile>>>> PagingFileByFolderId(PageSysfileModel _model);
        public Task<ApiResponse<IEnumerable<string>>> GetColumns(int fileId);
        public Task<ApiResponse<List<SysFileResponseModel>>> UploadSysFile(IList<IFormFile> files, string folder, int folderId);
    }
    public class SysfileManager : SysfileManagerBase ,ISysfileManager
    {
        private readonly AppSettingModel _appSetting;
        private readonly ICurrentContext _currentContext;
        public SysfileManager(IOptionsSnapshot<AppSettingModel> appConfig, ICurrentContext currentContext)
        {
            _appSetting = appConfig.Value;
            _currentContext = currentContext;
        }


        public async Task<ApiResponse<PageModelView<IEnumerable<SysfileVM.ItemSysfile>>>> PagingFileByFolderId(PageSysfileModel _model)
        {
            try
            {
                return await Task.Run(() =>
                {
                    RelationPredicateBucket filter = new RelationPredicateBucket();
                    if (!string.IsNullOrWhiteSpace(_model.strKey))
                    {
                        _model.strKey = "%" + _model.strKey + "%";
                        var pred1 = (SysfileFields.Name).Like(_model.strKey)
                            .Or(SysfileFields.Path.Like(_model.strKey)).Or(SysfileFields.Createby.Like(_model.strKey)).Or(SysfileFields.Unitcode.Like(_model.strKey));
                        filter.PredicateExpression.Add(pred1);
                    }
                    if (_model.folderId != null)
                    {
                        filter.PredicateExpression.AddWithAnd(SysfileFields.Folder == _model.folderId);
                    }
                    using (DataAccessAdapterBase adapter = new DataAccessAdapterFactory().CreateAdapter())
                    {
                        var data = new EntityCollection<SysfileEntity>();
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
                        SortExpression sort = new SortExpression(SysfileFields.Id | SortOperator.Descending);
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
                            return ApiResponse<PageModelView<IEnumerable<SysfileVM.ItemSysfile>>>.Generate(null, GeneralCode.Error, ex.Message);
                        }
                        return new PageModelView<IEnumerable<SysfileVM.ItemSysfile>>()
                        {
                            Data = data.Select(c => configEntitytoVM.CreateMapper().Map<SysfileEntity, SysfileVM.ItemSysfile>(c)).ToList(),
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
                return ApiResponse<PageModelView<IEnumerable<SysfileVM.ItemSysfile>>>.Generate(null, GeneralCode.Error, ex.Message);
            }
        }

        public async Task<ApiResponse<IEnumerable<string>>> GetColumns(int fileId)
        {
            using (DataAccessAdapterBase adapter = new DataAccessAdapterFactory().CreateAdapter())
            {
                string connectString = adapter.ConnectionString;
                //truy vấn thông tin file
                var metaData = new LinqMetaData(adapter);
                var file = metaData.Sysfile.First(c => c.Id == fileId);
                string pathFile = file.Path;

                if (pathFile.EndsWith(".shp"))
                {
                    //đọc dữ liệu
                    SHPReader reader = new SHPReader(pathFile);
                    return reader.GetColumns();
                }
                else if (pathFile.EndsWith(".xlsx") || pathFile.EndsWith(".xls"))
                {
                    ExcelReader reader = new ExcelReader(pathFile);
                    return reader.GetColumns();
                }
                else if (pathFile.EndsWith(".csv"))
                {
                    CSVReader reader = new CSVReader(pathFile);
                    return reader.GetColumns();
                }
            }
            return new ApiResponse<IEnumerable<string>>();
        }

        public async Task<ApiResponse<List<SysFileResponseModel>>> UploadSysFile(IList<IFormFile> files, string folder, int folderId)
        {
            try
            {
                var dtNow = DateTime.Now;
                var collection = new EntityCollection<SysfileEntity>();
                foreach (var item in files)
                    if (item.Length > 0)
                    {
                        if (item.Length > _appSetting.FileLocation.MaxSize * 1048576) // Bytes (B)
                            return ApiResponse<List<SysFileResponseModel>>.Generate(GeneralCode.Error,
                                "Dung dượng file quá lớn");
                        var filePath = _appSetting.FileLocation.Path;
                        if (string.IsNullOrWhiteSpace(filePath))
                        {
                            var rootPath =
                                Directory.GetDirectoryRoot(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location));
                            filePath = rootPath + FileBase.File + @"\SysFileManager";
                        }

                        if (!string.IsNullOrWhiteSpace(folder)) filePath += @"\" + folder;

                        if (!Directory.Exists(filePath)) Directory.CreateDirectory(filePath);

                        var localFileName = dtNow.ToString("yyyyMMdd") + "_" + item.FileName;
                        var fullPath = filePath + @"\" + localFileName;
                        using (var fileStream = item.OpenReadStream())
                        
                        using (var stream = new FileStream(fullPath, FileMode.Create))
                        {
                            await item.CopyToAsync(stream);
                        }

                        collection.Add(new SysfileEntity
                        {
                            Createby = _currentContext.UserName ?? string.Empty,
                            Name = item.FileName,
                            Path = fullPath,
                            Folder = folderId,
                            Length = (int?)item.Length,
                            Unitcode = _currentContext.UnitCode ?? string.Empty
                        });
                    }

                using (var adapter = new DataAccessAdapterFactory().CreateAdapter())
                {
                    await adapter.SaveEntityCollectionAsync(collection, true, true);
                }

                return collection.Select(c => new SysFileResponseModel
                {
                    id = (int)c.Id,
                    createBy = c.Createby,
                    fileName = c.Name,
                    filePath = c.Path,
                    folderId = (int)c.Folder
                }).ToList();
            }
            catch (Exception ex)
            {
                return ApiResponse<List<SysFileResponseModel>>.Generate(GeneralCode.Error, ex.Message);
            }
        }


    }

}

