using IdentityModel.OidcClient;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeLearn.Auth.Extensions;
public static class JwtBearerOptionsExtensions
{
    public static JwtBearerOptions AddIdentityServerAuthentication(this JwtBearerOptions options, string authority)
    {
        // TODO set auidence
        options.Authority = authority; // TODO use configuration

        options.TokenValidationParameters.ValidateAudience = false;

        options.TokenValidationParameters.ValidTypes = new[] { "at+jwt" };

        options.Events = new JwtBearerEvents
        {
            OnTokenValidated = async (context) =>
            {
                //context.HttpContext.RequestServices // TODO use
                // TODO use usermanager here with sub claim
                if (context.SecurityToken is JwtSecurityToken jwt)
                {
                    var accessToken = jwt.RawData;
                    var oidcClient = new OidcClient(new OidcClientOptions
                    {
                        Authority = authority,
                    });
                    var userInfoResult = await oidcClient.GetUserInfoAsync(accessToken);
                    if (userInfoResult.IsError)
                        throw new Exception(userInfoResult.ErrorDescription); // TODO

                    var claims = userInfoResult.Claims;
                    var claimsIdentity = new System.Security.Claims.ClaimsIdentity(claims);
                    context.Principal.AddIdentity(claimsIdentity);
                }
            }
        };

        return options;
    }
}
