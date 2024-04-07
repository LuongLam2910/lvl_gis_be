using System;
using System.Collections.Generic;

namespace App.Qtht.Services.Models;

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
        public string CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
    }
}