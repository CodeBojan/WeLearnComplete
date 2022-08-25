using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeLearn.Auth.Authorization.Claims;
using WeLearn.Auth.Authorization.Requirements;
using WeLearn.Data.Models;

namespace WeLearn.Auth.Authorization.Handlers;

public class StudyYearAdminAuthorizationHandler : AuthorizationHandler<ResourceAdminRequirement, StudyYear>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ResourceAdminRequirement requirement, StudyYear resource)
    {
        if (context.User.HasClaim(ClaimTypes.StudyYearAdmin, resource.Id.ToString()))
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
}
