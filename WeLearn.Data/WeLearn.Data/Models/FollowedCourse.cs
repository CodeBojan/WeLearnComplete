namespace WeLearn.Data.Models;

public class FollowedCourse : DatedEntity
{
    public FollowedCourse(Guid accountId, Guid courseId)
    {
        AccountId = accountId;
        CourseId = courseId;
    }

    public Guid AccountId { get; set; }
    public Guid CourseId { get; set; }

    public virtual Account Account { get; set; }
    public virtual Course Course { get; set; }
}