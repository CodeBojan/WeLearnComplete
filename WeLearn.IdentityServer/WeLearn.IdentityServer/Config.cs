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
            // m2m client credentials flow client
            new Client
            {
                ClientId = "m2m.client",
                ClientName = "Client Credentials Client",

                AllowedGrantTypes = GrantTypes.ClientCredentials,
                ClientSecrets = { new Secret("511536EF-F270-4058-80CA-1C89C192F69A".Sha256()) },

                AllowedScopes = { "openid profile email" }
            },

            // interactive client using code flow + pkce
            new Client
            {
                ClientId = "pwa",
                ClientSecrets = { new Secret("49C1A7E1-0C79-4A89-A3D6-A37998FB86B0".Sha256()) },

                AllowedGrantTypes = GrantTypes.Code,
                // TODO read from configuration
                RedirectUris = { "http://localhost:3000/api/auth/callback/identityServer" },
                PostLogoutRedirectUris = { "http://localhost:3000/auth/sign-out" },

                AllowOfflineAccess = true,
                AllowedScopes = Auth.Authorization.Scopes.Api.ApiScopes.Scopes
                 .Add(new ApiScope("openid"))
                 .Add(new ApiScope("profile"))
                 .Add(new ApiScope("email"))
                 .Select(s => s.Name)
                .ToArray(),
                AlwaysIncludeUserClaimsInIdToken = true,
                UpdateAccessTokenClaimsOnRefresh = true
            },
        };
}
