namespace App.Qtht.Services.Models;

public class LichSuDongBoModel
{
    public long Id { get; set; }

    public string DagId { get; set; }
    public bool IsPaused { get; set; } = false;
    public string TrangThai { get; set; } = "ReloadDag_QTHT";
    public string AppID { get; set; } = "2";
}