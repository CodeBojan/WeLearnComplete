using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeLearn.Data.Models.Content.Notices;

// TODO add ICollection of Courses that this study year notice can relate to - for creating manual relations, through the api
public class StudyYearNotice : Notice
{
    public StudyYearNotice(
        long? externalId,
        string? externalUrl,
        string body,
        string title,
        string? author,
        bool isImported,
        Guid? creatorId,
        Guid? externalSystemId,
        DateTime? externalCreatedDate,
        DateTime? relevantUntil,
        Guid studyYearId) : base(
            externalId,
            externalUrl,
            body,
            title,
            author,
            isImported,
            null,
            ContentType.NoticeStudyYear.Value(),
            creatorId,
            externalSystemId,
            externalCreatedDate,
            relevantUntil)
    {
        StudyYearId = studyYearId;
    }

    public Guid StudyYearId { get; set; }

    public virtual StudyYear StudyYear { get; set; }

    public override void Update(Content content)
    {
        if (content is not StudyYearNotice studyYearNotice)
            throw new ArgumentException($"{nameof(content)} is not a {nameof(StudyYearNotice)}");

        StudyYearId = studyYearNotice.StudyYearId;
        base.Update(content);
    }
}
