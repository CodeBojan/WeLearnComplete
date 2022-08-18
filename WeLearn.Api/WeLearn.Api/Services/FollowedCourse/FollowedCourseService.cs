﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;
using WeLearn.Api.Dtos.FollowedCourse;
using WeLearn.Api.Exceptions.Models;
using WeLearn.Api.Extensions.Models;
using WeLearn.Data.Models;
using WeLearn.Data.Persistence;
using WeLearn.Shared.Dtos.Paging;
using WeLearn.Shared.Extensions.Paging;

namespace WeLearn.Api.Services.FollowedCourse
{
    public class FollowedCourseService : IFollowedCourseService
    {
        private readonly ApplicationDbContext _dbContext;

        public FollowedCourseService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<GetFollowedCourseDto> FollowCourseAsync(Guid accountId, Guid courseId)
        {
            var existingFollowedCourse = await GetUntrackedQueryable()
                .Select(MapFollowedCourseToGetDto())
                .FirstOrDefaultAsync(fc => fc.AccountId == accountId && fc.CourseId == courseId);

            // TODO throw exception
            if (existingFollowedCourse is not null)
                return existingFollowedCourse;

            var followedCourse = new WeLearn.Data.Models.FollowedCourse(accountId, courseId);

            _dbContext.Add(followedCourse);
            await _dbContext.SaveChangesAsync();

            return followedCourse.MapToGetDto();
        }

        public async Task<GetFollowedCourseDto> UnfollowCourseAsync(Guid accountId, Guid courseId)
        {
            var existingFollowedCourse = await _dbContext.FollowedCourses
                .FirstOrDefaultAsync(fc => fc.AccountId == accountId && fc.CourseId == courseId);

            if (existingFollowedCourse is null)
                throw new NotFollowingCourseException();

            _dbContext.Remove(existingFollowedCourse);
            await _dbContext.SaveChangesAsync();

            return existingFollowedCourse.MapToGetDto();
        }

        public async Task<PagedResponseDto<GetFollowedCourseDto>> GetUserFollowedCoursesAsync(Guid accountId, PageOptionsDto pageOptions)
        {
            var dto = await GetUntrackedQueryable()
                .Where(fc => fc.AccountId == accountId)
                .OrderByDescending(fc => fc.UpdatedDate)
                .GetPagedResponseDtoAsync(pageOptions, MapFollowedCourseToGetDto());

            return dto;
        }

        private IIncludableQueryable<WeLearn.Data.Models.FollowedCourse, Course> GetUntrackedQueryable()
        {
            return _dbContext.FollowedCourses
                            .AsNoTracking()
                            .Include(fc => fc.Course);
        }

        private static Expression<Func<WeLearn.Data.Models.FollowedCourse, GetFollowedCourseDto>> MapFollowedCourseToGetDto()
        {
            return fc => fc.MapToGetDto();
        }
    }
}
