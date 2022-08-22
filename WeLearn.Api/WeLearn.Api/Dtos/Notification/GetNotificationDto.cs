namespace WeLearn.Api.Dtos.Notification;

public class GetNotificationDto
{
    public string Title { get; set; }
    public string Body { get; set; }
    public bool IsRead { get; set; }
    public string Uri { get; set; }
    public string Type { get; set; }
    public Guid? ReceiverId { get; set; }

}
