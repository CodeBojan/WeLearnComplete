using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using WeLearn.Data.Models;
using WeLearn.Data.Models.Roles;
using WeLearn.Shared.Dtos.Account;

namespace WeLearn.Shared.Services.Account;

public interface IAccountStore
{
    Task<IdentityResult> CreateAccountAsync(ApplicationUser user);
    Task<IdentityResult> AddRoleClaimAsync(Guid id, RoleType roleType, string value);
    Task<IdentityResult> RemoveAccountRoleClaimAsync(ApplicationUser user, Claim claim, RoleType roleType);
    Task<GetAccountDto> GetAccountAsync(Guid accountId);
    Task<GetAccountDto> UpdateAccountAsync(Guid accountId, string firstName, string lastName, string facultyStudentId);
}