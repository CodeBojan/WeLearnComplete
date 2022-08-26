using WeLearn.Api.Dtos.Notice;
using WeLearn.Shared.Dtos.Paging;

namespace WeLearn.Api.Services.Notice
{
    public interface INoticeService
    {
        Task<PagedResponseDto<GetStudyYearNoticeDto>> GetStudyYearNoticesAsync(Guid studyYearId, PageOptionsDto pageOptions);
    }
}