using WeLearn.Data.Models;
using WeLearn.Shared.Dtos.Course;

namespace WeLearn.Shared.Extensions.Models;

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
