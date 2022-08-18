using WeLearn.Api.Dtos.FollowedStudyYear;
using WeLearn.Shared.Dtos.Paging;

namespace WeLearn.Api.Services.FollowedStudyYear;

public interface IFollowedStudyYearService
{
    Task<GetFollowedStudyYearDto> FollowStudyYearAsync(Guid accountId, Guid studyYearId);
    Task<PagedResponseDto<GetFollowedStudyYearDto>> GetUserFollowedStudyYearsAsync(Guid accountId, PageOptionsDto pageOptions);
    Task<GetFollowedStudyYearDto> UnfollowStudyYearAsync(Guid accountId, Guid studyYearId);
}