namespace App.Qtht.Services.Models;

public class DmChucVuModel
{
    public long Id { get; set; }

    public string Ten { get; set; }

    public long? Cap { get; set; }

    public long? IdToChuc { get; set; }

    public string ToChucHienThi { get; set; }

    public short? TrangThai { get; set; }

    public string TrangThaiHienThi => TrangThai == 1 ? "Đang sử dụng" : "Ngừng sử dụng";

    public class ChucVuSelectModel
    {
        public long Id { get; set; }

        public string Ten { get; set; }

        public long? Cap { get; set; }
    }
}