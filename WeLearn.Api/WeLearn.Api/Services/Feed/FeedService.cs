using WeLearn.Api.Dtos.Feed;
using WeLearn.Data.Persistence;
using WeLearn.Shared.Dtos.Paging;

namespace WeLearn.Api.Services.Feed;

public class FeedService : IFeedService
{
    private readonly ApplicationDbContext _context;

    public FeedService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<PagedResponseDto<GetFeedDto>> GetUserFeedAsync(Guid accountId, PageOptionsDto pageOptions)
    {
        throw new NotImplementedException();
    }
}
