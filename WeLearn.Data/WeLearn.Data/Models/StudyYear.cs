using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeLearn.Data.Models.Content.Notices;
using WeLearn.Data.Models.Roles;

namespace WeLearn.Data.Models;

public class StudyYear : BaseEntity
{
    public StudyYear(string shortName, string fullName, string description)
    {
        Id = Guid.NewGuid();
        ShortName = shortName;
        FullName = fullName;
        Description = description;
    }

    public string ShortName { get; set; }
    public string FullName { get; set; }
    public string Description { get; set; }

    public virtual ICollection<Course> Courses { get; set; }
    public virtual ICollection<StudyYearAdminRole> AdminRoles { get; set; }
    public virtual ICollection<StudyYearNotice> StudyYearNotices { get; set; }
    public virtual ICollection<FollowedStudyYear> FollowedStudyYears { get; set; }
}
