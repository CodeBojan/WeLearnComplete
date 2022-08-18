using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeLearn.Auth.Authorization.Claims;
using WeLearn.Auth.Authorization.Roles;
using WeLearn.Auth.Policy;

namespace WeLearn.Auth.Extensions;

public static class AuthorizationOptionsExtensions
{
    public static AuthorizationOptions AddAuthorizationPolicies(this AuthorizationOptions options)
    {
        options.AddPolicy(Policies.IsUser, policy => policy.RequireClaim(ClaimTypes.Role, Roles.User));
        options.AddPolicy(Policies.IsAdmin, policy => policy.RequireClaim(ClaimTypes.Role, Roles.Admin));

        // TODO note: define "ApiController" policy - requires jwt auth - apply to every controller (maybe using reflection)

        return options;
    }
}
