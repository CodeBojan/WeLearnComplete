using Microsoft.AspNetCore.Authorization;
using WeLearn.Auth.Authorization.Requirements;
using WeLearn.Auth.Extensions.ClaimsPrincipal;
using WeLearn.Data.Models;

namespace WeLearn.Api.Authorization.Handlers;

public class CommentResourceAdminAuthorizationHandler : AuthorizationHandler<ResourceAdminRequirement, Comment>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ResourceAdminRequirement requirement, Comment resource)
    {
        // TODO expand for course admins and others
        if (context.User.GetIdAsGuid() == resource.AuthorId)
        {

            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
}
