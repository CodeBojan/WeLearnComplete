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

public class CourseAdminClaimAuthorizationHandler : AuthorizationHandler<ResourceAdminRequirement, Course>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ResourceAdminRequirement requirement, Course resource)
    {
        // TODO allow study year admins using studyYearId  of the course
        if (context.User.HasClaim(ClaimTypes.CourseAdmin, resource.Id.ToString()))
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
}
