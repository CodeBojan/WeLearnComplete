using WeLearn.Shared.Dtos.Paging;
using WeLearn.Shared.Dtos.StudyYear;

namespace WeLearn.Shared.Services.StudyYear
{
    public interface IStudyYearsService
    {
        Task<GetStudyYearDto> CreateStudyYearAsync(string shortName, string fullName, string description);
        Task<GetStudyYearDto> GetStudyYearAsync(Guid studyYearId);
        Task<PagedResponseDto<GetStudyYearDto>> GetStudyYearsAsync(PageOptionsDto pageOptions);
    }
}