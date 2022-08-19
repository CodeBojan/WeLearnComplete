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
            // TODO : Map properties
        };
    }
}
