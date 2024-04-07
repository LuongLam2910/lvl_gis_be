using System;

namespace App.Qtht.Services.Manager.Models;

public class DownloadAttachInfo
{
    public string[] lstAttachUrl { get; set; }
}

public class SearchVBModel
{
    public int[] DonVi { get; set; }
    public int KieuTimKiem { get; set; }
    public string TuNgay { get; set; }
    public string DenNgay { get; set; }
    public int[] LoaiVanBan { get; set; }
    public string Keyword { get; set; }
    public string[] SearchIn { get; set; }
    public string t_gridRequest { get; set; }
}

public class ValidAccessTokenResponse
{
    public long nbf { get; set; }
    public string scope { get; set; }
    public bool active { get; set; }
    public string token_type { get; set; }
    public long exp { get; set; }
    public long iat { get; set; }
    public string client_id { get; set; }
    public string username { get; set; }
}

public class RevokeTokenModel
{
    public string token { get; set; }

    public string tokenTypeHint { get; set; }

    public string client_id { get; set; }

    public string client_secret { get; set; }
}

public class TokenResponse
{
    public string id_token { get; set; }
    public string access_token { get; set; }
    public string scope { get; set; }
    public string token_type { get; set; }
    public int? expires_in { get; set; }
}

public class LGSPResponeInfo
{
    public string status { get; set; }
    public string errorCode { get; set; }
    public string errorDesc { get; set; }
    public string data { get; set; }
    public int total { get; set; }
}

public class LinhVucInfo
{
    public int ID { get; set; }
    public string Title { get; set; }
    public string TitleLinhVuc { get; set; }
    public string DMDescription { get; set; }
    public string DMVietTat { get; set; }
    public int OrderColumn { get; set; }
    public string TitleEng { get; set; }
    public bool DMIsDisplay { get; set; }
    public Vbpqdonvi[] VBPQDonVi { get; set; }
    public string MultipleDonVi { get; set; }
    public Vbpqnganh VBPQNganh { get; set; }
    public string Version { get; set; }
    public Author Author { get; set; }
    public Editor Editor { get; set; }
    public DateTime Created { get; set; }
    public DateTime Modified { get; set; }
    public string[] ListFileAttachAdd { get; set; }
    public string[] ListFileAttach { get; set; }
    public string[] ListFileRemove { get; set; }
}

public class CoQuanBanHanhInfo
{
    public int ID { get; set; }
    public string Title { get; set; }
    public string DMDescription { get; set; }
    public string DMVietTat { get; set; }
    public int OrderColumn { get; set; }
    public string TitleEng { get; set; }
    public bool DMIsDisplay { get; set; }
    public Dmcapcoquan DMCapCoQuan { get; set; }
    public Vbpqdonvi[] VBPQDonVi { get; set; }
    public Donviquanly DonViQuanLy { get; set; }
    public string MultipleDonVi { get; set; }
    public bool HienThiHopNhat { get; set; }
    public bool HienThiHopNhatTA { get; set; }
    public bool HienThiSearchTA { get; set; }
    public bool HienThiSearchTV { get; set; }
    public bool IsSearch { get; set; }
    public bool IsSearchMenuleftVBHN { get; set; }
    public int ModerationStatus { get; set; }
    public string IsSelectedInSearch { get; set; }
    public string Version { get; set; }
    public Author Author { get; set; }
    public Editor Editor { get; set; }
    public DateTime Created { get; set; }
    public DateTime Modified { get; set; }
    public string[] ListFileAttachAdd { get; set; }
    public string[] ListFileAttach { get; set; }
    public string[] ListFileRemove { get; set; }
}

public class Dmcapcoquan
{
    public int LookupId { get; set; }
    public string LookupValue { get; set; }
}

public class Donviquanly
{
    public int LookupId { get; set; }
    public string LookupValue { get; set; }
}

