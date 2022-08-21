using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WeLearn.Api.Dtos.Course;
using WeLearn.Api.Exceptions.Models;
using WeLearn.Api.Services.Course;
using WeLearn.Shared.Dtos.Account;
using WeLearn.Shared.Dtos.Paging;

namespace WeLearn.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CoursesController : UserAuthorizedController
{
    private readonly ICourseService _courseService;

    public CoursesController(ICourseService courseService)
    {
        _courseService = courseService;
    }

    [HttpGet]
    public async Task<ActionResult<PagedResponseDto<GetCourseDto>>> GetCoursesAsync([FromQuery] PageOptionsDto pageOptions, [FromQuery] bool isFollowing = false)
    {
        var dto = await _courseService.GetCoursesAsync(pageOptions, UserId, isFollowing);

        return Ok(dto);
    }

    [HttpGet("{courseId}")]
    public async Task<ActionResult<GetCourseDto>> GetCourseAsync([FromRoute] Guid courseId)
    {
        try
        {
            var dto = await _courseService.GetCourseAsync(courseId);
            return dto;
        }
        catch (CourseNotFoundException)
        {
            return NotFound();
        }
    }

    [HttpGet("{courseId}/accounts")]
    public async Task<ActionResult<PagedResponseDto<GetAccountDto>>> GetFollowingAccountsAsync([FromRoute] Guid courseId, [FromQuery] PageOptionsDto pageOptions)
    {
        try
        {
            var dto = await _courseService.GetFollowingAccountsAsync(courseId, pageOptions);
            return dto;
        }
        catch (CourseNotFoundException)
        {
            return NotFound();
        }
    }

    [HttpPost]
    public async Task<ActionResult<GetCourseDto>>
        PostCourseAsync([FromBody] PostCourseDto postDto)
    {
        try
        {
            var dto = await _courseService.CreateCourseAsync(
                postDto.Code,
                postDto.ShortName,
                postDto.FullName,
                postDto.Staff,
                postDto.Description,
                postDto.Rules,
                postDto.StudyYearId);

            return Ok(dto);
        }
        catch (CourseCreationException)
        {
            return Conflict();
        }
    }

    [HttpPut("{courseId}")]
    public async Task<ActionResult<GetCourseDto>> PutCourseAsync([FromRoute] Guid courseId, [FromBody] PutCourseDto putDto)
    {
        try
        {
            var dto = await _courseService.UpdateCourseAsync(
                courseId,
                putDto.Code,
                putDto.ShortName,
                putDto.FullName,
                putDto.Staff,
                putDto.Description,
                putDto.Rules);

            return Ok(dto);
        }
        catch (CourseUpdateException)
        {
            return Conflict();
        }
    }
}
