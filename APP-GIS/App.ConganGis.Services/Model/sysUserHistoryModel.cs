using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using static App.CongAnGis.Services.Models.SysBaoChayModel;

namespace App.CongAnGis.Services.Models
{
    public class SysUserHistory
    {

        public class SysUserHistoryModel
        {
            public int? id { get; set; }
            public int? userId { get; set; }
            public string createDate { get; set; }
            public string userName { get; set; }
            public string shape { get; set; }
            public string icon { get; set; }
            public string deviceInfo { get; set; }
            public string model { get; set; }
            public int? status { get; set; }
        }

        public class SysUserHistoryLineModel
        {
            public int? id { get; set; }
            public string userId { get; set; }
            public DateTime? createDate { get; set; }
            public string userName { get; set; }
            public string shape { get; set; }
            public string icon { get; set; }
            public string deviceInfo { get; set; }
            public string model { get; set; }
            public int? status { get; set; }
            public string mahuyen { get; set; }
            public string maxa { get; set; }
        }

        public class SysUserHistoryPagingModel
        {
            public int currentPage { get; set; } = 1;
            public int pageSize { get; set; } = 10;
            public string strKey { get; set; }
        }
    }

}
