using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using App.Core.Common;
using App.Qtht.Services.Models;
using App.QTHTGis.Dal.EntityClasses;
using App.QTHTGis.Dal.HelperClasses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Options;
using SD.LLBLGen.Pro.ORMSupportClasses;
using SD.LLBLGen.Pro.QuerySpec;
using static App.Core.Common.Constants;

namespace App.QTHTGis.Services.Manager;

public interface IFileManager
{
    Task<ApiResponse<List<FileAttachment.ResponseModel>>> UploadFile(IList<IFormFile> files, string folder);
    Task<ApiResponse<List<FileAttachment.ResponseModel>>> UploadFileBase64(FileAttachment.FileBase64RequestModel model);

    Task<ApiResponse<List<FileAttachment.ResponseModel>>> UploadFileBase64Multi(
        FileAttachment.FileBase64MultiRequestModel model);

    Task<FileStreamResult> Download(int fileId);
    Task<ApiResponse> DeleteFile(string lstId);
    Task<FileStreamResult> DownloadListFile(string strFileId);
    Task<ApiResponse<List<FileAttachment.ListFileModel>>> GetPathFileById([FromBody] int[] lstId);
    Task<ApiResponse<List<FileAttachment.ListFileModel>>> GetPathFile(int id);
}

public class FileManager : IFileManager
{
    private readonly AppSettingModel _appSetting;
    private readonly ICurrentContext _currentContext;

    public FileManager(IOptionsSnapshot<AppSettingModel> appConfig, ICurrentContext currentContext)
    {
        _appSetting = appConfig.Value;
        _currentContext = currentContext;
    }

    public async Task<ApiResponse> DeleteFile(string lstId)
    {
        try
        {
            using (var adapter = new DataAccessAdapterFactory().CreateAdapter())
            {
                var lstAttachment = new EntityCollection<SysattachmentEntity>();
                adapter.FetchEntityCollection(lstAttachment,
                    new RelationPredicateBucket(SysattachmentFields.Id.In(lstId.Split(';'))));
                foreach (var item in lstAttachment) DeleteFileInFolder(item.Filepath);
                await adapter.DeleteEntitiesDirectlyAsync(nameof(SysattachmentEntity),
                    new RelationPredicateBucket(SysattachmentFields.Id.In(lstId.Split(";"))));
                return GeneralCode.Success;
            }
        }
        catch (Exception ex)
        {
            return ApiResponse.Generate(GeneralCode.Error, ex.Message);
        }
    }

