using Duende.IdentityServer.Models;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text;

namespace WeLearn.Auth.Authorization.Scopes.Api
{
    public static class ApiScopes
    {
        // TODO define more scopes
        public static readonly ImmutableList<ApiScope> Scopes = ImmutableList.Create(
            new ApiScope("year.read"),
            new ApiScope("year.write"),
            new ApiScope("course.read"),
            new ApiScope("course.write"),
            new ApiScope("content.read"),
            new ApiScope("profile.read"),
            new ApiScope("profile.write"),
            
            new ApiScope("weather.read") // TODO remove
            );
    }
}
