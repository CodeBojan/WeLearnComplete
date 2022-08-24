using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WeLearn.Api.Dtos.CourseMaterialUploadRequest;
using WeLearn.Api.Exceptions.Models;
using WeLearn.Api.Services.CourseMaterialUploadRequest;
using WeLearn.Shared.Dtos.Paging;
using WeLearn.Shared.Exceptions.Models;

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

    [HttpGet("course/{courseId}")]
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

    [HttpPost("course/{courseId}")]
    [Consumes("multipart/form-data")]
    public async Task<ActionResult<GetCourseMaterialUploadRequestDto>> PostCourseMaterialUploadRequestAsync([FromRoute] Guid courseId, [FromForm] PostCourseMaterialUploadRequestDtoWrapper postDtoWrapper)
    {
        try
        {
            var dto = await _uploadRequestService.CreateCourseMaterialUploadRequestDtoAsync(courseId, postDtoWrapper.PostDto, postDtoWrapper.Files, UserId);
            return Ok(dto);
        }
        catch (CourseMaterialUploadRequestValidationException)
        {
            return BadRequest();
        }
        catch (CourseNotFoundException)
        {
            return NotFound();
        }
    }

    [HttpPost("{courseMaterialUploadRequestId}/approve")]
    public async Task<IActionResult>
        PostApproveCourseMaterialUploadRequestAsync([FromRoute] Guid courseMaterialUploadRequestId)
    {
        try
        {
            await _uploadRequestService.ApproveCourseMaterialUploadRequestAsync(courseMaterialUploadRequestId);
            return Ok();
        }
        catch (CourseMaterialUploadRequestNotFoundException)
        {
            return NotFound();
        }
    }
}
