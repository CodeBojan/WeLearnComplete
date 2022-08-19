using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WeLearn.Api.Dtos.CourseMaterialUploadRequest;
using WeLearn.Api.Exceptions.Models;
using WeLearn.Api.Services.CourseMaterialUploadRequest;
using WeLearn.Shared.Dtos.Paging;

namespace WeLearn.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CourseMaterialUploadRequestsController : UserAuthorizedController
{
    private readonly ICourseMaterialUploadRequestService _uploadRequestService;

    public CourseMaterialUploadRequestsController(ICourseMaterialUploadRequestService uploadRequestService)
    {
        _uploadRequestService = uploadRequestService;
    }

    [HttpGet("{courseMaterialUploadRequestId}")]
    public async Task<ActionResult<GetCourseMaterialUploadRequestDto>> GetCourseMaterialUploadRequestAsync([FromRoute] Guid courseMaterialUploadRequestId)
    {
        try
        {
            var dto = await _uploadRequestService.GetCourseMaterialUploadRequestAsync(courseMaterialUploadRequestId);
            return Ok(dto);
        }
        catch (CourseMaterialUploadRequestNotFoundException)
        {
            return NotFound();
        }
    }

    [HttpGet("{courseId}")]
    public async Task<ActionResult<PagedResponseDto<GetCourseMaterialUploadRequestDto>>> GetCourseMaterialUploadRequestsAsync([FromRoute] Guid courseId, [FromQuery] PageOptionsDto pageOptions)
    {
        try
        {
            var dtos = await _uploadRequestService.GetCourseMaterialUploadRequestsAsync(courseId, pageOptions);
            return Ok(dtos);
        }
        catch (CourseNotFoundException)
        {
            return NotFound();
        }
    }
}
