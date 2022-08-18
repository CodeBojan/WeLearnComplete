using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WeLearn.Api.Dtos.FollowedCourse;
using WeLearn.Api.Exceptions.Models;
using WeLearn.Api.Services.FollowedCourse;
using WeLearn.Auth.Extensions.ClaimsPrincipal;
using WeLearn.Auth.Policy;
using WeLearn.Shared.Dtos.Paging;

namespace WeLearn.Api.Controllers;

// TODO define policy for all API controllers
[Route("api/[controller]")]
[ApiController]
public class FollowedCoursesController : UserAuthorizedController
{
    private readonly IFollowedCourseService _followedCoursesService;

    public FollowedCoursesController(IFollowedCourseService followedCoursesService)
    {
        _followedCoursesService = followedCoursesService;
    }

    [HttpGet]
    public async Task<ActionResult<PagedResponseDto<GetFollowedCourseDto>>> GetUserFollowedCourses([FromQuery] PageOptionsDto pageOptions)
    {
        var dto = await _followedCoursesService.GetUserFollowedCoursesAsync(UserId, pageOptions);

        return Ok(dto);
    }

    [HttpPost]
    public async Task<ActionResult<GetFollowedCourseDto>> PostFollowedCourseAsync([FromBody] PostFollowedCourseDto postDto)
    {
        var dto = await _followedCoursesService.FollowCourseAsync(UserId, postDto.CourseId);

        return Ok(dto);
    }

    [HttpDelete]
    public async Task<ActionResult<GetFollowedCourseDto>> DeleteFollowedCourseAsync([FromBody] DeleteFollowedCourseDto deleteDto)
    {
        try
        {
            var dto = await _followedCoursesService.UnfollowCourseAsync(UserId, deleteDto.CourseId);
            return Ok(dto);
        }
        catch (NotFollowingCourseException)
        {
            return NotFound();
        }
    }
}
