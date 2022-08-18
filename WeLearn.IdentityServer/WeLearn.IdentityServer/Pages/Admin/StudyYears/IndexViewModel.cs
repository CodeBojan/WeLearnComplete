using WeLearn.Shared.Dtos.Paging;
using WeLearn.Shared.Dtos.StudyYear;

namespace WeLearn.IdentityServer.Pages.Admin.StudyYears;

public class IndexViewModel
{
    public PagedResponseDto<GetStudyYearDto> PagedStudyYears { get; set; }
}
