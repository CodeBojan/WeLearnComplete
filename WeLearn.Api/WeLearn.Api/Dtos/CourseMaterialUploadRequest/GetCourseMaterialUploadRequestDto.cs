using WeLearn.Api.Dtos.Document;
using WeLearn.Shared.Dtos.Account;

namespace WeLearn.Api.Dtos.CourseMaterialUploadRequest;

public class GetCourseMaterialUploadRequestDto
{
    public Guid Id { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }
    public string Body { get; set; }
    public bool IsApproved { get; set; }
    public string Remark { get; set; }
    public string? Type { get; set; }
    public Guid CreatorId { get; set; }
    public Guid CourseId { get; set; }
    public int DocumentCount { get; set; }
    public GetDocumentDto[] Documents { get; set; }
    public GetAccountDto? Creator { get; set; }
    public string Title { get; set; }
}
