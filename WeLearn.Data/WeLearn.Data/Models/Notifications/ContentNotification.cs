using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeLearn.Data.Models.Notifications;

public class ContentNotification : Notification
{
    public Guid ContentId { get; set; }

    public virtual Content.Content Content { get; set; }
}
