namespace WeLearn.Api.Dtos.FollowedCourse;

public class GetFollowedCourseDto
{
    public Guid AccountId { get; set; }
    public Guid CourseId { get; set; }
    public string CourseShortName { get; set; }
    public string CourseFullName { get; set; }
    public string CourseCode { get; set; }
}
