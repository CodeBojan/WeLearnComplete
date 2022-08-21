using WeLearn.Api.Dtos.FollowedCourse;

namespace WeLearn.Api.Extensions.Models;

public static class FollowedCourseExtensions
{
    public static GetFollowedCourseDto MapToGetDto(this WeLearn.Data.Models.FollowedCourse fc)
    {
        return new GetFollowedCourseDto
        {
            AccountId = fc.AccountId,
            CourseId = fc.CourseId,
            CourseCode = fc.Course.Code,
            CourseFullName = fc.Course.FullName,
            CourseShortName = fc.Course.ShortName,
        };
    }
}
