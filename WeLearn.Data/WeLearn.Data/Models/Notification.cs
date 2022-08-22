using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeLearn.Data.Models;

public class Notification : BaseEntity
{
    public string Title { get; set; }
    public string Body { get; set; }
    public bool IsRead { get; set; }
    public string Uri { get; set; }
    public string? ImageUri { get; set; }
    public string Type { get; set; }
    public Guid ReceiverId { get; set; }

    public virtual Account Receiver { get; set; }
}
