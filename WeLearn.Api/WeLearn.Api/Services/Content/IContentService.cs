using WeLearn.Api.Dtos.Content;
using WeLearn.Shared.Dtos.Paging;

namespace WeLearn.Api.Services.Content
{
    public interface IContentService
    {
        Task<PagedResponseDto<GetContentDto>> GetCourseContentAsync(Guid courseId, PageOptionsDto pageOptions);
    }
}