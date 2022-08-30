using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeLearn.Data.Models.Roles;
using WeLearn.Shared.Dtos.AccountRole;

namespace WeLearn.Shared.Extensions.Models;

public static class AccountRoleExtensions
{
    public static GetAccountRoleDto MapToGetDto(this AccountRole ar)
    {
        return new GetAccountRoleDto
        {
            AccountId = ar.AccountId,
            RoleId = ar.RoleId,
            Type = ar.Type,
            EntityId = ar is StudyYearAdminRole syar ? syar.StudyYearId : ar is CourseAdminRole car ? car.CourseId : null
        };
    }
}
