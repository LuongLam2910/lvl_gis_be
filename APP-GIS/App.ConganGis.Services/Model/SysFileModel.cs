using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.CongAnGis.Services.Model
{
    public class SysFileModel
    {
        public class PageSysfileModel
        {
            public int currentPage { get; set; } = 1;
            public int pageSize { get; set; } = 10;
            public string strKey { get; set; }
            public int folderId { get; set; }             
        }

        public class SysFileResponseModel
        {
            public int id { get; set; }
            public string fileName { get; set; } 
            public int folderId { get; set; } 
            public string filePath { get; set; }
            public string createBy { get; set; }
            public int length { get; set; }
            public string unitCodde { get; set; }
        }
        public class SysFileModelGis
        {
            ///<summary>Createddate. </summary>
            public DateTime Createddate { get; set; }
            ///<summary>Createuserid. </summary>
            public decimal? Createuserid { get; set; }
            ///<summary>Filename. </summary>
            public string Filename { get; set; }
            ///<summary>Filepath. </summary>
            public string Filepath { get; set; }
            ///<summary>Id. </summary>
            public int Id { get; set; }
            ///<summary>Iddata. </summary>
            public decimal? Iddata { get; set; }
            ///<summary>Tablename. </summary>
            public string Tablename { get; set; }
            public string TypeFile { get; set; }
        }
        public class SysFileManagerModel
        {
            public List<SysFileModelGis> ListImage { get; set; }
            public List<SysFileModelGis> ListVideo { get; set; }
        }
    }
}
