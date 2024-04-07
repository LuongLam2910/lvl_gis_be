using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;

namespace App.Core.Common;

[AttributeUsageAttribute(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
public class AuthorizeRole : AuthorizeAttribute
{
    public AuthorizeRole(params string[] roles)
    {
        Roles = string.Join(',', roles);
    }
}

public class CustomClaimsPrincipal : ClaimsPrincipal
{
    public CustomClaimsPrincipal(IPrincipal principal) : base(principal)
    {
    }

    public override bool IsInRole(string role)
    {
        if (FindFirst(Constants.ClaimTypes.UserName).Value.ToLower() == "admin") return true;

        var arRole = role.Split("&");
        var lstRole = GetClaimValues(ClaimTypes.Role);
        var checkRoleMain = lstRole.FirstOrDefault(c => c.StartsWith(arRole[0]));
        if (checkRoleMain != null)
        {
            if (arRole.Length > 1)
            {
                if (checkRoleMain.Contains(arRole[1])) return true;
            }
            else
            {
                return true;
            }
        }

        return base.IsInRole(role);
    }

    private List<string> GetClaimValues(string claimType)
    {
        var result = new List<string>();
        var claims = FindAll(claimType);
        foreach (var claim in claims) result.Add(claim.Value);

        return result;
    }
}

public class ClaimsTransformer : IClaimsTransformation
{
    public Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
    {
        var customPrincipal = new CustomClaimsPrincipal(principal) as ClaimsPrincipal;
        return Task.FromResult(customPrincipal);
    }
}