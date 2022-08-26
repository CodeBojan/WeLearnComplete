using WeLearn.Api.Dtos.Document;
using WeLearn.Shared.Dtos.Account;

namespace WeLearn.Api.Dtos.Notice;

public class GetStudyYearNoticeDto
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

    public GetAccountDto? Creator { get; set; }

    public int DocumentCount { get; set; }
    public GetDocumentDto[] Documents { get; set; }

    public DateTime? RelevantUntil { get; set; }

    public Guid StudyYearId { get; set; }
}
