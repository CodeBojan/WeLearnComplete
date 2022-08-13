using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeLearn.Data.Models.Content.Notices;

public class GeneralNotice : Notice
{
    public GeneralNotice(
        long? externalId,
        string? externalUrl,
        string body,
        string title,
        string? author,
        bool isImported,
        Guid? courseId,
        string type,
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
            type,
            creatorId,
            externalSystemId,
            relevantUntil)
    {
    }

    public override void Update(Content content)
    {
        if (content is not GeneralNotice notice)
            throw new ArgumentException($"{nameof(content)} is not a {nameof(GeneralNotice)}");
        
        base.Update(content);
    }
}
