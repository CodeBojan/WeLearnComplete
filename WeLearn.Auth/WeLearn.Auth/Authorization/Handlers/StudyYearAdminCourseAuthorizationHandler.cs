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

public class StudyYearAdminCourseAuthorizationHandler : AuthorizationHandler<ResourceAdminRequirement, Course>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ResourceAdminRequirement requirement, Course resource)
    {
        if (context.User.HasClaim(ClaimTypes.StudyYearAdmin, resource.StudyYearId.ToString()))
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
}