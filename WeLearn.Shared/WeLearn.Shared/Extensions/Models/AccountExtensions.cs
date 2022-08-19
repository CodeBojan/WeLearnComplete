using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WeLearn.Data.Models;
using WeLearn.Shared.Dtos.Account;

namespace WeLearn.Shared.Extensions.Models;

public static class AccountExtensions
{
    public static GetAccountDto MapToGetDto(this Account a)
    {
        return new GetAccountDto
        {
            Id = a.Id,
            FirstName = a.FirstName,
            LastName = a.LastName,
            Email = a.User.Email,
            Username = a.Username,
            FacultyStudendId = a.StudentId,
        };
    }

    public static Expression<Func<Account, GetAccountDto>> MapAccountToGetDto()
    {
        return a => a.MapToGetDto();
    }
}
