namespace WeLearn.Importers.Dtos.Notification;

public class GetNotificationDto
{
    public Guid Id { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }
    public string Title { get; set; }
    public string Body { get; set; }
    public bool IsRead { get; set; }
    public string Uri { get; set; }
    public string Type { get; set; }
    public string? ImageUri { get; set; }
    public Guid ReceiverId { get; set; }
}