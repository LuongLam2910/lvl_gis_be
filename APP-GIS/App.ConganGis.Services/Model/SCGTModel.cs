using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static App.ConganGis.Services.Model.SysBaoTaiNanModel;

namespace App.ConganGis.Services.Model
{
    public class CSGTModel
    {
        public int id { get; set; }
        public int? userId { get; set; }
        public string name { get; set; }
        public DateTime? createDate { get; set; }
        public string donVi { get; set; }
        public int? status { get; set; }
        public string dienThoai { get; set; }
        public int? chucVu { get; set; }
        public string maHuyen { get; set; }
        public string maXa { get; set; }
        public string diaChi { get; set; }

    }
    public class CSGTModelPageModel
    {
        public int currentPage { get; set; } = 1;
        public int pageSize { get; set; } = 10;
        public string strKey { get; set; }
    }
}
