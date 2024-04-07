using System.Collections.Generic;

namespace App.Qtht.Services.Models;

public class DmToChucModel
{
    public long Id { get; set; }

    public string Ten { get; set; }

    public short? TrangThai { get; set; }
}

public class DmChucVuDetailModel
{
    public long Id { get; set; }
    public string Ten { get; set; }
    public short Trangthai { get; set; }
}

public class DmToChucChucVuModel
{
    public long IdToChuc { get; set; }
    public long IdChucVu { get; set; }
    public short Cap { get; set; }
}

public class DmToChucInsertUpdateModel
{
    public DmToChucModel DmToChuc { get; set; }
    public List<DmToChucChucVuModel> ListToChucChucVu { get; set; }
}