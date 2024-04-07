using App.Core.Common;
using App.QTHTGis.Services.Manager;
using App.Qtht.Services.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Authorize.Api.Controllers;

[EnableCors("AllowOrigin")]
[Authorize]
[ApiController]
[Route("App/QthtGis/api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
public class MenuController : Controller
{
    private readonly IMenuManager _menuManager;

    public MenuController(IMenuManager menuManager)
    {
        _menuManager = menuManager;
    }

    [HttpGet("GetMenuByUserLogin")]
    public async Task<ApiResponse<List<MenuModel.ListMenuModel>>> GetMenuByUserLogin()
    {
        return await _menuManager.GetMenuByUserLogin();
    }

    //[HttpGet("GetMenuByUserLogin_Fuse")]
    //public async Task<MenuModel.FuseNavigation> GetMenuByUserLogin_Fuse()
    //{
    //    return await _menuManager.GetMenuByUserLogin_Fuse();
    //}
}