using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeLearn.Data.Models.Notifications;

public class CommentNotification : Notification
{
    public CommentNotification()
    {
    }

    public CommentNotification(
        string title,
        string body,
        bool isRead,
        string? uri,
        string? imageUri,
        string? operationType,
        Guid receiverId,
        Guid commentId) : base(
            title,
            body,
            isRead,
            uri,
            imageUri,
            NotificationType.Comment.Value(),
            operationType,
            receiverId)
    {
        CommentId = commentId;
    }

    public Guid CommentId { get; set; }

    public virtual Comment Comment { get; set; }
}
