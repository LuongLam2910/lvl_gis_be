using App.Core.Common;
using App.QTHTGis.Services.Manager;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace App.QTHTGis.Api.Controllers;

[Route("App/QthtGis/api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
[ApiController]
[Authorize]
public class SysRefreshtokensController
{
    private readonly ISysRefreshtokensManager _manager;

    public SysRefreshtokensController(ISysRefreshtokensManager manager)
    {
        _manager = manager;
    }

    [HttpDelete]
    [Route("DeleteByUserId/{userId}")]
    public async Task<ApiResponse> DeleteByUserId([FromRoute] int userId)
    {
        return await _manager.DeleteByUserId(userId);
    }
}