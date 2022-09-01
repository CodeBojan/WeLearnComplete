using WeLearn.Api.Dtos.Content;
using WeLearn.Api.Dtos.Document;
using WeLearn.Data.Models.Content;
using WeLearn.Shared.Extensions.Models;

namespace WeLearn.Api.Extensions.Models;

public static class ContentExtensions
{
    public static GetContentDto MapToGetDto(this Content c)
    {
        return new GetContentDto
        {
            Id = c.Id,
            CreatedDate = c.CreatedDate,
            UpdatedDate = c.UpdatedDate,
            ExternalId = c.ExternalId, // TODO unnecessary
            ExternalUrl = c.ExternalUrl,
            Body = c.Body,
            Title = c.Title,
            Author = c.Author,
            IsImported = c.IsImported,
            CourseId = c.CourseId,
            Type = c.Type,
            CreatorId = c.CreatorId,
            ExternalSystemId = c.ExternalSystemId,
            ExternalCreatedDate = c.ExternalCreatedDate,
            CommentCount = c.CommentCount,
            Creator = c.Creator?.MapToGetDto(),
            Course = c.Course?.MapToGetDto(),
            ExternalSystem = c.ExternalSystem?.MapToGetDto()
        };
    }

    public static TContent WithCommentCount<TContent>(this TContent c) where TContent : Content
    {
        c.CommentCount = c.Comments?.Count ?? 0;
        return c;
    }

    public static TDocumentContainer WithDocumentCount<TDocumentContainer>(this TDocumentContainer dc) where TDocumentContainer : DocumentContainer
    {
        dc.DocumentCount = dc.Documents?.Count ?? 0;
        return dc;
    }
}
