using App.QTHTGis.Dal.EntityClasses;
using App.QTHTGis.Services.Manager;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace App.QTHTGis.Api.Controllers;

[EnableCors("AllowOrigin")]
[ApiController]
[Route("App/QthtGis/api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
[AllowAnonymous]
public class HomeShareController : DefaultController<HomeShareEntity>
{
    private readonly HomeShareManager _manager;

    public HomeShareController()
    {
        _manager = new HomeShareManager();
    }
}