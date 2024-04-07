using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.ConganGis.Services.Model
{
    public class SysBaoTaiNanModel
    {

        public class BaoTaiNanModel
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
            public string mahuyen { get; set; }
            public string maxa { get; set; }
            public int id { get; set; }
            public int? nguoitiepnhan { get; set; }
        }

        public class SysbaotainanModel
        {
            public int currentPage { get; set; } = 1;
            public int pageSize { get; set; } = 10;
            public string strKey { get; set; }
            public ConditionSearchh ListCondition { get; set; }
        }

        public class ConditionSearchh
        {
            public int time { get; set; }
            public int status { get; set; }
            public int sort { get; set; }
        }

    }
}
