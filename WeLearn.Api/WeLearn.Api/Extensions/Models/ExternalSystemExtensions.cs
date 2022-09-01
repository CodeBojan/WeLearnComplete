using WeLearn.Api.Dtos.ExternalSystem;
using WeLearn.Data.Models;

namespace WeLearn.Api.Extensions.Models;

public static class ExternalSystemExtensions
{
    public static GetExternalSystemDto MapToGetDto(this ExternalSystem es)
    {
        return new GetExternalSystemDto
        {
            Id = es.Id,
            Name = es.Name,
            Url = es.Url,
            CreatedDate = es.CreatedDate,
            UpdatedDate = es.UpdatedDate,
            FriendlyName = es.FriendlyName,
            LogoUrl = es.LogoUrl
        };
    }
}
