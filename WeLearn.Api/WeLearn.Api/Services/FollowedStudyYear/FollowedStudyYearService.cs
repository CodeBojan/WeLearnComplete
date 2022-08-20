using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;
using WeLearn.Api.Dtos.FollowedStudyYear;
using WeLearn.Api.Exceptions.Models;
using WeLearn.Api.Extensions.Models;
using WeLearn.Data.Models;
using WeLearn.Data.Persistence;
using WeLearn.Shared.Dtos.Paging;
using WeLearn.Shared.Extensions.Paging;

namespace WeLearn.Api.Services.FollowedStudyYear;

public class FollowedStudyYearService : IFollowedStudyYearService
{
    private readonly ApplicationDbContext _dbContext;

    public FollowedStudyYearService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<GetFollowedStudyYearDto> FollowStudyYearAsync(Guid accountId, Guid studyYearId)
    {
        var existingFollowedStudyYear = await GetUntrackedQueryable()
            .Where(fc => fc.AccountId == accountId && fc.StudyYearId == studyYearId)
            .Select(MapFollowedStudyYearToGetDto())
            .FirstOrDefaultAsync();

        if (existingFollowedStudyYear is not null)
            return existingFollowedStudyYear;

        var followedStudyYear = new WeLearn.Data.Models.FollowedStudyYear(accountId, studyYearId);

        _dbContext.Add(followedStudyYear);
        await _dbContext.SaveChangesAsync();

        return followedStudyYear.MapToGetDto();
    }

    public async Task<GetFollowedStudyYearDto> UnfollowStudyYearAsync(Guid accountId, Guid studyYearId)
    {
        var existingFollowedStudyYear = await GetUntrackedQueryable()
            .Where(fc => fc.AccountId == accountId && fc.StudyYearId == studyYearId)
            .FirstOrDefaultAsync();

        if (existingFollowedStudyYear is null)
            throw new NotFollowingStudyYearException();

        _dbContext.Remove(existingFollowedStudyYear);
        await _dbContext.SaveChangesAsync();

        return existingFollowedStudyYear.MapToGetDto();
    }

    public async Task<PagedResponseDto<GetFollowedStudyYearDto>> GetUserFollowedStudyYearsAsync(Guid accountId, PageOptionsDto pageOptions)
    {
        var dto = await GetUntrackedQueryable()
            .Where(fc => fc.AccountId == accountId)
            .OrderByDescending(fc => fc.UpdatedDate)
            .GetPagedResponseDtoAsync(pageOptions, MapFollowedStudyYearToGetDto());

        return dto;
    }

    private static Expression<Func<WeLearn.Data.Models.FollowedStudyYear, GetFollowedStudyYearDto>> MapFollowedStudyYearToGetDto()
    {
        return fsy => fsy.MapToGetDto();
    }

    private IIncludableQueryable<WeLearn.Data.Models.FollowedStudyYear, StudyYear> GetUntrackedQueryable()
    {
        return _dbContext.FollowedStudyYears
                    .AsNoTracking()
                    .Include(fsy => fsy.StudyYear);
    }
}
