using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeLearn.Data.Models.Roles;

public class CourseAdminRole : AccountRole
{
    public Guid CourseId { get; set; }

    public virtual Course Course { get; set; }
}
