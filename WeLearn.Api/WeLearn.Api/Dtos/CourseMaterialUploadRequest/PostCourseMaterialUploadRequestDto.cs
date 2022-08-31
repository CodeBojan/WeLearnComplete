using WeLearn.Api.Dtos.Document;

namespace WeLearn.Api.Dtos.CourseMaterialUploadRequest;

public class PostCourseMaterialUploadRequestDto
{
    public string Title { get; set; }
    public string Body { get; set; }
    public string Remark { get; set; }
    public Guid CourseId { get; set; }
    public PostDocumentDto[] Documents { get; set; } = Array.Empty<PostDocumentDto>();
}
