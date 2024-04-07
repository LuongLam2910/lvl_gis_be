using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;

namespace App.Core.Common;

public interface ICurrentContext
{
    int UserId { get; }
    int AppId { get; }
    string UserName { get; }
    string FullName { get; }
    string Token { get; }
    string UnitCode { get; }
    string UnitName { get; }
    string Tag { get; }

    string IpClient { get; }
    //IList<string> Roles { get; }
}

public class CurrentContext : ICurrentContext
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentContext(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public int UserId => GetClaimValue(Constants.ClaimTypes.UserId) == ""
        ? 0
        : int.Parse(GetClaimValue(Constants.ClaimTypes.UserId));

    public int AppId => GetClaimValue(Constants.ClaimTypes.AppId) == ""
        ? 0
        : int.Parse(GetClaimValue(Constants.ClaimTypes.AppId));

    public string UserName => GetClaimValue(Constants.ClaimTypes.UserName);
    public string FullName => GetClaimValue(Constants.ClaimTypes.FullName);
    public string UnitCode => GetClaimValue(Constants.ClaimTypes.UnitCode);
    public string UnitName => GetClaimValue(Constants.ClaimTypes.UnitName);
    public string Tag => GetClaimValue(Constants.ClaimTypes.Tag);
    public string Token => _httpContextAccessor.HttpContext.Request.Headers[HeaderNames.Authorization].ToString();
    public string IpClient => _httpContextAccessor.HttpContext.Connection.RemoteIpAddress?.ToString();

    private string GetClaimValue(params string[] claimTypes)
    {
        foreach (var claimType in claimTypes)
        {
            var claim = _httpContextAccessor.HttpContext.User.FindFirst(claimType);

            if (claim != null)
                return claim.Value;
        }

        return string.Empty;
    }

    private IList<string> GetClaimValues(string claimType)
    {
        var result = new List<string>();

        var claims = _httpContextAccessor.HttpContext.User.FindAll(claimType);
        foreach (var claim in claims) result.Add(claim.Value);

        return result;
    }
}