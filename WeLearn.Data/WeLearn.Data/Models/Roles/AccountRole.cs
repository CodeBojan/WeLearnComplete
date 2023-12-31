﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeLearn.Data.Models.Roles;

public class AccountRole : BaseEntity
{
    public AccountRole(Guid accountId, Guid roleId, string type)
    {
        AccountId = accountId;
        RoleId = roleId;
        Type = type;
    }

    public Guid AccountId { get; set; }
    public Guid RoleId { get; set; }
    public string? Type { get; set; }

    public virtual Account Account { get; set; }
    public virtual Role Role { get; set; }
}
