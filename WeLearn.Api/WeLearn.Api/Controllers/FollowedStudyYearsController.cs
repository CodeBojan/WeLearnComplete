using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WeLearn.Api.Dtos.FollowedStudyYear;
using WeLearn.Api.Exceptions.Models;
using WeLearn.Api.Services.FollowedStudyYear;
using WeLearn.Auth.Extensions.ClaimsPrincipal;
using WeLearn.Auth.Policy;
using WeLearn.Shared.Dtos.Paging;

namespace WeLearn.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FollowedStudyYearsController : UserAuthorizedController
{
    private readonly IFollowedStudyYearService _yearsService;

    public FollowedStudyYearsController(IFollowedStudyYearService yearsService)
    {
        _yearsService = yearsService;
    }

    [HttpGet]
    public async Task<ActionResult<PagedResponseDto<GetFollowedStudyYearDto>>> GetUserFollowedCourses([FromQuery] PageOptionsDto pageOptions)
    {
        var dto = await _yearsService.GetUserFollowedStudyYearsAsync (UserId, pageOptions);

        return Ok(dto);
    }

    [HttpPost]
    public async Task<ActionResult<GetFollowedStudyYearDto>> PostFollowedCourseAsync([FromBody] PostFollowedStudyYearDto postDto)
    {
        var dto = await _yearsService.FollowStudyYearAsync(UserId, postDto.StudyYearId);

        return Ok(dto);
    }

    [HttpDelete]
    public async Task<ActionResult<GetFollowedStudyYearDto>> DeleteFollowedCourseAsync([FromBody] DeleteFollowedStudyYearDto deleteDto)
    {
        try
        {
            var dto = await _yearsService.UnfollowStudyYearAsync(UserId, deleteDto.StudyYearId);
            return Ok(dto);
        }
        catch (NotFollowingStudyYearException)
        {
            return NotFound();
        }
    }
}
