using Swashbuckle.AspNetCore.JsonMultipartFormDataSupport.Attributes;

namespace WeLearn.Api.Dtos.CourseMaterialUploadRequest;

public class PostCourseMaterialUploadRequestDtoWrapper
{
    [FromJson]
    public PostCourseMaterialUploadRequestDto PostDto { get; set; }
    public IEnumerable<IFormFile> Files { get; set; }
}
