using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WeLearn.Api.Dtos.Content;
using WeLearn.Api.Services.Content;
using WeLearn.Auth.Controllers;
using WeLearn.Shared.Dtos.Paging;
using WeLearn.Shared.Exceptions.Models;

namespace WeLearn.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ContentController : UserAuthorizedController
{
    private readonly IContentService _contentService;

    public ContentController(IContentService contentService)
    {
        _contentService = contentService;
    }

    [HttpGet("course/{courseId}")]
    public async Task<ActionResult<PagedResponseDto<GetContentDto>>> GetCourseContentAsync([FromRoute] Guid courseId, [FromQuery] PageOptionsDto pageOptions)
    {
        try
        {
            var dtos = await _contentService.GetCourseContentAsync(courseId, pageOptions);
            return Ok(dtos);
        }
        catch (CourseNotFoundException)
        {
            return NotFound();
        }
    }
}
