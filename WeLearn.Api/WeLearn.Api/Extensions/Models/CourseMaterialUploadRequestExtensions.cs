using WeLearn.Api.Dtos.CourseMaterialUploadRequest;
using WeLearn.Api.Dtos.Document;
using WeLearn.Data.Models;

namespace WeLearn.Api.Extensions.Models;

public static class CourseMaterialUploadRequestExtensions
{
    public static GetCourseMaterialUploadRequestDto MapToGetDto(this CourseMaterialUploadRequest r)
    {
        return new GetCourseMaterialUploadRequestDto
        {
            Id = r.Id,
            Body = r.Body,
            IsApproved = r.IsApproved,
            Remark = r.Remark,
            Type = r.Type,
            CreatorId = r.CreatorId,
            CourseId = r.CourseId,
            Documents = r.Documents?.Select(d => d.MapToGetDto()).ToArray() ?? Array.Empty<GetDocumentDto>(),
            CreatedDate = r.CreatedDate,
            UpdatedDate = r.UpdatedDate
        };
    }
}
