namespace App.Core.Common;

public partial class AppSettingModel
{
    public LGSP_ConnectApiModel LGSP_ConnectApi { get; set; }

    public QLCB_ApiModel QLCB_Api { get; set; }
    public SyncQTHT_ApiModel SyncQTHT_Api { get; set; }
    public Login LoginModel { get; set; }

    public class SyncQTHT_ApiModel
    {
        public string Link { get; set; }
    }

    public class Login
    {
        public string UserName { get; set; }
        public string PassWord { get; set; }
    }

    public class LGSP_ConnectApiModel
    {
        public string Link { get; set; }
    }

    public class QLCB_ApiModel
    {
        public string ThongTinHoSo { get; set; }
        public string ChuyenMon { get; set; }
        public string CongTac { get; set; }
        public string DmCoQuan { get; set; }
        public string PhuCap { get; set; }
        public string Luong { get; set; }
        public int? ThoiGianDongBo { get; set; }
    }
}