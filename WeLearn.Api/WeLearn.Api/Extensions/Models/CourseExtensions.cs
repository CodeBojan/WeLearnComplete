using WeLearn.Api.Dtos.Course;
using WeLearn.Data.Models;

namespace WeLearn.Api.Extensions.Models;

public static class CourseExtensions
{
    public static GetCourseDto MapToGetDto(this Course c)
    {
        return new GetCourseDto
        {
            Id = c.Id,
            Code = c.Code,
            Description = c.Description,
            FullName = c.FullName,
            Rules = c.Rules,
            ShortName = c.ShortName,
            Staff = c.Staff,
            StudyYearId = c.StudyYearId,
            FollowingCount = c.FollowingCount,
            IsFollowing = c.IsFollowing,
            CreatedDate = c.CreatedDate,
            UpdatedDate = c.UpdatedDate
        };
    }
}
