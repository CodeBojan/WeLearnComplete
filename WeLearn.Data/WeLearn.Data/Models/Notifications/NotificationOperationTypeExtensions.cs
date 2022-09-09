namespace WeLearn.Data.Models.Notifications;

// TODO move
public static class NotificationOperationTypeExtensions
{
    public static string Value(this NotificationOperationType notificationOperationType)
    {
        switch (notificationOperationType)
        {
            case NotificationOperationType.Create:
                return "Create";
            case NotificationOperationType.Update:
                return "Update";
            default:
                return "Unknown";
        }
    }

    public static string FriendlyName(this NotificationOperationType notificationOperationType)
    {
        switch (notificationOperationType)
        {
            case NotificationOperationType.Create:
                return "created";
            case NotificationOperationType.Update:
                return "updated";
            default:
                return "unknown";
        }
    }
}