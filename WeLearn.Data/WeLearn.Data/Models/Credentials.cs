using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeLearn.Data.Models;

public class Credentials : BaseEntity, IExternalSystem
{
    public Credentials(
        string username,
        string secret,
        Guid creatorId,
        Guid externalSystemId)
    {
        Id = Guid.NewGuid();
        Username = username;
        Secret = secret;
        CreatorId = creatorId;
        ExternalSystemId = externalSystemId;
    }

    public string Username { get; set; }
    public string Secret { get; set; }
    public Guid ExternalSystemId { get; set; }
    public Guid CreatorId { get; set; }

    public virtual ExternalSystem ExternalSystem { get; set; }
    public virtual ICollection<CourseCredentials> CourseCredentials { get; set; }
    public virtual Account Creator { get; set; }

    Guid IExternalSystem.ExternalSystemId => ExternalSystemId;

    public void AddCourseCredentials(CourseCredentials courseCredentials)
    {
        if (CourseCredentials is null)
            CourseCredentials = new HashSet<CourseCredentials>();

        CourseCredentials.Add(courseCredentials);
    }
}