    public async Task<FileStreamResult> Download(int fileId)
    {
        using (var adapter = new DataAccessAdapterFactory().CreateAdapter())
        {
            var sysattachmentEntity = new SysattachmentEntity(fileId);
            adapter.FetchEntity(sysattachmentEntity);
            var memory = new MemoryStream();
            using (var stream = new FileStream(sysattachmentEntity.Filepath, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }

            memory.Position = 0;
            string contentType;
            new FileExtensionContentTypeProvider().TryGetContentType(sysattachmentEntity.Filepath, out contentType);
            contentType = contentType ?? "application/octet-stream";
            var streamResult = new FileStreamResult(memory, contentType);
            streamResult.FileDownloadName = sysattachmentEntity.Filename;
            //return File(memory, contentType, fileName);
            return streamResult;
        }
    }

    public async Task<FileStreamResult> DownloadListFile(string strFileId)
    {
        return await Task.Run(() =>
        {
            using (var adapter = new DataAccessAdapterFactory().CreateAdapter())
            {
                var lstFileId = strFileId.Split(',').Select(int.Parse).ToList();
                var lstAttach = new EntityCollection<SysattachmentEntity>();
                adapter.FetchEntityCollection(lstAttach,
                    new RelationPredicateBucket(SysattachmentFields.Id.In(lstFileId)));
                return GetZipArchiveDownload(lstAttach.Select(c => new InMemoryFile
                {
                    Content = File.ReadAllBytes(c.Filepath),
                    FileName = c.Filename
                }).ToList());
            }
        });
    }

    public async Task<ApiResponse<List<FileAttachment.ResponseModel>>> UploadFile(IList<IFormFile> files, string folder)
    {
        try
        {
            var dtNow = DateTime.Now;
            var collection = new EntityCollection<SysattachmentEntity>();
            foreach (var item in files)
                if (item.Length > 0)
                {
                    if (item.Length > _appSetting.FileLocation.MaxSize * 1048576) // Bytes (B)
                        return ApiResponse<List<FileAttachment.ResponseModel>>.Generate(GeneralCode.Error,
                            "Dung dượng file quá lớn");
                    var filePath = _appSetting.FileLocation.Path;
                    if (string.IsNullOrWhiteSpace(filePath))
                    {
                        var rootPath =
                            Directory.GetDirectoryRoot(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location));
                        filePath = rootPath + FileBase.File;
                    }

                    filePath += $@"\{DateTime.Now.Year}";

                    if (!string.IsNullOrWhiteSpace(folder)) filePath += @"\" + folder;

                    if (!Directory.Exists(filePath)) Directory.CreateDirectory(filePath);

                    var localFileName = dtNow.ToString("yyyyMMddHHmmss") + "_" + item.FileName;
                    var fullPath = filePath + @"\" + localFileName;
                    using (var fileStream = item.OpenReadStream())
                        //{
                        //    using (var ms = new MemoryStream())
                        //    {
                        //        fileStream.CopyTo(ms);
                        //        var fileByte = ms.ToArray();
                        //        StreamWriter writer = new StreamWriter(fileByte,)
                        //        File.WriteAllBytes(fullPath, fileByte,);
                        //    }
                        //}
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        await item.CopyToAsync(stream);
                    }

                    collection.Add(new SysattachmentEntity
                    {
                        Createby = _currentContext.UserName ?? string.Empty,
                        Createdate = dtNow,
                        Filename = item.FileName,
                        Filepath = fullPath,
                        Localfilename = localFileName
                    });
                }

            using (var adapter = new DataAccessAdapterFactory().CreateAdapter())
            {
                await adapter.SaveEntityCollectionAsync(collection, true, true);
            }

            return collection.Select(c => new FileAttachment.ResponseModel
            {
                Id = (int)c.Id,
                CreateBy = c.Createby,
                CreateDate = c.Createdate,
                FileName = c.Filename,
                FilePath = c.Filepath,
                LocalFileName = c.Localfilename
            }).ToList();
        }
        catch (Exception ex)
        {
            return ApiResponse<List<FileAttachment.ResponseModel>>.Generate(GeneralCode.Error, ex.Message);
        }
    }

    public async Task<ApiResponse<List<FileAttachment.ListFileModel>>> GetPathFileById([FromBody] int[] lstId)
    {
        try
        {
            return await Task.Run(() =>
            {
                using (var adapter = new DataAccessAdapterFactory().CreateAdapter())
                {
                    var filePath = _appSetting.FileLocation.Path;
                    if (string.IsNullOrWhiteSpace(filePath))
                    {
                        var rootPath =
                            Directory.GetDirectoryRoot(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location));
                        filePath = rootPath + FileBase.File;
                    }

                    var collection = new EntityCollection<SysattachmentEntity>();
                    adapter.FetchEntityCollection(collection,
                        new RelationPredicateBucket(SysattachmentFields.Id.In(lstId)));
                    return collection.Select(c => new FileAttachment.ListFileModel
                    {
                        Id = (int)c.Id,
                        FileName = c.Filename,
                        FilePath = c.Filepath.Replace(filePath, FileBase.FileLink).Replace(@"\", "/")
                    }).ToList();
                }
            });
        }
        catch (Exception ex)
        {
            return ApiResponse<List<FileAttachment.ListFileModel>>.Generate(GeneralCode.Error, ex.Message);
        }
    }

    public async Task<ApiResponse<List<FileAttachment.ListFileModel>>> GetPathFile(int id)
    {
        try
        {
            return await Task.Run(() =>
            {
                using (var adapter = new DataAccessAdapterFactory().CreateAdapter())
                {
                    var filePath = _appSetting.FileLocation.Path;
                    if (string.IsNullOrWhiteSpace(filePath))
                    {
                        var rootPath =
                            Directory.GetDirectoryRoot(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location));
                        filePath = rootPath + FileBase.File;
                    }

                    var collection = new EntityCollection<SysattachmentEntity>();
                    adapter.FetchEntityCollection(collection,
                        new RelationPredicateBucket(SysattachmentFields.Id == id));
                    return collection.Select(c => new FileAttachment.ListFileModel
                    {
                        Id = (int)c.Id,
                        FileName = c.Filename,
                        FilePath = c.Filepath.Replace(filePath, FileBase.FileLink).Replace(@"\", "/")
                    }).ToList();
                }
            });
        }
        catch (Exception ex)
        {
            return ApiResponse<List<FileAttachment.ListFileModel>>.Generate(GeneralCode.Error, ex.Message);
        }
    }

    public Task<ApiResponse<List<FileAttachment.ResponseModel>>> UploadFileBase64(
        FileAttachment.FileBase64RequestModel model)
    {
        var formFiles = Base64ToImage(model.Files);

        return UploadFile(formFiles, model.Folder);
    }

    public async Task<ApiResponse<List<FileAttachment.ResponseModel>>> UploadFileBase64Multi(
        FileAttachment.FileBase64MultiRequestModel model)
    {
        var lstResult = new List<FileAttachment.ResponseModel>();
        foreach (var item in model.lstFile)
        {
            var formFiles = Base64ToImage(item.Files);

            lstResult.AddRange((await UploadFile(formFiles, item.Folder)).Data);
        }

        return lstResult;
    }

    private bool DeleteFileInFolder(string path)
    {
        try
        {
            if (File.Exists(path))
            {
                File.Delete(path);
                return true;
            }

            return false;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public static FileStreamResult GetZipArchiveDownload(List<InMemoryFile> files)
    {
        byte[] archiveFile;
        using (var archiveStream = new MemoryStream())
        {
            using (var archive = new ZipArchive(archiveStream, ZipArchiveMode.Create, true))
            {
                foreach (var file in files)
                {
                    var zipArchiveEntry = archive.CreateEntry(file.FileName, CompressionLevel.Fastest);
                    using (var zipStream = zipArchiveEntry.Open())
                    {
                        zipStream.Write(file.Content, 0, file.Content.Length);
                    }
                }
            }

            archiveFile = archiveStream.ToArray();
        }

        var contentType = "application/zip";
        var memory = new MemoryStream(archiveFile);
        memory.Position = 0;

        var streamResult = new FileStreamResult(memory, contentType);
        streamResult.FileStream = memory;
        streamResult.FileDownloadName = "Archive.zip";
        return streamResult;
    }

    private List<IFormFile> Base64ToImage(IList<FileAttachment.FileBase64> files)
    {
        var formFiles = new List<IFormFile>();
        foreach (var item in files)
        {
            var base64Intance = item.FileData.Split(',');
            var bytes = Convert.FromBase64String(base64Intance.Length > 1 ? base64Intance[1] : item.FileData);

            var stream = new MemoryStream(bytes);

            IFormFile file = new FormFile(stream, 0, bytes.Length, item.FileName, item.FileName);
            formFiles.Add(file);
        }

        return formFiles;
    }

    public class InMemoryFile
    {
        public string FileName { get; set; }
        public byte[] Content { get; set; }
    }
}