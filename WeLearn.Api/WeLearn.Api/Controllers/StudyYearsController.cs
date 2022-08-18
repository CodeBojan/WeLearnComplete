using Microsoft.AspNetCore.Mvc;
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
    public async Task<ActionResult<PagedResponseDto<GetStudyYearDto>>> GetStudyYearsAsync([FromQuery] PageOptionsDto pageOptions)
    {
        var dto = await _studyYearService.GetStudyYearsAsync(pageOptions);

        return Ok(dto);
    }

    [HttpGet("{studyYearId}")]
    public async Task<ActionResult<GetStudyYearDto>> GetStudyYearAsync([FromRoute] Guid studyYearId)
    {
        try
        {
            // TODO use separate method for including courses and content related to a study year
            var dto = await _studyYearService.GetStudyYearAsync(studyYearId);

            return Ok(dto);
        }
        catch (StudyYearNotFoundException)
        {
            return NotFound();
        }
    }

    [HttpGet("{studyYearId}/accounts")]
    public async Task<ActionResult<PagedResponseDto<GetAccountDto>>> GetFollowingAccountsAsync([FromRoute] Guid studyYearId, [FromQuery] PageOptionsDto pageOptions)
    {
        try
        {
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
