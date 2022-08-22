using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeLearn.Importers.Dtos.Notification;

public class PostNotificationReadStatusDto
{
    public Guid NotificationId { get; set; }
    public bool ReadState { get; set; }
}
