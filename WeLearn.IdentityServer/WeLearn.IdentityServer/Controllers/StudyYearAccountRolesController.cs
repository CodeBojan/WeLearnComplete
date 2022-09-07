using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WeLearn.Auth.Controllers;
using WeLearn.Auth.Policy;
using WeLearn.Data.Models;
using WeLearn.IdentityServer.Dtos.StudyYearAdminRole;
using WeLearn.IdentityServer.Services.Identity;
using WeLearn.Shared.Dtos.ProblemDetails;
using WeLearn.Shared.Services.StudyYear;

namespace WeLearn.IdentityServer.Controllers;

[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Route("api/[controller]")]
[ApiController]
public class StudyYearAccountRolesController : UserAuthorizedController
{
    private readonly IRoleManager _roleManager;

    public StudyYearAccountRolesController(IRoleManager roleManager)
    {
        _roleManager = roleManager;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status409Conflict, Type = typeof(WeLearnProblemDetails))]
    public async Task<IActionResult> PostMakeStudyYearAdminAsync(
        [FromBody] PostStudyYearAdminRoleDto postDto,
        [FromServices] IAuthorizationService authorizationService, [FromServices] IStudyYearsService studyYearsService)
    {
        if (!await studyYearsService.StudyYearExistsAsync(postDto.StudyYearId))
            return NotFound(new WeLearnProblemDetails()
            {
                Title = "Study year not found",
                Detail = postDto.StudyYearId.ToString()
            });

        var authResult = await authorizationService.AuthorizeAsync(User, new StudyYear { Id = postDto.StudyYearId }, Policies.IsResourceAdmin);
        if (!authResult.Succeeded)
            return new JsonResult(new WeLearnProblemDetails()
            {
                Title = "Forbidden",
                Status = StatusCodes.Status403Forbidden,
                Detail = "You are not authorized to make this user a study year admin",
            });

        var result = await _roleManager.AddStudyYearAdminRoleAsync(postDto.AccountId, postDto.StudyYearId);
        if (!result.Succeeded)
        {
            return Conflict(new WeLearnProblemDetails()
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Role Conflict",
                Detail = result.Errors
                .FirstOrDefault().Description,
            });
        }

        return Ok();
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteRemoveStudyYearAdminAsync(
        [FromBody] DeleteStudyYearAdminRoleDto postDto,
        [FromServices] IAuthorizationService authorizationService, [FromServices] IStudyYearsService studyYearsService)
    {
        if (!await studyYearsService.StudyYearExistsAsync(postDto.StudyYearId))
            return NotFound(new WeLearnProblemDetails()
            {
                Title = "Study year not found",
                Detail = postDto.StudyYearId.ToString()
            });

        var authResult = await authorizationService.AuthorizeAsync(User, new StudyYear { Id = postDto.StudyYearId }, Policies.IsResourceAdmin);
        if (!authResult.Succeeded)
            return new JsonResult(new WeLearnProblemDetails()
            {
                Title = "Forbidden",
                Status = StatusCodes.Status403Forbidden,
                Detail = "You are not authorized to make this user a study year admin",
            });

        var result = await _roleManager.RemoveStudyYearAdminRoleAsync(postDto.AccountId, postDto.StudyYearId);
        if (!result.Succeeded)
        {
            return NotFound(new WeLearnProblemDetails()
            {
                Status = StatusCodes.Status404NotFound,
                Title = "Role Conflict",
                Detail = result.Errors
                .FirstOrDefault().Description,
            });
        }

        return Ok();
    }
}
