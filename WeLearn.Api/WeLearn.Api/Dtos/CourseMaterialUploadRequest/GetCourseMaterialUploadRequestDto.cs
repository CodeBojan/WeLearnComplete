using WeLearn.Api.Dtos.Document;

namespace WeLearn.Api.Dtos.CourseMaterialUploadRequest;

public class GetCourseMaterialUploadRequestDto
{
    public Guid Id { get; set; }
    public string Body { get; set; }
    public bool IsApproved { get; set; }
    public string Remark { get; set; }
    public string? Type { get; set; }
    public Guid CreatorId { get; set; }
    public Guid CourseId { get; set; }
    public GetDocumentDto[] Documents { get; set; }
}
