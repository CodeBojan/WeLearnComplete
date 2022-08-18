namespace WeLearn.Api.Dtos.FollowedCourse;

public class DeleteFollowedCourseDto
{
    public Guid AccountId { get; set; }
    public Guid CourseId { get; set; }
}