public class LoaiVanBanInfo
{
    public int ID { get; set; }
    public string Title { get; set; }
    public string DMDescription { get; set; }
    public string DMVietTat { get; set; }
    public int OrderColumn { get; set; }
    public string TitleEng { get; set; }
    public bool DMIsDisplay { get; set; }
    public Vbpqdonvi[] VBPQDonVi { get; set; }
    public string MultipleDonVi { get; set; }
    public bool HienThiHopNhat { get; set; }
    public bool HienThiHopNhatTA { get; set; }
    public bool HienThiSearchTV { get; set; }
    public bool HienThiSearchTA { get; set; }
    public bool IsSearch { get; set; }
    public bool IsSearchMenuleftVBHN { get; set; }
    public int ModerationStatus { get; set; }
    public string Version { get; set; }
    public Author Author { get; set; }
    public Editor Editor { get; set; }
    public DateTime Created { get; set; }
    public DateTime Modified { get; set; }
    public string[] ListFileAttachAdd { get; set; }
    public string[] ListFileAttach { get; set; }
    public string[] ListFileRemove { get; set; }
}

public class NguoiKyInfo
{
    public int ID { get; set; }
    public string Title { get; set; }
    public string DMDescription { get; set; }
    public string DMVietTat { get; set; }
    public int OrderColumn { get; set; }
    public string TitleEng { get; set; }
    public bool DMIsDisplay { get; set; }
    public Vbpqdonvi[] VBPQDonVi { get; set; }
    public string[] CoQuanBanHanh { get; set; }
    public string MultipleDonVi { get; set; }
    public int ModerationStatus { get; set; }
    public string Version { get; set; }
    public Author Author { get; set; }
    public Editor Editor { get; set; }
    public DateTime Created { get; set; }
    public DateTime Modified { get; set; }
    public string[] ListFileAttachAdd { get; set; }
    public string[] ListFileAttach { get; set; }
    public string[] ListFileRemove { get; set; }
}

public class ChucDanhInfo
{
    public int ID { get; set; }
    public string Title { get; set; }
    public string DMDescription { get; set; }
    public string DMVietTat { get; set; }
    public int OrderColumn { get; set; }
    public string TitleEng { get; set; }
    public bool DMIsDisplay { get; set; }
    public Vbpqdonvi[] VBPQDonVi { get; set; }
    public string[] CoQuanBanHanh { get; set; }
    public string MultipleDonVi { get; set; }
    public int ModerationStatus { get; set; }
    public string Version { get; set; }
    public Author Author { get; set; }
    public Editor Editor { get; set; }
    public DateTime Created { get; set; }
    public DateTime Modified { get; set; }
    public string[] ListFileAttachAdd { get; set; }
    public string[] ListFileAttach { get; set; }
    public string[] ListFileRemove { get; set; }
}

public class CoQuanBienTapInfo
{
    public int ID { get; set; }
    public string TreeGroupId { get; set; }
    public int DonViCap { get; set; }
    public int STT { get; set; }
    public string Title { get; set; }
    public string TitleEng { get; set; }
    public string DonviVietTat { get; set; }
    public string DonViUrl { get; set; }
    public bool DonViIsUsedCA { get; set; }
    public bool DMIsDisplay { get; set; }
    public bool DonViIsTW { get; set; }
    public string DonViDomain { get; set; }
    public bool DonViChinhThuc { get; set; }
    public string DonviPrefix { get; set; }
    public Parentid ParentID { get; set; }
    public string Version { get; set; }
    public Author Author { get; set; }
    public Editor Editor { get; set; }
    public DateTime Created { get; set; }
    public DateTime Modified { get; set; }
    public string[] ListFileAttachAdd { get; set; }
    public string[] ListFileAttach { get; set; }
    public string[] ListFileRemove { get; set; }
}

public class Parentid
{
    public int LookupId { get; set; }
    public string LookupValue { get; set; }
}

public class SearchResult
{
    public Ltsvanban[] LtsVanBan { get; set; }
    public int TotalRecord { get; set; }
}

