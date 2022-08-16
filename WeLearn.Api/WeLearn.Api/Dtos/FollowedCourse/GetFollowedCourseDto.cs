namespace WeLearn.Api.Dtos.FollowedCourse;

public class GetFollowedCourseDto
{
    public Guid AccountId { get; set; }
    public Guid CourseId { get; set; }
}
