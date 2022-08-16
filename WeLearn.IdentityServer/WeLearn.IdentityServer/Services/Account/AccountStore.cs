using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using WeLearn.Data.Models;
using WeLearn.Data.Models.Roles;
using WeLearn.Data.Persistence;
using WeLearn.Shared.Exceptions.Models;

namespace WeLearn.IdentityServer.Services.Account;

public class AccountStore : IAccountStore
{
    private readonly ApplicationDbContext _dbContext;
    private readonly ILogger _logger;

    public AccountStore(
        ApplicationDbContext dbContext,
        ILogger<AccountStore> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<AccountRole> AddRoleAsync(WeLearn.Data.Models.Account account, Role role, RoleType roleType)
    {
        _logger.LogInformation("Adding role {@RoleId} to account {@AccountId}", role.Id, account.Id);

        var entityId = new Guid(role.Value);
        var accountId = account.Id;
        var roleId = role.Id;
        string roleTypeStr = roleType.ToString();

        AccountRole accountRole;
        if (roleType is RoleType.CourseAdmin)
        {
            var course = await _dbContext.Courses.FirstOrDefaultAsync(c => c.Id == entityId);
            if (course is null)
                throw new CourseNotFoundException(entityId.ToString());

            accountRole = new CourseAdminRole(accountId, roleId, roleTypeStr, course.Id);
        }
        else if (roleType is RoleType.StudyYearAdmin)
        {
            var studyYear = await _dbContext.StudyYears.FirstOrDefaultAsync(y => y.Id == entityId);
            if (studyYear is null)
                throw new StudyYearNotFoundException(entityId.ToString());

            accountRole = new StudyYearAdminRole(accountId, roleId, roleTypeStr, studyYear.Id);
        }
        else throw new NotImplementedException();

        _dbContext.Add(accountRole);

        return accountRole;
    }

    public async Task<IdentityResult> AddRoleClaimAsync(Guid userId, RoleType roleType, string value)
    {
        var roleTypeStr = roleType.ToString();
        var role = await GetOrCreateRoleFromRoleClaimAsync(roleTypeStr, roleTypeStr, value);

        var account = await GetAccountAsync(userId);
        try
        {
            var accountRole = await AddRoleAsync(account, role, roleType);
        }
        catch (Exception ex)
        {
            _dbContext.Remove(role);
            if (ex is CourseNotFoundException or StudyYearNotFoundException)
            {
                return IdentityResult.Failed(new IdentityError() { Code = nameof(AccountRole), Description = "Entity not found" });
            }
            else
                throw;
        }

        await _dbContext.SaveChangesAsync();

        return IdentityResult.Success;
    }

    public async Task<IdentityResult> CreateAccountAsync(ApplicationUser user)
    {
        _logger.LogInformation("Creating account for user {@Username}", user.UserName);

        _dbContext.Accounts.Add(new WeLearn.Data.Models.Account(user.Id, user.UserName));

        await _dbContext.SaveChangesAsync();

        return IdentityResult.Success;
    }

    public async Task<WeLearn.Data.Models.Account> GetAccountAsync(Guid id)
    {
        var account = await _dbContext.Accounts.FirstOrDefaultAsync(a => a.Id == id);

        return account;
    }

    public async Task<Role> GetOrCreateRoleFromRoleClaimAsync(string name, string type, string value)
    {
        var role = await _dbContext.ApiRoles.FirstOrDefaultAsync(r =>
        r.Type == type
        && r.Value == value
        && r.Name == name);
        if (role is null)
        {
            role = new Role(name, type, value);

            _dbContext.Add(role);
        }

        return role;
    }

    public async Task<IdentityResult> RemoveAccountRoleClaimAsync(ApplicationUser user, Claim claim, RoleType roleType)
    {
        var accountId = user.Id;
        var entityId = new Guid(claim.Value);

        AccountRole accountRole;
        if (roleType is RoleType.CourseAdmin)
        {
            accountRole = await _dbContext.CourseAdminRoles.FirstOrDefaultAsync(car => car.CourseId == entityId && car.AccountId == accountId);
        }
        else if (roleType is RoleType.StudyYearAdmin)
        {
            accountRole = await _dbContext.StudyYearAdminRoles.FirstOrDefaultAsync(syar => syar.StudyYearId == entityId && syar.AccountId == accountId);
        }
        else throw new NotImplementedException();

        if (accountRole is not null)
        {
            _dbContext.Remove(accountRole);
            await _dbContext.SaveChangesAsync();
        }

        return IdentityResult.Success;
    }
}
