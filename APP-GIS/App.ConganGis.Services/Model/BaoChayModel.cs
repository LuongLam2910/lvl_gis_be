using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace App.CongAnGis.Services.Models
{
    public class SysBaoChayModel
    {

        public class BaoChayModel
        {
            public int? iddata { get; set; }
            public string shape { get; set; }
            public string? tablename { get; set; }

            public int? status { get; set; }
            public string createby { get; set; }
            public string address { get; set; }
            public string phonenumber { get; set; }
            public string reasonfire { get; set; }
            public string updateAt { get; set; }
            public string icon { get; set; }
            public string createdate { get; set; }
            public int id { get; set; }
        }

        public class SysbaochayModel
        {
            public int currentPage { get; set; } = 1;
            public int pageSize { get; set; } = 10;
            public string strKey { get; set; }
            public ConditionSearch ListCondition { get; set; }
        }

        public class ConditionSearch
        {
            public int time { get; set; }
            public int status { get; set; }
            public int sort { get; set; }
        }

    }

}
