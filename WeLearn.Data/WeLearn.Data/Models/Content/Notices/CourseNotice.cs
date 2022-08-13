using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeLearn.Data.Models.Content.Notices;

public class CourseNotice : Notice
{
    public CourseNotice(
        long? externalId,
        string? externalUrl,
        string body,
        string title,
        string? author,
        bool isImported,
        Guid? courseId,
        Guid? creatorId,
        Guid? externalSystemId,
        DateTime? relevantUntil) : base(
            externalId,
            externalUrl,
            body,
            title,
            author,
            isImported,
            courseId,
            ContentType.NoticeCourse.Value(),
            creatorId,
            externalSystemId,
            relevantUntil)
    {
    }

    public override void Update(Content content)
    {
        if (content is not CourseNotice courseNotice)
            throw new ArgumentException($"{nameof(content)} is not a {nameof(CourseNotice)}");
        
        base.Update(content);
    }
}
