using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeLearn.Data.Models.Notifications;

public class ContentNotification : Notification
{
    public ContentNotification()
    {

    }

    public ContentNotification(
        string title,
        string body,
        bool isRead,
        string? uri,
        string? imageUri,
        string? operationType,
        Guid receiverId,
        Guid contentId) : base(
            title,
            body,
            isRead,
            uri,
            imageUri,
            NotificationType.Content.Value(),
            operationType,
            receiverId)
    {
        ContentId = contentId;
    }

    public Guid ContentId { get; set; }

    public virtual Content.Content Content { get; set; }
}
