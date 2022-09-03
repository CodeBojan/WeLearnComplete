using Duende.IdentityServer.Models;
using System.Collections.Immutable;
using WeLearn.Auth.Authorization.Claims;

namespace WeLearn.IdentityServer;

public static class Config
{
    private static IdentityResource ProfileResource = new IdentityResources.Profile();
    public static IEnumerable<IdentityResource> IdentityResources =>
        new IdentityResource[]
        {
            new IdentityResources.OpenId(),
            new IdentityResource("profile",
                ImmutableList.Create(ProfileResource.UserClaims.ToArray())
                .AddRange(new string[]{
                    ClaimTypes.StudyYearAdmin,
                    ClaimTypes.CourseAdmin,
                    ClaimTypes.Role
                })),
            new IdentityResources.Email(),
        };

    public static IEnumerable<ApiScope> ApiScopes =>
            Auth.Authorization.Scopes.Api.ApiScopes.Scopes;

    public static IEnumerable<Client> Clients =>
        new Client[]
        {
        };
}
