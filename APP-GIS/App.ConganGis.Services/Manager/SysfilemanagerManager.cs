
using App.CongAnGis.Services.Model;
using App.CongAnGis.Dal.DatabaseSpecific;
using App.CongAnGis.Dal.EntityClasses;
using App.CongAnGis.Dal.HelperClasses;
using App.CongAnGis.Dal.Linq;
using App.CongAnGis.Services.ManagerBase;
using App.Core.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using SD.LLBLGen.Pro.ORMSupportClasses;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using static App.CongAnGis.Services.Model.SysFileModel;
using static App.CongAnGis.Services.Models.FileAttachment;
using static App.Core.Common.Constants;
using DocumentFormat.OpenXml.Office2010.Excel;
using Microsoft.AspNetCore.JsonPatch.Internal;
using Microsoft.AspNetCore.Mvc;
using App.QTHTGis.Dal.EntityClasses;
using Microsoft.AspNetCore.StaticFiles;

namespace App.CongAnGis.Services.Manager
{
    public interface ISysfilemanagerManager
    {
        Task<ApiResponse> UploadFile(IList<IFormFile> files, FileManagerModel model);
        Task<ApiResponse<SysFileManagerModel>> GetFileByIdBase64(string tableName, int idFeatureData);
        Task<ApiResponse<SysFileManagerModel>> GetFileById(string tableName, int idFeatureData);
        Task<ApiResponse> DeleteSysFileManager(string fileName,int idData);
        Task<FileStreamResult> Download(int fileId);
    }
    public class SysfilemanagerManager : SysfilemanagerManagerBase, ISysfilemanagerManager
    {
        private readonly AppSettingModel _appSetting;
        private readonly ICurrentContext _currentContext;
        public SysfilemanagerManager(IOptionsSnapshot<AppSettingModel> appConfig, ICurrentContext currentContext)
        {
            _appSetting = appConfig.Value;
            _currentContext = currentContext;
        }
        public async Task<FileStreamResult> Download(int fileId)
        {
            using (var adapter = new DataAccessAdapterFactory().CreateAdapter())
            {
                var entity = new SysfilemanagerEntity(fileId);
                adapter.FetchEntity(entity);
                var memory = new MemoryStream();
                using (var stream = new FileStream(entity.Filepath, FileMode.Open))
                {
                    await stream.CopyToAsync(memory);
                }

                memory.Position = 0;
                string contentType;
                new FileExtensionContentTypeProvider().TryGetContentType(entity.Filepath, out contentType);
                contentType = contentType ?? "application/octet-stream";
                var streamResult = new FileStreamResult(memory, contentType);
                streamResult.FileDownloadName = entity.Filename;
                //return File(memory, contentType, fileName);
                return streamResult;
            }
        }
        public async Task<ApiResponse<SysFileManagerModel>> GetFileByIdBase64(string tableName, int idFeatureData)
        {
            return await Task.Run(() =>
            {
                using (DataAccessAdapter adapter = new DataAccessAdapterFactory().CreateAdapter())
                {
                    List<SysFileModelGis> lstVideo = new List<SysFileModelGis>();
                    List<SysFileModelGis> lstImage = new List<SysFileModelGis>();
                    var metaData = new LinqMetaData(adapter);
                    Byte[] bytes;
                    string file;
                    var data = metaData.Sysfilemanager.Where(m => m.Tablename == tableName && m.Idddata == idFeatureData).Select(c => new SysFileModelGis
                    {
                        TypeFile = c.Typefile,
                        Filename = c.Filename,
                        Filepath = c.Filepath,
                        Id = c.Id,
                        Tablename = c.Tablename,
                    }).ToList();
                    foreach (var item in data)
                    {
                        if (item.TypeFile.Contains("image"))
                        {
                            string location = Directory.GetDirectoryRoot(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location)) + FileBase.File;
                            string path = item.Filepath.StartsWith("\\") ? location + item.Filepath : item.Filepath;
                            bytes = File.ReadAllBytes(path);
                            file = Convert.ToBase64String(bytes);
                            lstImage.Add(new SysFileModelGis
                            {
                                Filename = item.Filename,
                                Filepath = file,
                                Id = item.Id,
                                Tablename = item.Tablename,
                                TypeFile = item.TypeFile
                            }
                            );
                        }
                        else
                        {
                            lstVideo.Add(new SysFileModelGis
                            {
                                Filename = item.Filename,
                                Filepath = item.Filepath,
                                Id = item.Id,
                                Tablename = item.Tablename,
                                TypeFile = item.TypeFile
                            }
                           );
                        }
                    };

                    return new SysFileManagerModel
                    {
                        ListImage = lstImage,
                        ListVideo = lstVideo
                    };
                };
            });
        }
        public async Task<ApiResponse<SysFileManagerModel>> GetFileById(string tableName, int idFeatureData)
        {
            return await Task.Run(() =>
            {
                using (DataAccessAdapter adapter = new DataAccessAdapterFactory().CreateAdapter())
                {
                    List<SysFileModelGis> lstVideo = new List<SysFileModelGis>();
                    List<SysFileModelGis> lstImage = new List<SysFileModelGis>();
                    var metaData = new LinqMetaData(adapter);
                    var data = metaData.Sysfilemanager.Where(m => m.Tablename == tableName && m.Idddata == idFeatureData).Select(c => new SysFileModelGis
                    {
                        TypeFile = c.Typefile,
                        Filename = c.Filename,
                        Filepath = c.Filepath,
                        Id = c.Id,
                        Tablename = c.Tablename,
                    }).ToList();
                    foreach (var item in data)
                    {
                        if (item.TypeFile.Contains("image"))
                        {
                            lstImage.Add(new SysFileModelGis
                            {
                                Filename = item.Filename,
                                Filepath = item.Filepath,
                                Id = item.Id,
                                Tablename = item.Tablename,
                                TypeFile = item.TypeFile
                            }
                            );
                        }
                        else
                        {
                            lstVideo.Add(new SysFileModelGis
                            {
                                Filename = item.Filename,
                                Filepath = item.Filepath,
                                Id = item.Id,
                                Tablename = item.Tablename,
                                TypeFile = item.TypeFile
                            }
                           );
                        }
                    };

                    return new SysFileManagerModel
                    {
                        ListImage = lstImage,
                        ListVideo = lstVideo
                    };
                };
            });
        }
        

