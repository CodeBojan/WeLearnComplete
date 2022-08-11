using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeLearn.Data.Models.Roles;

public class StudyYearAdminRole : AccountRole
{
    public StudyYearAdminRole(
        Guid accountId,
        Guid roleId,
        string type,
        Guid studyYearId) : base(
            accountId,
            roleId,
            type)
    {
        StudyYearId = studyYearId;
    }

    public Guid StudyYearId { get; set; }

    public virtual StudyYear StudyYear { get; set; }
}
