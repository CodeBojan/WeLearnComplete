using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeLearn.Data.Models;

public class Notification : BaseEntity
{
    internal Notification()
    {
    }

    public Notification(
        string title,
        string body,
        bool isRead,
        string? uri,
        string? imageUri,
        string type,
        string? operationType,
        Guid receiverId)
    {
        Id = Guid.NewGuid();
        Title = title;
        Body = body;
        IsRead = isRead;
        Uri = uri;
        ImageUri = imageUri;
        Type = type;
        OperationType = operationType;
        ReceiverId = receiverId;
    }

    public string Title { get; set; }
    public string Body { get; set; }
    public bool IsRead { get; set; }
    public string? Uri { get; set; }
    public string? ImageUri { get; set; }
    public string Type { get; set; }
    public string? OperationType { get; set; }
    public Guid ReceiverId { get; set; }

    public virtual Account Receiver { get; set; }
}
