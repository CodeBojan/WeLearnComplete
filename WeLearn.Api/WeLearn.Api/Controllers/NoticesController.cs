using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WeLearn.Api.Dtos.Notice;
using WeLearn.Api.Services.Notice;
using WeLearn.Shared.Dtos.Paging;
using WeLearn.Shared.Exceptions.Models;

namespace WeLearn.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class NoticesController : UserAuthorizedController
{
    private readonly INoticeService _noticeService;

    public NoticesController(INoticeService noticeService)
    {
        _noticeService = noticeService;
    }

    [HttpGet("studyYear/{studyYearId}")]
    public async Task<ActionResult<PagedResponseDto<GetStudyYearNoticeDto>>> GetStudyYearNoticesAsync([FromRoute] Guid studyYearId, [FromQuery] PageOptionsDto pageOptions)
    {
        try
        {
            var dtos = await _noticeService.GetStudyYearNoticesAsync(studyYearId, pageOptions);
            return Ok(dtos);
        }
        catch (StudyYearNotFoundException)
        {
            return NotFound();
        }
    }
}
