using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeLearn.Data.Models;

public class Credentials : BaseEntity, IExternalSystem
{
    public string Username { get; set; }
    public string Secret { get; set; }
    public Guid ExternalSystemId { get; set; }
    public Guid? CourseId { get; set; }
    public Guid CreatorId { get; set; }

    public virtual ExternalSystem ExternalSystem { get; set; }
    public virtual Course? Course { get; set; }
    public virtual Account Creator { get; set; }

    Guid IExternalSystem.ExternalSystemId => ExternalSystemId;
}
