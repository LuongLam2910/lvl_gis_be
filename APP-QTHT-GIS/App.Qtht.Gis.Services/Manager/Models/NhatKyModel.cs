using System;

namespace App.Qtht.Services.Models;

public class NhatKyModel
{
}

public class NhatKyHoatDongModel
{
    public int userId { get; set; }
    public DateTime? NgayTao { get; set; }
    public string NguoiTao { get; set; }
    public string HanhDong { get; set; }
    public string Noidung { get; set; }
    public string DiaChiIP { get; set; }
}