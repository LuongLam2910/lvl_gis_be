namespace App.Qtht.Services.Models;

public class DmPhongBanModel
{
    public long Id { get; set; }

    public string Ten { get; set; }

    public string UnitCode { get; set; }

    public string UnitName { get; set; }

    public short? TrangThai { get; set; }

    public string TrangThaiHienThi => TrangThai == 1 ? "Đang sử dụng" : "Ngừng sử dụng";

    public class PhongBanSelectModel
    {
        public long Id { get; set; }

        public string Ten { get; set; }
    }
}