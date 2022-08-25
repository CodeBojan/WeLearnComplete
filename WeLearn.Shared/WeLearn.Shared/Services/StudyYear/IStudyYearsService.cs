using WeLearn.Shared.Dtos.Account;
using WeLearn.Shared.Dtos.Paging;
using WeLearn.Shared.Dtos.StudyYear;

namespace WeLearn.Shared.Services.StudyYear
{
    public interface IStudyYearsService
    {
        Task<GetStudyYearDto> CreateStudyYearAsync(string shortName, string fullName, string description);
        Task<GetStudyYearDto> GetStudyYearAsync(Guid studyYearId);
        Task<PagedResponseDto<GetStudyYearDto>> GetStudyYearsAsync(PageOptionsDto pageOptions);
        Task<GetStudyYearDto> UpdateStudyYearAsync(Guid studyYearId, string shortName, string fullName, string description);
        Task<PagedResponseDto<GetAccountDto>> GetFollowingAccountsAsync(Guid studyYearId, PageOptionsDto pageOptions);
        Task<PagedResponseDto<GetStudyYearDto>> GetStudyYearsAsync(PageOptionsDto pageOptions, Guid accountId, bool isFollowing);
        Task<bool> StudyYearExistsAsync(Guid studyYearId);
    }
}