using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeLearn.Data.Models.Roles;

public class Role : BaseEntity
{
    public Role(string name, string type, string value)
    {
        Id = Guid.NewGuid();
        Name = name;
        Type = type;
        Value = value;
    }

    public string Name { get; set; }
    public string? Type { get; set; }
    public string? Value { get; set; }

    public virtual ICollection<AccountRole> AccountRoles { get; set; }
}
