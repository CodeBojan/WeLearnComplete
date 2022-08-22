using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeLearn.Importers.Dtos.Notification;

namespace WeLearn.Importers.Extensions.Models;

public static class NotificationExtensions
{
    public static GetNotificationDto MapToGetDto(this Data.Models.Notification n)
    {
        return new GetNotificationDto
        {
            Id = n.Id,
            CreatedDate = n.CreatedDate,
            UpdatedDate = n.UpdatedDate,
            Title = n.Title,
            Body = n.Body,
            IsRead = n.IsRead,
            Uri = n.Uri,
            Type = n.Type,
            ImageUri = n.ImageUri,
            ReceiverId = n.ReceiverId,
        };
    }
}
