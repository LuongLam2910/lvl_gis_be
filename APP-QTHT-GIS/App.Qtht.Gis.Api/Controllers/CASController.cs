using App.Core.Common;
using App.QTHTGis.Services.Manager;
using App.Qtht.Services.Manager.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace App.QTHTGis.Api.Controllers;

[EnableCors("AllowOrigin")]
[AllowAnonymous]
[Route("App/QthtGis/api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
[ApiController]
public class CASController : Controller
{
    private readonly ICasManager _manager;

    public CASController(ICasManager manager)
    {
        _manager = manager;
    }

    [HttpGet]
    [Route("GetApp")]
    public Task<ApiResponse<List<string>>> GetApp(string authCode)
    {
        return _manager.GetAppByAuthCode(authCode);
    }


    [HttpGet]
    [Route("isExpriedAccessToken")]
    public Task<ApiResponse<bool>> isExpriedAccessToken(string accessToken)
    {
        return _manager.isExpriedAccessToken(accessToken);
    }

    [HttpPost]
    [Route("RevokeToken")]
    public Task<ApiResponse<bool>> RevokeToken([FromBody] RevokeTokenModel model)
    {
        return _manager.RevokeToken(model);
    }

    [HttpGet]
    [Route("GetUserInfo")]
    public Task<ApiResponse<List<string>>> GetUserInfo(string idToken)
    {
        return _manager.GetUserInfo(idToken);
    }
}