using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WeLearn.Api.Dtos.Content;
using WeLearn.Api.Dtos.Feed;
using WeLearn.Api.Extensions.Models;
using WeLearn.Data.Models.Content;
using WeLearn.Data.Persistence;
using WeLearn.Shared.Dtos.Paging;
using WeLearn.Shared.Extensions.Paging;

namespace WeLearn.Api.Services.Feed;

public class FeedService : IFeedService
{
    private readonly ApplicationDbContext _dbContext;

    public FeedService(ApplicationDbContext context)
    {
        _dbContext = context;
    }

    public async Task<PagedResponseDto<GetContentDto>> GetUserFeedAsync(Guid accountId, PageOptionsDto pageOptions)
    {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
        var dtos = await _dbContext.Contents
            .AsNoTracking()

            .Include(c => c.Course)
                .ThenInclude(c => c.FollowingUsers)
            .Include(c => (c as WeLearn.Data.Models.Content.Notices.StudyYearNotice).StudyYear)
                .ThenInclude(sy => sy.FollowedStudyYears)
            .Include(c => c.Creator)
                .ThenInclude(a => a.User)
            .Include(c => c.ExternalSystem)
            .Include(c => (c as DocumentContainer).Documents)
                .ThenInclude(d => d.Creator)
                    .ThenInclude(a => a.User)
            .Include(c => c.Comments)
                .ThenInclude(c => c.Author)
                    .ThenInclude(a => a.User)

            .Where(c => !(c is WeLearn.Data.Models.Content.Document))
            .Where(c => c.Course.FollowingUsers.Any(fu => fu.AccountId == accountId)
                        || (c as WeLearn.Data.Models.Content.Notices.StudyYearNotice).StudyYear.FollowedStudyYears.Any(fu => fu.AccountId == accountId)
                        || (c is WeLearn.Data.Models.Content.Notices.GeneralNotice)
                        || (c is WeLearn.Data.Models.Content.Post))
            .OrderByDescending(c => c.UpdatedDate)
                .ThenByDescending(c => c.ExternalCreatedDate)
            .Select(c => c.WithCommentCount())
            .Select(c => c.WithPossibleDocumentCount())
            .GetPagedResponseDtoAsync(pageOptions, MapContentToGetDto());
#pragma warning restore CS8602 // Dereference of a possibly null reference.

        return dtos;
    }

    private static Expression<Func<WeLearn.Data.Models.Content.Content, GetContentDto>> MapContentToGetDto()
    {
        return c => c.MapToGetDto();
    }
}
