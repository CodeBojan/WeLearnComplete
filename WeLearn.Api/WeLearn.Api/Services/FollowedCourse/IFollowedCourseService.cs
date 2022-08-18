using WeLearn.Api.Dtos.FollowedCourse;
using WeLearn.Api.Dtos.FollowedStudyYear;
using WeLearn.Shared.Dtos.Paging;

namespace WeLearn.Api.Services.FollowedCourse;

public interface IFollowedCourseService
{
    Task<GetFollowedCourseDto> FollowCourseAsync(Guid accountId, Guid courseId);
    Task<PagedResponseDto<GetFollowedCourseDto>> GetUserFollowedCoursesAsync(Guid accountId, PageOptionsDto pageOptions);
    Task<GetFollowedCourseDto> UnfollowCourseAsync(Guid accountId, Guid courseId);
}