using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeLearn.Data.Models.Notifications;

public enum NotificationType
{
    General,
    Comment,
    Content,
}

public static class NotificationTypeExtensions
{
    public static string Value(this NotificationType notificationType)
    {
        switch (notificationType)
        {
            case NotificationType.General:
                return "General";
            case NotificationType.Content:
                return "Content";
            case NotificationType.Comment:
                return "Comment";
            default:
                throw new NotImplementedException();
        }
    }
}
