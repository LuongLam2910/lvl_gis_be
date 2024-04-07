using App.Qtht.Services.Manager;
using App.QTHTGis.Services.Manager;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Authorize.Api.Controllers;

[EnableCors("AllowOrigin")]
[Authorize]
[ApiController]
[Route("App/QthtGis/api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
public class AuthorizationController : Controller
{
    private IMenuManager _menuManager;

    public AuthorizationController(IMenuManager menuManager)
    {
        _menuManager = menuManager;
    }
}