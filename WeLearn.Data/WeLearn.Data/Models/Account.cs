using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeLearn.Data.Models.Roles;

namespace WeLearn.Data.Models;

public class Account : BaseEntity
{
    public string Username { get; set; }

    public Account(Guid id, string username)
    {
        Id = id;
        Username = username;
    }

    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? FacultyId { get; set; }

    public virtual ApplicationUser User { get; set; }
    public virtual ICollection<AccountRole> Roles { get; set; }
    public virtual ICollection<Credentials> Credentials { get; set; }
    public virtual ICollection<FollowedCourse> FollowedCourses { get; set; }
    public virtual ICollection<Comment> Comments { get; set; }
    public virtual ICollection<CourseMaterialUploadRequest> CourseMaterialUploadRequests { get; set; }
    public virtual ICollection<Content.Content> CreatedContent { get; set; }
    public virtual ICollection<Notification> Notifications { get; set; }
}
