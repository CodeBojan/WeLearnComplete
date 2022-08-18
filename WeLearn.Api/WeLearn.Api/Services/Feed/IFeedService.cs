using WeLearn.Api.Dtos.Feed;
using WeLearn.Shared.Dtos.Paging;

namespace WeLearn.Api.Services.Feed
{
    public interface IFeedService
    {
        Task<PagedResponseDto<GetFeedDto>> GetUserFeedAsync(Guid accountId, PageOptionsDto pageOptions);
    }
}