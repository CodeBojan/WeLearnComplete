using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeLearn.Data.Models.Notifications;

namespace WeLearn.Data.Models.Content;

public class Content : BaseEntity
{
    public Content(
        string? externalId,
        string? externalUrl,
        string? body,
        string? title,
        string? author,
        bool isImported,
        Guid? courseId,
        string type,
        Guid? creatorId,
        Guid? externalSystemId,
        DateTime? externalCreatedDate)
    {
        ExternalId = externalId;
        ExternalUrl = externalUrl;
        Body = body;
        Title = title;
        Author = author;
        IsImported = isImported;
        CourseId = courseId;
        Type = type;
        CreatorId = creatorId;
        ExternalSystemId = externalSystemId;
        ExternalCreatedDate = externalCreatedDate;
    }

    public string? ExternalId { get; set; }
    public string? ExternalUrl { get; set; }
    public string? Body { get; set; }
    public string? Title { get; set; }
    public string? Author { get; set; }
    public bool IsImported { get; set; }
    public Guid? CourseId { get; set; }
    public string Type { get; set; }
    public Guid? CreatorId { get; set; }
    public Guid? ExternalSystemId { get; set; }
    public DateTime? ExternalCreatedDate { get; set; }

    public virtual Account? Creator { get; set; }
    public virtual Course? Course { get; set; }
    public virtual ICollection<Comment> Comments { get; set; }
    public virtual ExternalSystem ExternalSystem { get; set; }
    public virtual ICollection<ContentNotification> ContentNotifications { get; set; }

    public virtual void Update(Content content)
    {
        ExternalId = content.ExternalId;
        ExternalUrl = content.ExternalUrl;
        Body = content.Body;
        Title = content.Title;
        Author = content.Author;
        IsImported = content.IsImported;
        CourseId = content.CourseId;
        Type = content.Type;
        CreatorId = content.CreatorId;
        ExternalSystemId = content.ExternalSystemId;
    }
}
