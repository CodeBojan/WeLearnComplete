using System.Security.Claims;

namespace WeLearn.IdentityServer.Extensions.ClaimsPrincipal;

public static class ClaimsPrincipalExtensions
{
    public static string? GetEmail(this System.Security.Claims.ClaimsPrincipal claimsPrincipal)
    {
        return claimsPrincipal.FindFirst(c => c.Type == "email")?.Value;
    }

    public static string[]? GetRoles(this System.Security.Claims.ClaimsPrincipal claimsPrincipal)
    {
        return claimsPrincipal.FindAll(c => c.Type == Auth.Authorization.Claims.ClaimTypes.Role).Select(c => c.Value).ToArray();
    }
}
