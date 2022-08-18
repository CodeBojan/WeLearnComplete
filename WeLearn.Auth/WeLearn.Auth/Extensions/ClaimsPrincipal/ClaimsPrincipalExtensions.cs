using System;
using System.Linq;
using System.Security.Claims;

namespace WeLearn.Auth.Extensions.ClaimsPrincipal;

public static class ClaimsPrincipalExtensions
{
    public static string? GetId(this System.Security.Claims.ClaimsPrincipal claimsPrincipal)
    {
        return claimsPrincipal.FindFirst(c => c.Type == "sub")?.Value;
    }

    public static Guid GetIdAsGuid(this System.Security.Claims.ClaimsPrincipal claimsPrincipal)
    {
        return new Guid(claimsPrincipal.FindFirst(c => c.Type == "sub").Value);
    }

    public static string? GetEmail(this System.Security.Claims.ClaimsPrincipal claimsPrincipal)
    {
        return claimsPrincipal.FindFirst(c => c.Type == "email")?.Value;
    }

    public static string[]? GetRoles(this System.Security.Claims.ClaimsPrincipal claimsPrincipal)
    {
        return claimsPrincipal.FindAll(c => c.Type == Auth.Authorization.Claims.ClaimTypes.Role).Select(c => c.Value).ToArray();
    }
}
