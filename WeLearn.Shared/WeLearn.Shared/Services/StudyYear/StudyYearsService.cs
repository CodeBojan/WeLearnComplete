using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WeLearn.Data.Models;
using WeLearn.Data.Models.Roles;
using WeLearn.Data.Persistence;
using WeLearn.Shared.Dtos.Account;
using WeLearn.Shared.Dtos.Paging;
using WeLearn.Shared.Dtos.StudyYear;
using WeLearn.Shared.Exceptions.Models;
using WeLearn.Shared.Extensions.Models;
using WeLearn.Shared.Extensions.Paging;

namespace WeLearn.Shared.Services.StudyYear;

public class StudyYearsService : IStudyYearsService
{
    private readonly ApplicationDbContext _dbContext;
    private readonly ILogger _logger;

    public StudyYearsService(ApplicationDbContext dbContext, ILogger<StudyYearsService> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<bool> StudyYearExistsAsync(Guid studyYearId)
    {
        return await _dbContext.StudyYears.AnyAsync(x => x.Id == studyYearId);
    }

    public async Task<GetStudyYearDto> GetStudyYearAsync(Guid studyYearId)
    {
        var dto = await _dbContext.StudyYears
            .Where(sy => sy.Id == studyYearId)
            .Select(MapStudyYearToGetDto())
            .FirstOrDefaultAsync();

        if (dto is null)
            throw new StudyYearNotFoundException();

        return dto;
    }

    public async Task<GetStudyYearDto> GetStudyYearWithFollowingInfoAsync(Guid studyYearId, Guid accountId)
    {
        var dto = await GetStudyYearsNoTrackingWithFollowingInfo()
            .Where(sy => sy.Id == studyYearId)
            .Select(SelectStudyYearWithFollowingInfo(accountId))
            .Select(MapStudyYearToGetDto())
            .FirstOrDefaultAsync();

        if (dto is null)
            throw new StudyYearNotFoundException();

        return dto;
    }

    public async Task<GetStudyYearDto> UpdateStudyYearAsync(Guid studyYearId, string shortName, string fullName, string description)
    {
        var existingStudyYear = await _dbContext.StudyYears.FirstOrDefaultAsync(sy => sy.Id == studyYearId);
        if (existingStudyYear is null)
            throw new StudyYearNotFoundException();

        existingStudyYear.ShortName = shortName;
        existingStudyYear.FullName = fullName;

        try
        {
            await _dbContext.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            // TODO catch better exception
            _logger.LogError(ex, "Error updating Study Year {@StudyYearId}", studyYearId);
            throw new StudyYearUpdateException(null, ex);
        }

        return existingStudyYear.MapToGetDto();
    }

    public async Task<GetStudyYearDto> CreateStudyYearAsync(string shortName, string fullName, string description)
    {
        var existingStudyYear = await GetStudyYearByNamesAsync(shortName, fullName);
        if (existingStudyYear is not null)
            throw new StudyYearAlreadyExistsException();

        existingStudyYear = new Data.Models.StudyYear(shortName, fullName, description);
        _dbContext.Add(existingStudyYear);

        await _dbContext.SaveChangesAsync();

        return existingStudyYear.MapToGetDto();
    }

    private Task<Data.Models.StudyYear?> GetStudyYearByNamesAsync(string shortName, string fullName)
    {
        // TODO unique index
        return _dbContext.StudyYears.FirstOrDefaultAsync(sy => sy.ShortName == shortName || sy.FullName == fullName);
    }

    // TODO move to different service
    public async Task<PagedResponseDto<GetAccountDto>> GetFollowingAccountsAsync(Guid studyYearId, PageOptionsDto pageOptions)
    {
        var studyYear = await _dbContext.StudyYears
            .AsNoTracking()
            .FirstOrDefaultAsync(sy => sy.Id == studyYearId);
        if (studyYear is null)
            throw new StudyYearNotFoundException();

        var dto = await _dbContext.FollowedStudyYears
            .AsNoTracking()
            .Include(fsy => fsy.Account)
                .ThenInclude(a => a.Roles)
                    .ThenInclude(r => (r as StudyYearAdminRole).StudyYear)
            .Include(fsy => fsy.Account)
                .ThenInclude(a => a.User)
            .Where(fsy => fsy.StudyYearId == studyYearId)
            .OrderBy(fsy => fsy.StudyYear.ShortName)
                .ThenBy(fsy => fsy.StudyYear.FullName) // TODO check ordering
            .Select(fsy => fsy.Account)
            .GetPagedResponseDtoAsync(pageOptions, AccountExtensions.MapAccountToGetDto());

        return dto;
    }

    public async Task<PagedResponseDto<GetStudyYearDto>> GetStudyYearsAsync(PageOptionsDto pageOptions)
    {
        var dto = await _dbContext.StudyYears
            .AsNoTracking()
            .OrderBy(sy => sy.ShortName)
                .ThenBy(sy => sy.FullName)
            .GetPagedResponseDtoAsync(pageOptions, MapStudyYearToGetDto());

        return dto;
    }

    public async Task<PagedResponseDto<GetStudyYearDto>> GetStudyYearsAsync(PageOptionsDto pageOptions, Guid accountId, bool isFollowing)
    {
        IQueryable<WeLearn.Data.Models.StudyYear> queryable =
            GetStudyYearsNoTrackingWithFollowingInfo()
            .OrderBy(sy => sy.ShortName);

        if (isFollowing)
            queryable = queryable.Where(sy => sy.FollowedStudyYears.Any(fsy => fsy.AccountId == accountId));

        var dto = await queryable
            .Select(SelectStudyYearWithFollowingInfo(accountId))
            .GetPagedResponseDtoAsync(pageOptions, MapStudyYearToGetDto());

        return dto;
    }

    private IIncludableQueryable<Data.Models.StudyYear, ICollection<FollowedStudyYear>> GetStudyYearsNoTrackingWithFollowingInfo()
    {
        return _dbContext.StudyYears
                    .AsNoTracking()
                    .Include(sy => sy.FollowedStudyYears);
    }

    private static Expression<Func<Data.Models.StudyYear, Data.Models.StudyYear>> SelectStudyYearWithFollowingInfo(Guid accountId)
    {
        return sy => new Data.Models.StudyYear
        {
            Id = sy.Id,
            ShortName = sy.ShortName,
            FullName = sy.FullName,
            Description = sy.Description,
            IsFollowing = sy.FollowedStudyYears.Any(fsy => fsy.AccountId == accountId),
            FollowingCount = sy.FollowedStudyYears.Count,
            CreatedDate = sy.CreatedDate,
            UpdatedDate = sy.UpdatedDate
        };
    }

    private static Expression<Func<Data.Models.StudyYear, GetStudyYearDto>> MapStudyYearToGetDto()
    {
        return sy => sy.MapToGetDto();
    }
}
