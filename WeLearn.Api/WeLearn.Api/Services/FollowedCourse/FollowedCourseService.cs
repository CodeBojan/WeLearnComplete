using Microsoft.EntityFrameworkCore;
using WeLearn.Api.Dtos.FollowedCourse;
using WeLearn.Data.Persistence;

namespace WeLearn.Api.Services.FollowedCourse
{
    public class FollowedCourseService : IFollowedCourseService
    {
        private readonly ApplicationDbContext dbContext;

        public FollowedCourseService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<GetFollowedCourseDto> FollowCourseAsync(Guid accountId, Guid courseId)
        {
            var existingFollowedCourse = await dbContext.FollowedCourses
                .AsNoTracking()
                .FirstOrDefaultAsync(fc => fc.AccountId == accountId && fc.CourseId == courseId);

            if (existingFollowedCourse is not null)
                return new GetFollowedCourseDto
                {
                    AccountId = accountId,
                    CourseId = courseId
                };

            var followedCourse = new WeLearn.Data.Models.FollowedCourse(accountId, courseId);

            dbContext.Add(followedCourse);
            await dbContext.SaveChangesAsync();
            
            return new GetFollowedCourseDto
            {
                AccountId = accountId,
                CourseId = courseId
            };
        }
    }
}