public class Ltsvanban
{
    public bool IsCanhBao { get; set; }
    public string LogVersion { get; set; }
    public bool Exits { get; set; }
    public string LtsVanBanGoc { get; set; }
    public int ID { get; set; }
    public string Title { get; set; }
    public string VBPQLyDoHetHieuLuc1Phan { get; set; }
    public string VBPQNgayHetHieuLuc1PhanText { get; set; }
    public string Title1 { get; set; }
    public string VBPQDiaDanh { get; set; }
    public string VBPQCTListUrl { get; set; }
    public string VBPQCTItemID { get; set; }
    public string VBPQNgayapdung { get; set; }
    public DateTime VBPQNgayBanHanh { get; set; }
    public DateTime VBPQNgaycohieuluc { get; set; }
    public string VBPQNgaydangcongbao { get; set; }
    public string VBPQNgayHetHieuLuc { get; set; }
    public string VBPQNgayHetHieuLuc1phan { get; set; }
    public string VBPQNguontrich { get; set; }
    public string VBPQPhanHetHieuLuc { get; set; }
    public string VBPQSokyhieu { get; set; }
    public string VBPQToanVan { get; set; }
    public string VBPQTrichYeu { get; set; }
    public int VBPQTypeOfVanBan { get; set; }
    public int ReadCount { get; set; }
    public string VBPQLaVanBanDuoc { get; set; }
    public string IsMultiDonVi { get; set; }
    public string VBPQDanhMuc { get; set; }
    public string VBPQPhamvidieuchinh { get; set; }
    public string VBPQChedinh { get; set; }
    public Vbpqtinhtranghieuluc VBPQTinhTrangHieuLuc { get; set; }
    public string VBPQLidohetHieuLuc { get; set; }
    public string VBPQPhanHetHieuLuc1Phan { get; set; }
    public string VBPQVanbanduochuongdan { get; set; }
    public string VBPQVanBanDuocQuyDinhChiTiet { get; set; }
    public string VBPQVanBanQuyDinhChiTiet { get; set; }
    public string VBPQVanBanBiHetHieuLuc { get; set; }
    public string VBPQVanBanLamHetHieuLuc { get; set; }
    public string VBPQVanBanBiHetHieuLuc1Phan { get; set; }
    public string VBPQVanBanLamHetHieuLuc1Phan { get; set; }
    public string VBPQVanBanCanCu { get; set; }
    public string VBPQVanbandanchieu { get; set; }
    public string VBPQVanBanbithaythe { get; set; }
    public string VBPQVanbanBiBaibo { get; set; }
    public string VBPQVanbanbiHuyBo { get; set; }
    public string VBPQVanbanbiDinhchi { get; set; }
    public string VBPQVanbanbiThaythe1phan { get; set; }
    public string VBPQVanbanbibaibo1phan { get; set; }
    public string VBPQVanbanbihuybo1phan { get; set; }
    public string VBPQVanbanbidinhchi1phan { get; set; }
    public string VBPQVanbanduocsuadoibosung { get; set; }
    public string VBPQVanbanbidinhchinh { get; set; }
    public Vbpqvanbantienganh VBPQVanBanTiengAnh { get; set; }
    public string VBHopNhatCoQuanHopNhatVBTV { get; set; }
    public string VBHopNhatVanBanNguonTV { get; set; }
    public string VBHopNhatVanBanDichTV { get; set; }
    public string CAInfor { get; set; }
    public Vbpqloaivanban VBPQLoaivanban { get; set; }
    public string VBPQLoaivanbanTitle { get; set; }
    public bool VBPQVFooter { get; set; }
    public bool VBPQVHeader { get; set; }
    public int OldID { get; set; }
    public bool IsDieuUoc { get; set; }
    public Vbpqlinhvuc[] VBPQLinhVuc { get; set; }
    public string VBPQCoquanbanhanh { get; set; }
    public string VBPQCoQuanChuTri { get; set; }
    public string VBPQCoQuanLienQuan { get; set; }
    public string VBPQChucDanh { get; set; }
    public string VBPQNguoiKy { get; set; }
    public string[] VBPQDonVi { get; set; }
    public string VBPQVanBanChuaXacDinh { get; set; }
    public string VBPQVanBanChuaXacDinh1Phan { get; set; }
    public string VBPQVanBanLienQuanKhac { get; set; }
    public int ModerationStatus { get; set; }
    public string VBPQNganh { get; set; }
    public string VBPQChuDe { get; set; }
    public string VBPQDuThao { get; set; }
    public string LoaiVanBanLuocDo { get; set; }
    public string LoaiVanBanLuocDoTA { get; set; }
    public bool IsVanBanGoc { get; set; }
    public bool Favorites { get; set; }
    public bool IsVBPQ { get; set; }
    public string ThuocChuongTrinhCuaQuocHoi { get; set; }
    public string ChuongTrinhTinh { get; set; }
    public string DuThaoTinh { get; set; }
    public string ThuocChuongTrinhCuaChinhPhu { get; set; }
    public string ThuocChuongTrinhCuaBo { get; set; }
    public string MultipleDonVi { get; set; }
    public string LtsFileAttachClone { get; set; }
    public string VBPQVanBanHuongDan { get; set; }
    public string VBPQVanBanThayThe { get; set; }
    public string VBPQVanBanBaiBo { get; set; }
    public string VBPQVanBanHuyBo { get; set; }
    public string VBPQVanBanDinhChi { get; set; }
    public string VBPQVanBanThayThe1Phan { get; set; }
    public string VBPQVanBanBaiBo1Phan { get; set; }
    public string VBPQVanBanHuyBo1Phan { get; set; }
    public string VBPQVanBanDinhChi1Phan { get; set; }
    public string VBPQVanBanSuaDoiBoSung { get; set; }
    public string VBPQVanbanduocsuadoi { get; set; }
    public string VBPQVanBanSuaDoi { get; set; }
    public bool IsToanVan { get; set; }
    public bool IsKiemTra { get; set; }
    public string HTH_GhiChu { get; set; }
    public string HTH_GhiChuThoiDiemCoHieuLuc { get; set; }
    public string HTH_GhiChuNgayHetHieuLuc { get; set; }
    public string HTH_NoiDungKienNghi { get; set; }
    public string HTH_ThoiHanXuLy { get; set; }
    public string HTH_KienNghi { get; set; }
    public string txtAttachments { get; set; }
    public bool VBPQDaKiemTraTaoLap { get; set; }
    public bool HasVanBanLienQuan { get; set; }
    public int IsVanBanLienQuan { get; set; }
    public string txtLienKetTTHC { get; set; }
    public string VBPQ_CauHoiLienKet { get; set; }
    public string HeThongChiaSe { get; set; }
    public string VBPQVanBanHetHieuLuc { get; set; }
    public string VBPQVanBanHetHieuLuc1Phan { get; set; }
    public string Version { get; set; }
    public string Author { get; set; }
    public string Editor { get; set; }
    public DateTime Created { get; set; }
    public DateTime Modified { get; set; }
    public string[] ListFileAttachAdd { get; set; }
    public string[] ListFileAttach { get; set; }
    public string[] ListFileRemove { get; set; }
}

