using WeLearn.Api.Dtos.FollowedStudyYear;
using WeLearn.Data.Models;

namespace WeLearn.Api.Extensions.Models;

public static class FollowedStudyYearExtensions
{
    public static GetFollowedStudyYearDto MapToGetDto(this FollowedStudyYear fsy)
    {
        return new GetFollowedStudyYearDto
        {
            AccountId = fsy.AccountId,
            StudyYearId = fsy.StudyYearId,
        };
    }
}
