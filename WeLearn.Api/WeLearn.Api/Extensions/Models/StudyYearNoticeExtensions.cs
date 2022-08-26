
using WeLearn.Api.Dtos.Document;
using WeLearn.Api.Dtos.Notice;
using WeLearn.Data.Models.Content.Notices;
using WeLearn.Shared.Extensions.Models;

namespace WeLearn.Api.Extensions.Models;

public static class StudyYearNoticeExtensions
{
    public static GetStudyYearNoticeDto MapToGetDto(this StudyYearNotice syn)
    {
        return new GetStudyYearNoticeDto
        {
            Id = syn.Id,
            CreatedDate = syn.CreatedDate,
            UpdatedDate = syn.UpdatedDate,
            ExternalId = syn.ExternalId,
            ExternalUrl = syn.ExternalUrl,
            Body = syn.Body,
            Title = syn.Title,
            Author = syn.Author,
            IsImported = syn.IsImported,
            CourseId = syn.CourseId,
            Type = syn.Type,
            CreatorId = syn.CreatorId,
            ExternalSystemId = syn.ExternalSystemId,
            ExternalCreatedDate = syn.ExternalCreatedDate,
            Creator = syn.Creator?.MapToGetDto(),
            DocumentCount = syn.DocumentCount,
            Documents = syn.Documents?.Select(d => d.MapToGetDto()).ToArray() ?? Array.Empty<GetDocumentDto>(),
            RelevantUntil = syn.RelevantUntil,
            StudyYearId = syn.StudyYearId,
        };
    }
}
