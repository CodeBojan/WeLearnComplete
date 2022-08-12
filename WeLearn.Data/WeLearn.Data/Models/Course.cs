using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeLearn.Data.Models.Roles;

namespace WeLearn.Data.Models;

public class Course : BaseEntity
{
    // TODO index on codes
    public string Code { get; set; }
    public string ShortName { get; set; }
    public string FullName { get; set; }
    public string Staff { get; set; }
    public string Description { get; set; }
    public string Rules { get; set; }
    public Guid StudyYearId { get; set; }


    public virtual ICollection<Credentials> Credentials { get; set; }
    public virtual StudyYear StudyYear { get; set; }
    public virtual ICollection<CourseAdminRole> AdminRoles { get; set; }
    public virtual ICollection<FollowedCourse> FollowingUsers { get; set; }
}
