using System;
using System.Text;

namespace App.Core.Common;

public static class ApiLgspBacGiang
{
    public static readonly string DmDanToc = "https://lgsp.bacgiang.gov.vn:8243/dmdc_DmMaDanToc_Data/1.0";
}

public static class ApiNgspModel
{
    public static string GetEncoded()
    {
        var username = "admin";
        var password = "123456";
        var encoded = Convert.ToBase64String(Encoding.GetEncoding("ISO-8859-1").GetBytes(username + ":" + password));
        return encoded;
    }

    public static class HeThong
    {
        public static readonly string userSSo = "";
    }

    public static class VanBanQPPL
    {
        public static readonly string PostDag = "http://14.160.26.174:9999/api/v1/dags/SyncVBQPPL-Test/dagRuns";
        public static readonly string Authorization = "Basic YWRtaW46MTIzNDU2";
    }

    public class QTHT
    {
        private const string DagUrl = "/api/v1/dags/";
        public readonly string Authorization = "Basic " + GetEncoded();

        public QTHT(string host, string dag, string userName, string passWord)
        {
            Patch = host + DagUrl + dag;
            PostDag = Patch + "/dagRuns";
            Authorization = "Basic " + GetEncoded(userName, passWord);
        }

        public string PostDag { get; set; }
        public string Patch { get; set; }

        public static string GetEncoded(string username = "Admin", string password = "123456")
        {
            var encoded =
                Convert.ToBase64String(Encoding.GetEncoding("ISO-8859-1").GetBytes(username + ":" + password));
            return encoded;
        }
    }

    public class KeHoachDauTu
    {
        private const string KHDT = "/api/v1/variables/";
        private const string DagUrl = "/api/v1/dags/";
        public readonly string Authorization = "Basic " + GetEncoded();

        public KeHoachDauTu(string start, string end, string dag, string host)
        {
            KHDT_DAUTU_startdate = host + KHDT + start;
            KHDT_DAUTU_enddate = host + KHDT + end;
            PostDag = host + DagUrl + dag + "/dagRuns";
        }

        public string KHDT_DAUTU_startdate { get; set; }
        public string KHDT_DAUTU_enddate { get; set; }
        public string PostDag { get; set; }
    }

    public static class TinhHinhAnNinh
    {
        public static readonly string PatchTieuDe = "/api/v1/variables/CAT_tieude";
        public static readonly string PatchMoTa = "/api/v1/variables/CAT_mota";
        public static readonly string PostDag = "/api/v1/dags/SyncCAT/dagRuns";
        public static readonly string Authorization = "Basic " + GetEncoded();
    }

    public static class DanhMuc
    {
        //Api bậc lương
        public static readonly string bacLuong = "https://113.160.159.26:8243/dmdc_DmBacLuong_Data/1.0";

        //Api bảng lương
        public static readonly string bangLuong = "https://113.160.159.26:8243/dmdc_DmBangLuong_Data/1.0";

        //Api bưu chính Cấp 1
        public static readonly string buuChinhC1 = "https://113.160.159.26:8243/dmdc_MaBuuChinhCap1_Data/1.0";

        //Api bưu chính Cấp 2
        public static readonly string buuChinhC2 = "https://113.160.159.26:8243/dmdc_MaBuuChinhCap2_Data/1.0";

        //Api bưu chính Cấp 3
        public static readonly string buuChinhC3 = "https://113.160.159.26:8243/dmdc_MaBuuChinhCap3_Data/1.0";

        //Api định danh
        //Cấp 1
        public static readonly string dinhDanhC1 = "https://113.160.159.26:8243/dmdc_QuanLyVBDHCap1_Data/1.0";

        //Cấp 2
        public static readonly string dinhDanhC2 = "https://113.160.159.26:8243/dmdc_QuanLyVBDHCap2_Data/1.0";

        //Cấp 3
        public static readonly string dinhDanhC3 = "https://113.160.159.26:8243/dmdc_QuanLyVBDHCap3_Data/1.0";

        //Cấp 4
        public static readonly string dinhDanhC4 = "https://113.160.159.26:8243/dmdc_QuanLyVBDHCap4_Data/1.0/1.0";

        //Api đơn vị hành chính
        //Cấp 1
        public static readonly string donViHanhChinhC1 = "https://113.160.159.26:8243/dmdc_DmMaDVHCCap1_Data/1.0";

        //Cấp 2
        public static readonly string donViHanhChinhC2 = "https://113.160.159.26:8243/dmdc_DmMaDVHCCap2_Data/1.0";

        //Cấp 3
        public static readonly string donViHanhChinhC3 = "https://113.160.159.26:8243/dmdc_DmMaDVHCCap3_Data/1.0";

        //Api chức danh
        public static readonly string chucDanh = "https://113.160.159.26:8243/dmdc_DmMaChucDanh_Data/1.0";

        //Api dân tộc
        public static readonly string danToc = "https://113.160.159.26:8243/dmdc_DmMaDanToc_Data/1.0";

        //Api dân tộc khác
        public static readonly string danTocKhac = "https://113.160.159.26:8243/dmdc_DmMaDanTocKhac_Data/1.0";

