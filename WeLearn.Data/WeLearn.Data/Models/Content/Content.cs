using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeLearn.Data.Models.Notifications;

namespace WeLearn.Data.Models.Content;

public class Content : BaseEntity
{
    public long? ExternalId { get; set; }
    public string? ExternalUrl { get; set; }
    public string Body { get; set; }
    public string Title { get; set; }
    public string? Author { get; set; }
    public bool IsImported { get; set; }
    // TODO make course nullable
    // TODO add GeneralNotice type
    public Guid? CourseId { get; set; }
    public string Type { get; set; }
    public Guid? CreatorId { get; set; }
    public Guid? ExternalSystemId { get; set; }

    public virtual Account? Creator { get; set; }
    public virtual Course? Course { get; set; }
    public virtual ICollection<Comment> Comments { get; set; }
    public virtual ExternalSystem ExternalSystem { get; set; }
    public virtual ICollection<ContentNotification> ContentNotifications { get; set; }
}
