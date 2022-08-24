using WeLearn.Api.Dtos.Document;
using WeLearn.Api.Dtos.StudyMaterial;
using WeLearn.Data.Models.Content;

namespace WeLearn.Api.Extensions.Models;

public static class StudyMaterialExtensions
{
    public static GetStudyMaterialDto MapToGetDto(this StudyMaterial sm)
    {
        return new GetStudyMaterialDto
        {
            Id = sm.Id,
            CreatedDate = sm.CreatedDate,
            UpdatedDate = sm.UpdatedDate,
            ExternalId = sm.ExternalId,
            ExternalUrl = sm.ExternalUrl,
            Body = sm.Body,
            Title = sm.Title,
            Author = sm.Author,
            IsImported = sm.IsImported,
            CourseId = sm.CourseId,
            CreatorId = sm.CreatorId,
            ExternalSystemId = sm.ExternalSystemId,
            ExternalCreatedDate = sm.ExternalCreatedDate,
            DocumentCount = sm.Documents.Count,
            Documents = sm.Documents?.Select(d => d.MapToGetDto()).ToArray() ?? Array.Empty<GetDocumentDto>()
    };
}
}
