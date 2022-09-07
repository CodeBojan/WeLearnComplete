using WeLearn.Shared.Dtos.Account;
using WeLearn.Shared.Dtos.Course;
using WeLearn.Shared.Dtos.Paging;

namespace WeLearn.Shared.Services.Course
{
    public interface ICourseService
    {
        Task<bool> CourseExistsAsync(Guid courseId);
        Task<GetCourseDto> CreateCourseAsync(string code, string shortName, string fullName, string staff, string description, string rules, Guid studyYearId);
        Task<GetCourseDto?> GetBasicCourseInfo(Guid courseId);
        Task<GetCourseDto> GetCourseAsync(Guid courseId, Guid userId);
        Task<GetCourseDto> GetCourseBasicInfoAsync(Guid courseId);
        Task<PagedResponseDto<GetCourseDto>> GetCoursesAsync(PageOptionsDto pageOptions);
        Task<PagedResponseDto<GetCourseDto>> GetCoursesAsync(PageOptionsDto pageOptions, Guid accountId, Guid? studyYearId, bool isFollowing);
        Task<PagedResponseDto<GetAccountDto>> GetFollowingAccountsAsync(Guid courseId, PageOptionsDto pageOptions);
        Task<GetCourseDto> UpdateCourseAsync(Guid courseId, string code, string shortName, string fullName, string staff, string description, string rules);
    }
}