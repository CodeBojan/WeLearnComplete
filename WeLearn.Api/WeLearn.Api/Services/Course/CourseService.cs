using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WeLearn.Api.Dtos.Course;
using WeLearn.Api.Exceptions.Models;
using WeLearn.Api.Extensions.Models;
using WeLearn.Data.Persistence;
using WeLearn.Shared.Dtos.Account;
using WeLearn.Shared.Dtos.Paging;
using WeLearn.Shared.Extensions.Models;
using WeLearn.Shared.Extensions.Paging;

namespace WeLearn.Api.Services.Course;

public class CourseService : ICourseService
{
    private readonly ApplicationDbContext _dbContext;

    public CourseService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<PagedResponseDto<GetCourseDto>> GetCoursesAsync(PageOptionsDto pageOptions)
    {
        var dto = await _dbContext.Courses
            .AsNoTracking()
            .OrderByDescending(c => c.UpdatedDate)
            .GetPagedResponseDtoAsync(pageOptions, MapCourseToGetDto());

        return dto;
    }

    public async Task<PagedResponseDto<GetCourseDto>> GetCoursesAsync(PageOptionsDto pageOptions, Guid accountId, bool isFollowing)
    {
        IQueryable<WeLearn.Data.Models.Course> queryable = _dbContext.Courses
            .AsNoTracking()
            .Include(c => c.FollowingUsers)
            .OrderByDescending(c => c.UpdatedDate);

        if (isFollowing)
            queryable = queryable.Where(c => c.FollowingUsers.Any(fc => fc.AccountId == accountId));

        var dto = await queryable
            .Select(c => new WeLearn.Data.Models.Course
            {
                Id = c.Id,
                Code = c.Code,
                ShortName = c.ShortName,
                FullName = c.FullName,
                Description = c.Description,
                UpdatedDate = c.UpdatedDate,
                CreatedDate = c.CreatedDate,
                IsFollowing = c.FollowingUsers.Any(fu => fu.AccountId == accountId),
                FollowingCount = c.FollowingUsers.Count
            })
            .GetPagedResponseDtoAsync(pageOptions, MapCourseToGetDto());

        return dto;
    }

    public async Task<GetCourseDto> GetCourseAsync(Guid courseId)
    {
        // TODO maybe use different dto - coursedetails
        // TODO retrieve course content, ...
        var dto = await _dbContext.Courses
            .AsNoTracking()
            .Where(c => c.Id == courseId)
            .Select(MapCourseToGetDto())
            .FirstOrDefaultAsync();

        if (dto is null)
            throw new CourseNotFoundException();

        return dto;
    }

    public async Task<PagedResponseDto<GetAccountDto>> GetFollowingAccountsAsync(Guid courseId, PageOptionsDto pageOptions)
    {
        var course = await _dbContext.Courses
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == courseId);
        if (course is null)
            throw new CourseNotFoundException();

        var dto = await _dbContext.FollowedCourses
            .AsNoTracking()
            .Include(fc => fc.Account)
                .ThenInclude(a => a.User)
            .Where(fc => fc.CourseId == courseId)
            .Select(fc => fc.Account)
            .GetPagedResponseDtoAsync(pageOptions, AccountExtensions.MapAccountToGetDto());

        return dto;
    }

    public async Task<GetCourseDto> UpdateCourseAsync(
        Guid courseId,
        string code,
        string shortName,
        string fullName,
        string staff,
        string description,
        string rules)
    {
        var course = await _dbContext.Courses
            .FirstOrDefaultAsync(c => c.Id == courseId);
        if (course is null)
            throw new CourseNotFoundException();

        course.Code = code;
        course.ShortName = shortName;
        course.FullName = fullName;
        course.Staff = staff;
        course.Description = description;
        course.Rules = rules;

        // TODO handle exceptions better
        try
        {
            await _dbContext.SaveChangesAsync();

            return course.MapToGetDto();
        }
        catch (Exception ex)
        {
            throw new CourseUpdateException(null, ex);
        }
    }

    public async Task<GetCourseDto> CreateCourseAsync(
        string code,
        string shortName,
        string fullName,
        string staff,
        string description,
        string rules,
        Guid studyYearId)
    {
        var course = new WeLearn.Data.Models.Course(code, shortName, fullName, staff, description, rules, studyYearId);

        _dbContext.Add(course);
        // TODO better exception handling
        try
        {
            await _dbContext.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new CourseCreationException(null, ex);
        }

        return course.MapToGetDto();
    }

    private static Expression<Func<WeLearn.Data.Models.Course, GetCourseDto>> MapCourseToGetDto()
    {
        return c => c.MapToGetDto();
    }
}
