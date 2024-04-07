using App.Core.Common;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Gis.Services.Business
{
    public static class UserInfo
    {
        public static int GetUserId(ICurrentContext currentContext)
        {
            if (currentContext != null)
            {
                try
                {
                    if(currentContext.UserId > 0) return currentContext.UserId;
                    string token = currentContext.Token.Replace("Bearer ", "");
                    var handler = new JwtSecurityTokenHandler();
                    var jwtSecurityToken = handler.ReadJwtToken(token);
                    var claims = jwtSecurityToken.Claims.ToList();

                    foreach (var claim in claims)
                    {
                        if (claim.Type == "UserId") return int.Parse(claim.Value);
                    }
                }
                catch (Exception ex)
                {
                }
            }
            return 0;
        }
    }
}