        public async Task<ApiResponse> UploadFile(IList<IFormFile> files, FileManagerModel model)
        {
            try
            {
                EntityCollection<SysfilemanagerEntity> collection = new EntityCollection<SysfilemanagerEntity>();
                EntityCollection<SysfileEntity> _SysfileEntity = new EntityCollection<SysfileEntity>();
                foreach (var item in files)
                {
                    if (item.Length > 0)
                    {
                        string rootPath = Directory.GetDirectoryRoot(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location));
                        if (item.Length > (_appSetting.FileLocation.MaxSize * 1048576)) // Bytes (B)
                        {
                            return ApiResponse.Generate(GeneralCode.Error, "Dung dượng file quá lớn");
                        }
                        string filePath = _appSetting.FileLocation.Path;
                        if (string.IsNullOrWhiteSpace(filePath))
                        {
                            filePath = rootPath + FileBase.File + @"\SysFileManager";
                        }

                        if (!string.IsNullOrWhiteSpace(model.folder))
                        {
                            filePath += @"\" + model.folder;
                        }

                        if (!Directory.Exists(filePath))
                        {
                            Directory.CreateDirectory(filePath);
                        }

                        string fullPath = filePath + @"\" + item.FileName;
                        using (var fileStream = item.OpenReadStream())

                        using (var stream = new FileStream(fullPath, FileMode.Create))
                        {

                            await item.CopyToAsync(stream);
                        }
                        if (model.type == "Icon")
                        {
                            using (DataAccessAdapterBase adapter = new DataAccessAdapterFactory().CreateAdapter())
                            {
                                string table = adapter.CatalogNameToUse + "." + model.tableName;
                                string sql = "update " + table + " set icon = '" + item.FileName + "'";
                                adapter.ExecuteSQL(sql);
                                adapter.Commit();
                            }
                        }
                        else
                        {
                            string prePath = rootPath + FileBase.File;
                            string subPath = fullPath.Replace(prePath, "");
                            collection.Add(new SysfilemanagerEntity
                            {
                                Createuserid = _currentContext.UserId,
                                Filename = item.FileName,
                                Filepath = subPath,
                                Tablename = model.tableName,
                                Typefile = item.ContentType,
                                Idddata = model.idData
                            });
                            _SysfileEntity.Add(new SysfileEntity
                            {
                                Name = item.FileName,
                                Folder = 5,
                                Path = subPath,
                                Createby = _currentContext.FullName,
                                Unitcode = _currentContext.UnitCode,
                                Type = model.idData,
                                Length = (int?)item.Length
                            });
                        }
                    }
                }
                if (model.type == "Image")
                {
                    using (DataAccessAdapterBase adapter = new DataAccessAdapterFactory().CreateAdapter())
                    {
                        await adapter.SaveEntityCollectionAsync(collection, true, true);
                        await adapter.SaveEntityCollectionAsync(_SysfileEntity, true, true);
                    }
                }
                return ApiResponse.Generate(GeneralCode.Success, "Thành Công");
            }
            catch (System.Exception ex)
            {
                return ApiResponse.Generate(GeneralCode.Error, ex.Message);
            }
        }

        public async Task<ApiResponse> DeleteSysFileManager(string fileName, int idData)
        {
            using (DataAccessAdapterBase adapterBase = new DataAccessAdapterFactory().CreateAdapter())
            {
                try
                {
                    var metaData = new LinqMetaData(adapterBase);
                        var id = metaData.Sysfilemanager.FirstOrDefault(x => x.Filename == fileName && x.Idddata == idData).Id;
                        SysfilemanagerEntity _Entity = new SysfilemanagerEntity(id);
                        adapterBase.DeleteEntity(_Entity);
                    return ApiResponse.Generate(GeneralCode.Success);
                }
                catch (Exception ex)
                {
                    return ApiResponse.Generate(GeneralCode.Error, ex.Message);
                }
               
            }
        }
    }
}

