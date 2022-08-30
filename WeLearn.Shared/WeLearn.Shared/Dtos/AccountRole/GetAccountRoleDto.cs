using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeLearn.Shared.Dtos.AccountRole;

public class GetAccountRoleDto
{
    public Guid AccountId { get; set; }
    public Guid RoleId { get; set; }
    public string? Type { get; set; }
    public Guid? EntityId { get; set; }
}
