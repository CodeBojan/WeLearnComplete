using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeLearn.Data.Models.Content;

public class Notice : Content
{
    public Notice(
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
            externalSystemId)
    {
        Id = Guid.NewGuid();
        RelevantUntil = relevantUntil;
    }

    public DateTime? RelevantUntil { get; set; }

    public override void Update(Content content)
    {
        if (content is not Notice notice)
            throw new ArgumentException("The given content is not a Notice");
        
        RelevantUntil = notice.RelevantUntil;
        base.Update(notice);
    }
}
