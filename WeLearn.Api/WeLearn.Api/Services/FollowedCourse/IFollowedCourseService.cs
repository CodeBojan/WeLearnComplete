using WeLearn.Api.Dtos.FollowedCourse;
using WeLearn.Api.Dtos.FollowedStudyYear;

namespace WeLearn.Api.Services.FollowedCourse
{
    public interface IFollowedCourseService
    {
        Task<GetFollowedCourseDto> FollowCourseAsync(Guid accountId, Guid courseId);
    }
}