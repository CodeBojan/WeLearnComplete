using WeLearn.Api.Dtos.Content;
using WeLearn.Api.Dtos.Feed;
using WeLearn.Shared.Dtos.Paging;

namespace WeLearn.Api.Services.Feed
{
    public interface IFeedService
    {
        Task<PagedResponseDto<GetContentDto>> GetUserFeedAsync(Guid accountId, PageOptionsDto pageOptions);
    }
}