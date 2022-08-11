using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using WeLearn.Data.Models;
using WeLearn.Data.Models.Roles;

namespace WeLearn.IdentityServer.Services.Account;

public interface IAccountStore
{
    Task<IdentityResult> CreateAccountAsync(ApplicationUser user);
    Task<IdentityResult> AddRoleClaimAsync(Guid id, RoleType roleType, string value);
    Task<IdentityResult> RemoveAccountRoleClaimAsync(ApplicationUser user, Claim claim, RoleType roleType);
}