        //Api Giáo dục đào tạo việt nam
        //Cấp 1
        public static readonly string giaoDucDaoTaoVnC1 =
            "https://113.160.159.26:8243/dmdc_DmGiaoDucDaoTaoVNCap1_Data/1.0";

        //Cấp 2
        public static readonly string giaoDucDaoTaoVnC2 =
            "https://113.160.159.26:8243/dmdc_DmGiaoDucDaoTaoVNCap2_Data/1.0";

        //Cấp 3
        public static readonly string giaoDucDaoTaoVnC3 =
            "https://113.160.159.26:8243/dmdc_DmGiaoDucDaoTaoVNCap3_Data/1.0";

        //Cấp 4
        public static readonly string giaoDucDaoTaoVnC4 =
            "https://113.160.159.26:8243/dmdc_DmGiaoDucDaoTaoVNCap4_Data/1.0";

        //Cấp 5
        public static readonly string giaoDucDaoTaoVnC5 =
            "https://113.160.159.26:8243/dmdc_DmGiaoDucDaoTaoVNCap5_Data/1.0";

        //Api giới tính
        public static readonly string gioiTinh = "https://113.160.159.26:8243/dmdc_DmMaGioiTinh_Data/1.0";

        //Api hệ số lương
        public static readonly string heSoLuong = "https://113.160.159.26:8243/dmdc_DmMaHeSoLuong_Data/1.0";

        //Api hệ số lương vùng
        public static readonly string heSoLuongVung = "https://113.160.159.26:8243/dmdc_DmMaHeSoLuongVung_Data/1.0";

        //Api nhóm lương
        public static readonly string nhomLuong = "https://113.160.159.26:8243/dmdc_DmMaNhomLuong_Data/1.0";

        //Api nhóm máu
        public static readonly string nhomMau = "https://113.160.159.26:8243/dmdc_DmMaNhomMau_Data/1.0";

        //Api quốc gia
        public static readonly string quocGia = "https://113.160.159.26:8243/dmdc_DmMaQuocGia_Data/1.0";

        //Api thi đua khen thưởng
        public static readonly string thiDuaKhenThuong =
            "https://113.160.159.26:8243/dmdc_DmMaThiDuaKhenThuong_Data/1.0";

        //Api tôn giáo
        public static readonly string tonGiao = " https://113.160.159.26:8243/dmdc_DmMaTonGiao_Data/1.0";

        //Api tình trạng hôn nhân
        public static readonly string tinhTrangHonNhan =
            "  https://113.160.159.26:8243/dmdc_DmMaTinhTrangHonNhan_Data/1.0";

        //Api văn bản hành chính
        public static readonly string
            vanBanHanhChinh = "  https://113.160.159.26:8243/dmdc_DmMaTenVBHanhChinh_Data/1.0";

        //Api văn bản quy phạm pháp luật
        public static readonly string maTenVanBanTheoQuyDinhPhapLuat =
            " https://113.160.159.26:8243/dmdc_DmMaTenVBTheoQDPL_Data/1.0";

        //Api ý nghĩa nhóm máu
        public static readonly string yNghiaNhomMau = "  https://113.160.159.26:8243/dmdc_DmMaYNghiaNhomMau_Data/1.0";

        //Api Mã quy định độ khẩn văn bản
        public static readonly string doKhanVanBan = "https://113.160.159.26:8243/dmdc_dokhandata/1.0";

        //Api Văn bản quy phạm pháp luật
        public static readonly string maLoaiVanBanTheoQuyDinhPhapLuat =
            "https://113.160.159.26:8243/dmdc_quyphamphapluatdata/1.0";

        //Api Danh mục loại công chức
        public static readonly string loaiCongChuc = "https://113.160.159.26:8243/dmdcloaidmcongchuc/1.0";

        //Api Danh mục mã bưu chính vùng, khu vực
        public static readonly string maBuuChinh = "https://113.160.159.26:8243/dmdc_dmmabuuchinhcap4/1.0";
    }

    // Sở KHCN
    public static class SoToKhcn
    {
        public static readonly string PatchTieuDe = "http://10.132.2.54/api/v1/variables/KHCN_tochuc_tieude";
        public static readonly string PatchMoTa = "http://10.132.2.54/api/v1/variables/KHCN_tochuc_mota";
        public static readonly string PostDag = "http://10.132.2.54/api/v1/dags/SyncKHCN_ToChuc/dagRuns";
        public static readonly string Authorization = "Basic YWRtaW46MTIzNDU2";
    }

    public static class TieuChuanKhcn
    {
        public static readonly string PatchTieuDe = "http://10.132.2.54/api/v1/variables/KHCN_tieuchuan_tieude";
        public static readonly string PatchMoTa = "http://10.132.2.54/api/v1/variables/KHCN_tieuchuan_mota";
        public static readonly string PostDag = "http://10.132.2.54/api/v1/dags/SyncKHCN_TieuChuan/dagRuns";
        public static readonly string Authorization = "Basic YWRtaW46MTIzNDU2";
    }

