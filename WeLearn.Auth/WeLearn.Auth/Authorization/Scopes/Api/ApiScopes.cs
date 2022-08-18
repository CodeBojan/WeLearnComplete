using Duende.IdentityServer.Models;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text;
using WeLearn.Auth.Authorization.Claims;

namespace WeLearn.Auth.Authorization.Scopes.Api
{
    public static class ApiScopes
    {
        // TODO define more scopes
        public static readonly ImmutableList<ApiScope> Scopes = ImmutableList.Create(
            new ApiScope(ClaimTypes.StudyYear.Name),
            new ApiScope(ClaimTypes.Course.Name),
            new ApiScope(ClaimTypes.Config.Name),
            new ApiScope(ClaimTypes.Credentials.Name),
            new ApiScope(ClaimTypes.Account.Name)
            );
    }
}
