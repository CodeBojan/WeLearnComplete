using WeLearn.Api.Dtos.Document;
using WeLearn.Api.Dtos.ExternalSystem;
using WeLearn.Shared.Dtos.Account;
using WeLearn.Shared.Dtos.Course;
using WeLearn.Shared.Dtos.StudyYear;

namespace WeLearn.Api.Dtos.Content;

public class GetContentDto
{
    public Guid Id { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }
    public string? ExternalId { get; set; }
    public string? ExternalUrl { get; set; }
    public string? Body { get; set; }
    public string? Title { get; set; }
    public string? Author { get; set; }
    public bool IsImported { get; set; }
    public Guid? CourseId { get; set; }
    public string Type { get; set; }
    public Guid? CreatorId { get; set; }
    public Guid? ExternalSystemId { get; set; }
    public DateTime? ExternalCreatedDate { get; set; }

    public int? CommentCount { get; set; }

    public int? DocumentCount { get; set; }
    public GetDocumentDto[]? Documents { get; set; }

    public GetAccountDto? Creator { get; set; }
    public GetExternalSystemDto? ExternalSystem { get; set; }
    public GetCourseDto? Course { get; set; }

    public GetStudyYearDto? StudyYear { get; set; }
}
