using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using ClaimTypes = WeLearn.Auth.Authorization.Claims.ClaimTypes;
using WeLearn.Data.Models;
using WeLearn.Auth.Authorization.Roles;
using WeLearn.Data.Models.Roles;
using WeLearn.Shared.Services.Account;

namespace WeLearn.IdentityServer.Services.Identity;

public class WeLearnUserManager : UserManager<ApplicationUser>, IRoleManager
{
    private readonly IAccountStore _accountStore;

    public WeLearnUserManager(
        IUserStore<ApplicationUser> store,
        IOptions<IdentityOptions> optionsAccessor,
        IPasswordHasher<ApplicationUser> passwordHasher,
        IEnumerable<IUserValidator<ApplicationUser>> userValidators,
        IEnumerable<IPasswordValidator<ApplicationUser>> passwordValidators,
        ILookupNormalizer keyNormalizer,
        IdentityErrorDescriber errors,
        IServiceProvider services,
        ILogger<UserManager<ApplicationUser>> logger,
        IAccountStore accountStore) : base(
            store,
            optionsAccessor,
            passwordHasher,
            userValidators,
            passwordValidators,
            keyNormalizer,
            errors,
            services,
            logger)
    {
        _accountStore = accountStore;
    }

    // TODO override other methods with claims
    // TODO override Role methods

    public override async Task<IdentityResult> CreateAsync(ApplicationUser user)
    {
        var result = await base.CreateAsync(user);

        if (result.Succeeded)
        {
            var accountResult = await _accountStore.CreateAccountAsync(user);

            if (accountResult.Succeeded && user.Approved)
            {
                var addToUserResult = await AddToRoleAsync(user, Roles.User);
                result = addToUserResult;
            }
        }

        return result;
    }

    // TODO use EF transactions
    public override async Task<IdentityResult> AddClaimAsync(ApplicationUser user, Claim claim)
    {
        var result = await base.AddClaimAsync(user, claim);

        if (result.Succeeded && IsAccountRole(claim))
        {
            var roleType = GetRoleType(claim.Type);
            var accountRoleResult = await _accountStore.AddRoleClaimAsync(user.Id, roleType, claim.Value);
            result = accountRoleResult;

            if (!accountRoleResult.Succeeded)
                await RemoveClaimAsync(user, claim);
        }


        return result;
    }

    // TODO make extension
    private static bool IsAccountRole(Claim claim)
    {
        return claim.Type == ClaimTypes.CourseAdmin || claim.Type == ClaimTypes.StudyYearAdmin;
    }

    // TODO make extension
    private static RoleType GetRoleType(string claimType)
    {
        switch (claimType)
        {
            case ClaimTypes.CourseAdmin:
                return RoleType.CourseAdmin;
            case ClaimTypes.StudyYearAdmin:
                return RoleType.StudyYearAdmin;
            default:
                throw new NotImplementedException();
        }
    }

    public override async Task<IdentityResult> RemoveClaimAsync(ApplicationUser user, Claim claim)
    {
        var result = await base.RemoveClaimAsync(user, claim);

        if (result.Succeeded && IsAccountRole(claim))
        {
            var roleType = GetRoleType(claim.Type);

            return await _accountStore.RemoveAccountRoleClaimAsync(user, claim, roleType);
        }

        return result;
    }

    public async Task<IdentityResult> AddStudyYearAdminRoleAsync(Guid userId, Guid studyYearId)
    {
        var user = await FindByIdAsync(userId.ToString());
        if (user is null)
            return IdentityResult.Failed(new IdentityError { Code = "UserNotFound", Description = "User not found" });

        var claims = await GetClaimsAsync(user);
        if (claims.Any(c => c.Type == ClaimTypes.StudyYearAdmin && c.Value == studyYearId.ToString()))
            return IdentityResult.Failed(
new IdentityError { Code = "AccountAlreadyStudyYearAdmin", Description = "The account is already a study year admin" });

        var result = await AddClaimAsync(user, new Claim(ClaimTypes.StudyYearAdmin, studyYearId.ToString()));
        return result;
    }

    public async Task<IdentityResult> RemoveStudyYearAdminRoleAsync(Guid userId, Guid studyYearId)
    {
        var user = await FindByIdAsync(userId.ToString());
        if (user is null)
            return IdentityResult.Failed(new IdentityError { Code = "UserNotFound", Description = "User not found" });

        var claims = await GetClaimsAsync(user);
        if (!claims.Any(c => c.Type == ClaimTypes.StudyYearAdmin && c.Value == studyYearId.ToString()))
            return IdentityResult.Failed(
new IdentityError { Code = "AccountNotStudyYearAdmin", Description = "The account is not a study year admin" });

        var result = await RemoveClaimAsync(user, new Claim(ClaimTypes.StudyYearAdmin, studyYearId.ToString()));
        return result;
    }
}
