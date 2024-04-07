using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using static App.CongAnGis.Services.Models.SysBaoChayModel;

namespace App.CongAnGis.Services.Models
{
    public class SysCauHinhModel
    {

        public class CauHinhResponseModel
        {
            public int? id { get; set; }
            public string doanDuongTu { get; set; }
            public string doanDuongDen { get; set; }
            public string startTime { get; set; }
            public string endTime { get; set; }
            public string tuanSuat { get; set; }
            public int? status { get; set; }
        }

        public class CauHinhPagingModel
        {
            public int currentPage { get; set; } = 1;
            public int pageSize { get; set; } = 10;
            public string strKey { get; set; }
        }
    }

}
