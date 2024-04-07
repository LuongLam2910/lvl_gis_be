using App.Core.Common;
using App.QTHTGis.Services.Manager;
using App.Qtht.Services.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace App.QTHTGis.Api.Controllers;

[Route("App/QthtGis/api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
[ApiController]
[AllowAnonymous]
public class UserClientController : Controller
{
    private readonly ISysUserManager _userManager;

    public UserClientController(ISysUserManager userManager)
    {
        _userManager = userManager;
    }

    [Route("InsertUserAndUserGroup")]
    [HttpPost]
    public async Task<ApiResponse> InsertUserAndUserGroup([FromBody] SysUserModel.InsertUpdateModel model)
    {
        if (ModelState.IsValid)
        {
            return await _userManager.InsertUserAndUserGroup(model);
        }

        return null;
    }
}