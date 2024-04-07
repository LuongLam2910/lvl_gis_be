using App.Core.Common;
using App.QTHTGis.Services.Manager;
using App.Qtht.Services.Manager.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace App.QTHTGis.Api.Controllers;

[Authorize]
[Route("App/QthtGis/api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
[ApiController]
public class TraCuuVBController : Controller
{
    private readonly ITraCuuVBManager _manager;

    public TraCuuVBController(ITraCuuVBManager manager)
    {
        _manager = manager;
    }

    [HttpPost]
    [Route("GetAllLinhVuc")]
    public Task<ApiResponse<List<LinhVucInfo>>> GetAllLinhVuc([FromBody] List<int> lstDonvi)
    {
        return _manager.GetAllLinhVuc(lstDonvi);
    }

    [HttpPost]
    [Route("GetAllCoQuanBienTap")]
    public Task<ApiResponse<List<CoQuanBienTapInfo>>> GetAllCoQuanBienTap()
    {
        return _manager.GetAllCoQuanBienTap();
    }

    [HttpPost]
    [Route("GetAllCoQuanPhatHanh")]
    public Task<ApiResponse<List<CoQuanBanHanhInfo>>> GetAllCoQuanPhatHanh()
    {
        return _manager.GetAllCoQuanPhatHanh();
    }

    [HttpPost]
    [Route("GetAllLoaiVanBan")]
    public Task<ApiResponse<List<LoaiVanBanInfo>>> GetAllLoaiVanBan([FromBody] List<int> lstDonvi)
    {
        return _manager.GetAllLoaiVanBan(lstDonvi);
    }

    [HttpPost]
    [Route("TimKiem")]
    public Task<ApiResponse<SearchResult>> TimKiem([FromBody] SearchVBModel model)
    {
        return _manager.TimKiemVanBan(model);
    }

    [HttpPost]
    [Route("TaiFile")]
    public Task<ApiResponse<List<FileAttach>>> TaiFileDinhKem(List<string> lstAttachs)
    {
        return _manager.TaiFileDinhKem(lstAttachs);
    }

    [HttpGet]
    [Route("GetAllNguoiKy")]
    public Task<ApiResponse<List<NguoiKyInfo>>> GetAllNguoiKy()
    {
        return _manager.GetAllNguoiKy();
    }

    [HttpGet]
    [Route("GetAllChucDanh")]
    public Task<ApiResponse<List<ChucDanhInfo>>> GetAllChucDanh()
    {
        return _manager.GetAllChucDanh();
    }
}