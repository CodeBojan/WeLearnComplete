using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WeLearn.Api.Dtos.StudyMaterial;
using WeLearn.Api.Services.StudyMaterial;
using WeLearn.Shared.Dtos.Paging;
using WeLearn.Shared.Exceptions.Models;

namespace WeLearn.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StudyMaterialsController : ControllerBase
{
    private readonly IStudyMaterialService _studyMaterialService;

    public StudyMaterialsController(IStudyMaterialService studyMaterialService)
    {
        _studyMaterialService = studyMaterialService;
    }

    [HttpGet("course/{courseId}")]
    public async Task<ActionResult<PagedResponseDto<GetStudyMaterialDto>>> GetCourseStudyMaterialsAsync([FromRoute] Guid courseId, [FromQuery] PageOptionsDto pagingOptions)
    {
        try
        {
            var studyMaterials = await _studyMaterialService.GetCourseStudyMaterialsAsync(courseId, pagingOptions);
            return Ok(studyMaterials);
        }
        catch (CourseNotFoundException)
        {
            return NotFound();
        }
    }
}
