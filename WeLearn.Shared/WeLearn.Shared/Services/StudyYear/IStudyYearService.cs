using WeLearn.Shared.Dtos.Paging;
using WeLearn.Shared.Dtos.StudyYear;

namespace WeLearn.Shared.Services.StudyYear
{
    public interface IStudyYearService
    {
        Task<PagedResponseDto<GetStudyYearDto>> GetStudyYearsAsync(PageOptionsDto pageOptions);
    }
}