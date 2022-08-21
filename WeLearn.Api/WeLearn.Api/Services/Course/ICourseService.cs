using WeLearn.Api.Dtos.Course;
using WeLearn.Shared.Dtos.Account;
using WeLearn.Shared.Dtos.Paging;

namespace WeLearn.Api.Services.Course
{
    public interface ICourseService
    {
        Task<GetCourseDto> CreateCourseAsync(string code, string shortName, string fullName, string staff, string description, string rules, Guid studyYearId);
        Task<GetCourseDto> GetCourseAsync(Guid courseId, Guid userId);
        Task<PagedResponseDto<GetCourseDto>> GetCoursesAsync(PageOptionsDto pageOptions);
        Task<PagedResponseDto<GetCourseDto>> GetCoursesAsync(PageOptionsDto pageOptions, Guid accountId, Guid? studyYearId, bool isFollowing);
        Task<PagedResponseDto<GetAccountDto>> GetFollowingAccountsAsync(Guid courseId, PageOptionsDto pageOptions);
        Task<GetCourseDto> UpdateCourseAsync(Guid courseId, string code, string shortName, string fullName, string staff, string description, string rules);
    }
}