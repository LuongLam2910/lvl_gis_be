using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.ConganGis.Services.Model
{
    public class SysphieuChienThuatModel
    {
        public class phieuChienThuatModel
        {   
            public int id{ get; set; }
            public string tenphieu { get; set; }
            public string? donvi { get; set; }
            public int? status { get; set; }
            public DateTime? createdate { get; set; }
            public string idDonvicbcs { get; set; }
        }

        public class UploadImgPhieuchienThuatModel
        {   
            public string Imagavatar { get; set; }
            public int id { get; set; }
            public int idMucB { get; set; }
            public string check { get; set; }
            public string Fullname { get; set; }
            public string folder { get; set; }
            public string type { get; set; } // Type = "Icon" upload icon ; type = "Image" upload image,video
        }

        public class SysInfoHoSoModel
        {
            public int id { get; set; }
            public string vitridl { get; set; }
            public string tencoso { get; set; }
            public string sohoso { get; set; }
            public string dienthoai1 { get; set; }
            public string diachi { get; set; }
            public string coquancaptructiep { get; set; }
            public string dienthoai2 { get; set; }
            public DateTime? ngaylap { get; set; }
            public DateTime? ngayduyet { get; set; }
            public string nguoilap { get; set; }
            public string nguoipheduyet { get; set; }
            public string imghosonha { get; set; }
            public int? idphieuct { get; set; }
        }

        public class SysMucaModel
        {
            public int id { get; set; }
            public string vitridl { get; set; }
            public string ngosau { get; set; }
            public string giaothongpvcc { get; set; }
            public string nuocbentrong { get; set; }
            public string nuocbenngoai { get; set; }
            public string truluong { get; set; }
            public string kcnnuoc { get; set; }
            public string luuy { get; set; }
            public string dacdiem { get; set; }
            public string luclungcc { get; set; }
            public string lucluongtt { get; set; }
            public string phuongtien { get; set; }
            public int? idttphieu { get; set; }
        }

        public class SysMucbModel
        {
            public int id { get; set; }
            public string tinhhuongpt { get; set; }
            public string trienkhaicc { get; set; }
            public string giaothongpvcc { get; set; }
            public string sdtrienkhai { get; set; }
            public string nhiemvu { get; set; }
            public string truluong { get; set; }
            public string tinhhuong { get; set; }
            public string tctrienkhaicc { get; set; }
            public string sodotrienkhai { get; set; }
            public string nvchihuycc { get; set; }
            public int? idttphieu { get; set; }
        }

        public class SysMuccModel
        {
            public int id { get; set; }
            public DateTime? createdate { get; set; }
            public string ndbosung { get; set; }
            public string nguoixaydung { get; set; }
            public string nguoiduyet { get; set; }
            public int? idttphieu { get; set; }
        }
        public class SysMucdModel
        {
            public int id { get; set; }
            public DateTime? createdate { get; set; }
            public string ndhoctap { get; set; }
            public string thchay { get; set; }
            public string lucluongthgia { get; set; }
            public string danhgia { get; set; }
            public int? idttphieu { get; set; }
        }

        public class SysPhieuchienthuatManagerModel
        {   
            public int id { get; set; }
            public SysInfoHoSoModel listInfo { get; set; }
            public SysMucaModel listMuca { get; set; }
            public SysMucbModel listMucb { get; set; }
            public SysMuccModel listMucc { get; set; }
            public SysMucdModel listMucd { get; set; }
            public string Type { get; set; }
        }

        public class SysListPhieuchienthuatManagerModel
        {
            public SysInfoHoSoModel listInfo { get; set; }
            public List<SysMucaModel> listMuca { get; set; }
            public List<SysMucbModel> listMucb { get; set; }
            public List<SysMuccModel> listMucc { get; set; }
            public List<SysMucdModel> listMucd { get; set; }

        }

        public class SysphieuchienthuatModel
        {
            public int? id { get; set; }
            public DateTime? createDate { get; set; }
            public string tenphieu { get; set; }
            public int? status { get; set; }
            public string tendonvi { get; set; }
            public string donVi { get; set; }
            public string mahuyen { get; set; }
            public string maxa { get; set; }
        }

        public class SysphieuchienthuatPagingModel
        {
            public int currentPage { get; set; } = 1;
            public int pageSize { get; set; } = 10;
            public string strKey { get; set; }
        }
    }
}
