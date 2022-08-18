using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WeLearn.Auth.Extensions.ClaimsPrincipal;
using WeLearn.Auth.Policy;

namespace WeLearn.Api.Controllers;

[Authorize(Policy = Policies.IsUser)]
public abstract class UserAuthorizedController : ControllerBase
{
    public Guid UserId => User.GetIdAsGuid();
}
