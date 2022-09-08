using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WeLearn.Auth.Controllers;
using WeLearn.Auth.Policy;
using WeLearn.Data.Models;
using WeLearn.Shared.Dtos.Account;
using WeLearn.Shared.Dtos.Paging;
using WeLearn.Shared.Dtos.StudyYear;
using WeLearn.Shared.Exceptions.Models;
using WeLearn.Shared.Services.StudyYear;

namespace WeLearn.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StudyYearsController : UserAuthorizedController
{
    private readonly IStudyYearsService _studyYearService;

    public StudyYearsController(IStudyYearsService studyYearService)
    {
        _studyYearService = studyYearService;
    }

    [HttpGet]
    public async Task<ActionResult<PagedResponseDto<GetStudyYearDto>>> GetStudyYearsAsync([FromQuery] PageOptionsDto pageOptions, [FromQuery] bool isFollowing)
    {
        var dto = await _studyYearService.GetStudyYearsAsync(pageOptions, UserId, isFollowing);

        return Ok(dto);
    }

    [HttpGet("{studyYearId}")]
    public async Task<ActionResult<GetStudyYearDto>> GetStudyYearAsync([FromRoute] Guid studyYearId)
    {
        try
        {
            var dto = await _studyYearService.GetStudyYearWithFollowingInfoAsync(studyYearId, UserId);

            return Ok(dto);
        }
        catch (StudyYearNotFoundException)
        {
            return NotFound();
        }
    }

    [HttpGet("{studyYearId}/accounts")]
    public async Task<ActionResult<PagedResponseDto<GetAccountDto>>> GetFollowingAccountsAsync([FromRoute] Guid studyYearId, [FromQuery] PageOptionsDto pageOptions, [FromServices] IAuthorizationService authorizationService)
    {
        try
        {
            if (!await _studyYearService.StudyYearExistsAsync(studyYearId))
                return NotFound();

            var authResult = await authorizationService.AuthorizeAsync(User, new StudyYear { Id = studyYearId }, Policies.IsResourceAdmin);
            if (!authResult.Succeeded)
                return Forbid();

            var dto = await _studyYearService.GetFollowingAccountsAsync(studyYearId, pageOptions);
            return Ok(dto);
        }
        catch (StudyYearNotFoundException)
        {
            return NotFound();
        }
    }

    [HttpPut("{studyYearId}")]
    public async Task<ActionResult<GetStudyYearDto>> PutStudyYearAsync([FromRoute] Guid studyYearId, [FromBody] PutStudyYearDto putDto)
    {
        try
        {
            var dto = await _studyYearService.UpdateStudyYearAsync(studyYearId, putDto.ShortName, putDto.FullName, putDto.Description);

            return Ok(dto);
        }
        catch (StudyYearNotFoundException)
        {
            return NotFound();
        }
        catch (StudyYearUpdateException)
        {
            return Conflict();
        }
    }
}
