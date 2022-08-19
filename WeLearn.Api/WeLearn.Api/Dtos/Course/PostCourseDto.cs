namespace WeLearn.Api.Dtos.Course;

public class PostCourseDto
{
    public string Code { get; set; }
    public string ShortName { get; set; }
    public string FullName { get; set; }
    public string Staff { get; set; }
    public string Description { get; set; }
    public string Rules { get; set; }
    public Guid StudyYearId { get; set; }
}
