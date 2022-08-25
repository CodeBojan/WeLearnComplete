using WeLearn.Api.Dtos.Document;

namespace WeLearn.Api.Extensions.Models;

public static class DocumentExtensions
{
    public static GetDocumentDto MapToGetDto(this WeLearn.Data.Models.Content.Document d)
    {
        return new GetDocumentDto
        {
            Id = d.Id,
            CreatedDate = d.CreatedDate,
            UpdatedDate = d.UpdatedDate,
            ExternalId = d.ExternalId,
            ExternalUrl = d.ExternalUrl,
            Body = d.Body,
            Title = d.Title,
            Author = d.Author,
            IsImported = d.IsImported,
            CourseId = d.CourseId,
            CreatorId = d.CreatorId,
            ExternalSystemId = d.ExternalSystemId,
            ExternalCreatedDate = d.ExternalCreatedDate,
            FileName = d.FileName,
            //Uri = d.Uri, // TODO - adjust
            Version = d.Version,
            Size = d.Size,
            Hash = d.Hash,
            HashAlgorithm = d.HashAlgorithm,
            DocumentContainerId = d.DocumentContainerId,
            CourseMaterialUploadRequestId = d.CourseMaterialUploadRequestId,
            FileExtension = d.FileExtension
        };
    }
}