    public static class KetQuaKhcn
    {
        public static readonly string PatchTieuDe = "http://10.132.2.54/api/v1/variables/KHCN_ketqua_nghiencuu_tieude";
        public static readonly string PatchMoTa = "http://10.132.2.54/api/v1/variables/KHCN_ketqua_nghiencuu_mota";
        public static readonly string PostDag = "http://10.132.2.54/api/v1/dags/SyncKHCN_KetQua_NghienCuu/dagRuns";
        public static readonly string Authorization = "Basic YWRtaW46MTIzNDU2";
    }

    // --
    // Sở Giáo dục đào tạo
    public static class TraCuuTs10ThptCl
    {
        public static readonly string PatchUnitCreate = "http://10.132.2.54/api/v1/variables/SyncGiaoDuc_unitcreate";
        public static readonly string PatchDescription = "http://10.132.2.54/api/v1/variables/SyncGiaoDuc_description";
        public static readonly string PostDag = "http://10.132.2.54/api/v1/dags/SyncGiaoDuc_DiemChuan/dagRuns";
        public static readonly string Authorization = "Basic YWRtaW46MTIzNDU2";
    }

    public static class SoLopTruong
    {
        public static readonly string PostDag = "http://10.132.2.54/api/v1/dags/SyncCAT/dagRuns";
        public static readonly string Authorization = "Basic YWRtaW46MTIzNDU2";
        public static readonly string PatchUpdate = "Basic YWRtaW46MTIzNDU2";
    }
    // 

    // Sở Nội vụ
    public static class SoNoiVu
    {
        public static readonly string ThongTinHoSo =
            "http://cbccvc.bacgiang.gov.vn/index.php?option=com_services&controller=tichhopcbcc&task=apiThongTinHoSo&username=667c182423b38a24e99392d60336aadc&password=a60271059a12df65b647a40e27c2fb77&limit=100&page=0";

        public static readonly string ChuyenMon =
            "http://cbccvc.bacgiang.gov.vn/index.php?option=com_services&controller=tichhopcbcc&task=apiChuyenMon&username=667c182423b38a24e99392d60336aadc&password=a60271059a12df65b647a40e27c2fb77&limit=100&page=0";

        public static readonly string CongTac =
            "http://cbccvc.bacgiang.gov.vn/index.php?option=com_services&controller=tichhopcbcc&task=apiCongTac&username=667c182423b38a24e99392d60336aadc&password=a60271059a12df65b647a40e27c2fb77&limit=100&page=0";

        public static readonly string DmCoQuan =
            "http://cbccvc.bacgiang.gov.vn/index.php?option=com_services&controller=tichhopcbcc&task=apiDanhMucCoQuan&username=667c182423b38a24e99392d60336aadc&password=a60271059a12df65b647a40e27c2fb77&limit=100&page=0";

        public static readonly string PhuCap =
            "http://cbccvc.bacgiang.gov.vn/index.php?option=com_services&controller=tichhopcbcc&task=apiPhuCap&username=667c182423b38a24e99392d60336aadc&password=a60271059a12df65b647a40e27c2fb77&limit=100&page=0";

        public static readonly string Luong =
            "http://cbccvc.bacgiang.gov.vn/index.php?option=com_services&controller=tichhopcbcc&task=apiLuong&username=667c182423b38a24e99392d60336aadc&password=a60271059a12df65b647a40e27c2fb77&limit=100&page=0";
        //public static readonly string ThongTinHoSo = "https://cbccvc.dnict.vn/index.php?option=com_services&controller=tichhopcbcc&task=apiThongTinHoSo&username=098f6bcd4621d373cade4e832627b4f6&password=3cf63f817ad634ba6c708adaef1099a1&limit=100&page=0";
        //public static readonly string ChuyenMon = "https://cbccvc.dnict.vn/index.php?option=com_services&controller=tichhopcbcc&task=apiChuyenMon&username=098f6bcd4621d373cade4e832627b4f6&password=3cf63f817ad634ba6c708adaef1099a1&limit=100&page=0";
        //public static readonly string CongTac = "https://cbccvc.dnict.vn/index.php?option=com_services&controller=tichhopcbcc&task=apiCongTac&username=098f6bcd4621d373cade4e832627b4f6&password=3cf63f817ad634ba6c708adaef1099a1&limit=100&page=0";
        //public static readonly string DmCoQuan = "https://cbccvc.dnict.vn/index.php?option=com_services&controller=tichhopcbcc&task=apiDanhMucCoQuan&username=098f6bcd4621d373cade4e832627b4f6&password=3cf63f817ad634ba6c708adaef1099a1&limit=100&page=0";
        //public static readonly string PhuCap = "https://cbccvc.dnict.vn/index.php?option=com_services&controller=tichhopcbcc&task=apiPhuCap&username=098f6bcd4621d373cade4e832627b4f6&password=3cf63f817ad634ba6c708adaef1099a1&limit=100&page=0";
        //public static readonly string Luong = "https://cbccvc.dnict.vn/index.php?option=com_services&controller=tichhopcbcc&task=apiLuong&username=098f6bcd4621d373cade4e832627b4f6&password=3cf63f817ad634ba6c708adaef1099a1&limit=100&page=0";
    }
}