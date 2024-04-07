using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.CongAnGis.Services.Model
{
    public class SysLogModel
    {
        public class LogModel
        {
            public int? Id { get; set; }
            public int user_id { get; set; }
            public string objectName { get; set; }
            public string action { get; set; }
            public string note { get; set; }
            public string data { get; set; }
            public int idData { get; set; }
            public string userName { get; set; }
        }
    }
}
