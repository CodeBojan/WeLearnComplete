using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeLearn.Auth.Authorization.Requirements;
using WeLearn.Auth.Extensions.ClaimsPrincipal;
using WeLearn.Data.Models;

namespace WeLearn.Auth.Authorization.Handlers;

public class SystemAdminCourseAuthorizationHandler : AuthorizationHandler<ResourceAdminRequirement, Course>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ResourceAdminRequirement requirement, Course resource)
    {
        if (context.User.GetRoles().IsInRole(Roles.Roles.Admin))
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
}
