using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace App.CongAnGis.Services.Models;

public class FileAttachment
{
    public class FileBase64
    {
        public string FileName { get; set; }

        public string FileData { get; set; }
    }

    public class ListFileModel
    {
        public int Id { get; set; }
        public string FilePath { get; set; }
        public string FileName { get; set; }
    }

    public class FileBase64MultiRequestModel
    {
        public List<FileBase64DataMultiModel> lstFile { get; set; }

        public class FileBase64DataMultiModel
        {
            public string Folder { get; set; }

            public IList<FileBase64> Files { get; set; }
        }
    }

    public class FileBase64RequestModel
    {
        public string Folder { get; set; }

        public IList<FileBase64> Files { get; set; }
    }

    
    public class ResponseModel
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public string LocalFileName { get; set; }
        public int CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public int Iddata { get; set; }
        public string Type { get; set; }
    }

    public class FileManagerModel
    {
        public int idData { get; set; }
        public string tableName { get; set; }
        public string folder { get; set; }
        public string type { get; set; } // Type = "Icon" upload icon ; type = "Image" upload image,video
    }
}