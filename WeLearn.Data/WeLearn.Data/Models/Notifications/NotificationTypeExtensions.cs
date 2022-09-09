namespace WeLearn.Data.Models.Notifications;

// TODO move
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
