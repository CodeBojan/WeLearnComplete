using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WeLearn.Auth.Controllers;
using WeLearn.Auth.Policy;
using WeLearn.Data.Models;
using WeLearn.IdentityServer.Dtos.CourseAdminRole;
using WeLearn.IdentityServer.Services.Identity;
using WeLearn.Shared.Dtos.ProblemDetails;
using WeLearn.Shared.Services.Course;

namespace WeLearn.IdentityServer.Controllers;

[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Route("api/[controller]")]
[ApiController]
public class CourseAccountRolesController : UserAuthorizedController
{
    private readonly IRoleManager _roleManager;

    public CourseAccountRolesController(IRoleManager roleManager)
    {
        _roleManager = roleManager;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status409Conflict, Type = typeof(WeLearnProblemDetails))]
    public async Task<IActionResult> PostMakeCourseAdminAsync(
         [FromBody] PostCourseAdminRoleDto postDto,
         [FromServices] IAuthorizationService authorizationService,
         [FromServices] ICourseService courseService)
    {
        var course = await courseService.GetBasicCourseInfo(postDto.CourseId);
        if (course is null)
            return NotFound(new WeLearnProblemDetails()
            {
                Title = "Course not found",
                Detail = postDto.CourseId.ToString()
            });

        var authResult = await authorizationService.AuthorizeAsync(User, new Course { Id = postDto.CourseId, StudyYearId = course.StudyYearId }, Policies.IsResourceAdmin);
        if (!authResult.Succeeded)
        {
            return new JsonResult(new WeLearnProblemDetails()
            {
                Title = "Forbidden",
                Status = StatusCodes.Status403Forbidden,
                Detail = "You are not authorized to make this user a course admin",
            });
        }

        var result = await _roleManager.AddCourseAdminRoleAsync(postDto.AccountId, postDto.CourseId);
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
    [ProducesResponseType(StatusCodes.Status409Conflict, Type = typeof(WeLearnProblemDetails))]
    public async Task<IActionResult> DeleteCourseAdminAsync(
        [FromBody] DeleteCourseAdminRoleDto deleteDto,
        [FromServices] IAuthorizationService authorizationService,
        [FromServices] ICourseService courseService)
    {
        var course = await courseService.GetBasicCourseInfo(deleteDto.CourseId);
        if (course is null)
            return NotFound(new WeLearnProblemDetails()
            {
                Title = "Course not found",
                Detail = deleteDto.CourseId.ToString()
            });

        var authResult = await authorizationService.AuthorizeAsync(User, new Course { Id = deleteDto.CourseId, StudyYearId = course.StudyYearId }, Policies.IsResourceAdmin);
        if (!authResult.Succeeded)
        {
            return new JsonResult(new WeLearnProblemDetails()
            {
                Title = "Forbidden",
                Status = StatusCodes.Status403Forbidden,
                Detail = "You are not authorized to remove this user's course admin role",
            });
        }

        var result = await _roleManager.RemoveCourseAdminRoleAsync(deleteDto.AccountId, deleteDto.CourseId);
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
}
