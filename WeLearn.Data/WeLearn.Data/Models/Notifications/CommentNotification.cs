using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeLearn.Data.Models.Notifications;

public class CommentNotification : Notification
{
    public Guid CommentId { get; set; }

    public virtual Comment Comment { get; set; }
}
