using WeLearn.Api.Dtos.FollowedStudyYear;

namespace WeLearn.Api.Services.FollowedStudyYear;

public interface IFollowedStudyYearService
{
    Task<GetFollowedStudyYearDto> FollowStudyYearAsync(Guid accountId, Guid studyYearId);
}