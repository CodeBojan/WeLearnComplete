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
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? FacultyId { get; set; }

    public virtual ApplicationUser User { get; set; }
    public virtual ICollection<AccountRole> Roles { get; set; }
    public virtual ICollection<Credentials> Credentials { get; set; }
}
