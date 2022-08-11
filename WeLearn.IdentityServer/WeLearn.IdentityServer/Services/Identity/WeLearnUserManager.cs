using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using ClaimTypes = WeLearn.Auth.Authorization.Claims.ClaimTypes;
using WeLearn.Data.Models;
using WeLearn.IdentityServer.Services.Account;
using WeLearn.Auth.Authorization.Roles;
using WeLearn.Data.Models.Roles;

namespace WeLearn.IdentityServer.Services.Identity;

public class WeLearnUserManager : UserManager<ApplicationUser>
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

            if (accountResult.Succeeded)
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

            await _accountStore.RemoveAccountRoleClaimAsync(user, claim, roleType);
        }

        return result;
    }
}