public class Vbpqtinhtranghieuluc
{
    public int LookupId { get; set; }
    public string LookupValue { get; set; }
}

public class Vbpqvanbantienganh
{
    public int LookupId { get; set; }
    public string LookupValue { get; set; }
}

public class Vbpqloaivanban
{
    public int LookupId { get; set; }
    public string LookupValue { get; set; }
}

public class Vbpqlinhvuc
{
    public int LookupId { get; set; }
    public string LookupValue { get; set; }
}

public class Vbpqnganh
{
    public int LookupId { get; set; }
    public string LookupValue { get; set; }
}

public class Author
{
    public int LookupId { get; set; }
    public string LookupValue { get; set; }
}

public class Editor
{
    public int LookupId { get; set; }
    public string LookupValue { get; set; }
}

public class Vbpqdonvi
{
    public int LookupId { get; set; }
    public string LookupValue { get; set; }
}

public class BodyRequest
{
    public int[] DonVi { get; set; }
    public string t_gridRequest { get; set; }
}

public class T_gridRequest
{
    public int page { get; set; }
    public int pageSize { get; set; }
    public Sort[] sort { get; set; }
}

public class Sort
{
    public string field { get; set; }
    public string dir { get; set; }
}

public class FileAttach
{
    public FileItem Result { get; set; }
}

public class FileItem
{
    public string Name { get; set; }
    public string Url { get; set; }
    public string DataFile { get; set; }
}