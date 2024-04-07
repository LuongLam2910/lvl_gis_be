using App.Core.Common;
using App.Qtht.Services.Authorize;
using App.QTHTGis.Services.Manager;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace App.QTHTGis.Api.Controllers;

[EnableCors("AllowOrigin")]
[Authorize]
[ApiController]
[Route("App/AuthGis/api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
public class AuthController : Controller
{
    private readonly IAuthManager _authManager;
    public AuthController(IAuthManager authManager)
    {
        _authManager = authManager;
    }
    
    [AllowAnonymous]
    [HttpPost("Authenticate")]
    public ApiResponse<AuthenticateResponse> Authenticate([FromBody] AuthenticateRequest model)
    {
        if (ModelState.IsValid)
        {
            ApiResponse<AuthenticateResponse> response = _authManager.Authenticate(model,ipAddress());

            if (response.Code != GeneralCode.Success.GenerateString())
            {
                return response;
            }

            setTokenCookie(response.Data.RefreshToken);

            return response;
        }

        return null;
    }

    [AllowAnonymous]
    [HttpPost("BaseAuthenticate")]
    public ApiResponse<BaseAuthenticateResponse> BaseAuthenticate([FromBody] AuthenticateRequest model)
    {
        if (ModelState.IsValid)
        {
            ApiResponse<BaseAuthenticateResponse> response = _authManager.BaseAuthenticate(model, ipAddress());
            if (response.Code != GeneralCode.Success.GenerateString())
            {
                return response;
            }

            setTokenCookie(response.Data.RefreshToken);
            return response;
        }

        return ApiResponse<BaseAuthenticateResponse>.Generate(GeneralCode.Error, "Input incorrect.");
    }

    [AllowAnonymous]
    [HttpPost("BaseAuthenticate_Fuse")]
    public ApiResponse<BaseAuthenticateResponse> BaseAuthenticate_Fuse([FromBody] AuthenticateRequest model)
    {
        if (ModelState.IsValid)
        {
            ApiResponse<BaseAuthenticateResponse> response = _authManager.BaseAuthenticate_Fuse(model, ipAddress());
            if (response.Code != GeneralCode.Success.GenerateString())
            {
                return response;
            }

            setTokenCookie(response.Data.RefreshToken);
            return response;
        }

        return ApiResponse<BaseAuthenticateResponse>.Generate(GeneralCode.Error, "Input incorrect.");
    }

    [AllowAnonymous]
    [HttpPost("AuthenHome")]
    public ApiResponse<AuthenticateResponse> AuthenHome([FromBody] AuthenticateRequest model)
    {
        ApiResponse<AuthenticateResponse> response = _authManager.AuthenHome(model, ipAddress());

        if (response.Code != GeneralCode.Success.GenerateString())
        {
            return response;
        }

        setTokenCookie(response.Data.RefreshToken);

        return response;
    }

    [AllowAnonymous]
    [HttpPost("AuthenticateMobile")]
    public ApiResponse<AuthenticateResponse> AuthenticateMobile([FromBody] AuthenticateRequest model)
    {
        ApiResponse<AuthenticateResponse> response = _authManager.AuthenticateMobile(model, ipAddress());

        if (response.Code != GeneralCode.Success.GenerateString())
        {
            return response;
        }

        setTokenCookie(response.Data.RefreshToken);

        return response;
    }


    [AllowAnonymous]
    [HttpPost("Register")]
    public ApiResponse<RegisterResponse> Register([FromBody] RegisterRequest model)
    {
        ApiResponse<RegisterResponse> response = _authManager.Register(model, ipAddress());

        if (response.Code != GeneralCode.Success.GenerateString())
        {
            return response;
        }

        return response;
    }

    [AllowAnonymous]
    [HttpPost("Refresh-token")]
    public ApiResponse<AuthenticateResponse> RefreshToken([FromBody] TokenForRefresh token)
    {
        var refreshToken = Request.Cookies["refreshToken"] ?? token.RefreshToken;
        AuthenticateResponse response = _authManager.RefreshToken(refreshToken, ipAddress(), token.Tag);

        if (response == null)
        {
            return ApiResponse<AuthenticateResponse>.Generate(null, GeneralCode.Error, "Token không hợp lệ");
        }

        setTokenCookie(response.RefreshToken);

        return response;
    }

    [AllowAnonymous]
    [HttpPost("Refresh-token-base")]
    public ApiResponse<BaseAuthenticateResponse> RefreshTokenBase([FromBody] TokenForRefresh token)
    {
        var refreshToken = Request.Cookies["refreshToken"] ?? token.RefreshToken;
        BaseAuthenticateResponse response = _authManager.RefreshTokenBase(refreshToken, ipAddress(), token.Tag);

        if (response == null)
        {
            return ApiResponse<BaseAuthenticateResponse>.Generate(null, GeneralCode.Error, "Token không hợp lệ");
        }

        setTokenCookie(response.RefreshToken);

        return response;
    }

    [HttpPost("Revoke-token")]
    public ApiResponse RevokeToken()
    {
        // accept token from request body or cookie
        var token = Request.Cookies["refreshToken"];

        if (String.IsNullOrEmpty(token))
        {
            return ApiResponse.Generate(GeneralCode.Error, "Yêu cầu Token");
        }

        var response = _authManager.RevokeToken(token, ipAddress());

        if (!response)
        {
            return ApiResponse.Generate(GeneralCode.Error, "Không tìm thấy Token");
        }

        return ApiResponse.Generate(GeneralCode.Error, "Đã thu hồi Token");
    }

    [AllowAnonymous]
    [HttpPost("GetTokenSSO")]
    public async Task<string> GetTokenSSO([FromBody] SsoModel model)
    {
        if (!ModelState.IsValid)
        {
            return null;
        }

        return await _authManager.GetTokenSSO(model);
    }

    //[HttpGet]
    //public IActionResult GetAll()
    //{
    //    var users = _authManager.GetAll();
    //    return Ok(users);
    //}

    //[HttpGet("{id}")]
    //public IActionResult GetById(int id)
    //{
    //    var user = _authManager.GetById(id);
    //    if (user == null) return NotFound();

    //    return Ok(user);
    //}

    //[HttpGet("{id}/refresh-tokens")]
    //public IActionResult GetRefreshTokens(int id)
    //{
    //    var user = _authManager.GetById(id);
    //    if (user == null) return NotFound();

    //    return Ok(user.RefreshTokens);
    //}

    // helper methods

    private void setTokenCookie(string token)
    {
        CookieOptions cookieOptions = new() {
            HttpOnly = true,
            Expires = DateTime.UtcNow.AddDays(7)
        };
        Response.Cookies.Append("refreshToken", token, cookieOptions);
    }

    private string ipAddress()
    {
        if (Request.Headers.ContainsKey("X-Forwarded-For"))
        {
            return Request.Headers["X-Forwarded-For"];
        }

        return HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
    }
}