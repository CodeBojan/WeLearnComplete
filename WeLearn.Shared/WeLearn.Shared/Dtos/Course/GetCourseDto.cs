﻿namespace WeLearn.Shared.Dtos.Course;

public class GetCourseDto
{
    public Guid Id { get; set; }
    public string Code { get; set; }
    public string ShortName { get; set; }
    public string FullName { get; set; }
    public string Staff { get; set; }
    public string Description { get; set; }
    public string Rules { get; set; }
    public Guid StudyYearId { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }
    public int? FollowingCount { get; set; }
    public bool? IsFollowing { get; set; }
}
