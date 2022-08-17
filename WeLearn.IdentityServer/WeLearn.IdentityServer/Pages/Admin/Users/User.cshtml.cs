using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ClaimTypes = WeLearn.Auth.Authorization.Claims.ClaimTypes;
using WeLearn.Data.Models;
using WeLearn.Data.Persistence;
using System.Security.Claims;
using WeLearn.IdentityServer.Extensions.ModelState;

namespace WeLearn.IdentityServer.Pages.Admin.Users;

public class UserModel : PageModel
{
    private readonly ApplicationDbContext _dbContext;
    private readonly UserManager<ApplicationUser> _userManager;
    public UserModel(ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager)
    {
        _dbContext = dbContext;
        _userManager = userManager;
    }

    [BindProperty(SupportsGet = true)]
    public string UserId { get; set; }

    public UserViewModel ViewModel { get; set; }

    public AddUserRoleClaimModel AddUserRoleClaimModel { get; set; }

    public DeleteUserRoleViewModel DeleteUserRoleViewModel { get; set; }

    public async Task<IActionResult> OnGet()
    {
        var user = await GetUserAsync(UserId);

        return await GetIndexPageAsync(user);
    }

    private async Task<IActionResult> GetIndexPageAsync(ApplicationUser user)
    {
        if (user is null)
        {
            AddUserNotFoundModelStateError();
            return RedirectToPage("/Admin/Users/Index");
        }

        await PrepareViewModelAsync(user);

        return Page();
    }

    private void AddUserNotFoundModelStateError()
    {
        ModelState.AddModelError("User", "User not found.");
    }

    public async Task<IActionResult> OnPostAddRoleClaim(AddUserRoleClaimModel addUserRoleClaimModel)
    {
        var userId = addUserRoleClaimModel.UserId;
        var claimType = addUserRoleClaimModel.ClaimType;
        var entityId = addUserRoleClaimModel.EntityId;

        var user = await GetUserAsync(userId);
        if (!ModelState.IsValid)
            return await GetIndexPageAsync(user);

        string claimValue = entityId;

        switch (claimType)
        {
            case ClaimTypes.StudyYearAdmin or ClaimTypes.CourseAdmin:
                break;
            default:
                ModelState.AddModelError(nameof(ClaimTypes), "Invalid Claim Type");
                return await GetIndexPageAsync(user);
        }

        var claim = await GetUserClaimAsync(user, claimType, claimValue);
        if (claim is not null)
        {
            ModelState.AddModelError(nameof(ClaimTypes), "Claim type and value already exist on the user.");
            return await GetIndexPageAsync(user);
        }

        var addClaimResult = await _userManager.AddClaimAsync(user, new Claim(claimType, claimValue));
        ModelState.TryAddModelStateErrors(addClaimResult);

        return await GetIndexPageAsync(user);
    }

    private async Task<Claim> GetUserClaimAsync(ApplicationUser user, string claimType, string claimValue)
    {
        var userClaims = await _userManager.GetClaimsAsync(user);
        var claim = userClaims.Where(c => c.Type == claimType && c.Value == claimValue).FirstOrDefault();
        return claim;
    }

    public async Task<IActionResult> OnPostDeleteRoleClaim([FromForm] DeleteUserRoleViewModel deleteUserRoleViewModel)
    {
        var userId = deleteUserRoleViewModel.UserId;
        var claimType = deleteUserRoleViewModel.ClaimType;
        var claimValue = deleteUserRoleViewModel.ClaimValue;
        var user = await GetUserAsync(userId);
        if (!ModelState.IsValid)
            return await GetIndexPageAsync(user);

        var claim = await GetUserClaimAsync(user, claimType, claimValue);
        if (claim is null)
        {
            ModelState.AddModelError(nameof(ClaimTypes), "User does not have the specified claim.");
            return await GetIndexPageAsync(user);
        }

        var removeClaimResult = await _userManager.RemoveClaimAsync(user, claim);
        ModelState.TryAddModelStateErrors(removeClaimResult);

        return await GetIndexPageAsync(user);
    }

    private async Task<ApplicationUser> GetUserAsync(string userId)
    {
        return await _userManager.FindByIdAsync(userId);
    }

    private async Task PrepareViewModelAsync(ApplicationUser user)
    {
        var claimTypes = new List<string>()
        {
        ClaimTypes.CourseAdmin,
        ClaimTypes.StudyYearAdmin
        };

        var roles = await _userManager.GetRolesAsync(user);
        var allClaims = await _userManager.GetClaimsAsync(user);
        var claims = allClaims.Where(c => claimTypes.Contains(c.Type)).ToList();

        ViewModel = new()
        {
            Id = user.Id.ToString(),
            Username = user.UserName,
            Email = user.Email,
            Roles = roles.ToArray(),
            Approved = user.Approved,
            CreatedDate = user.CreatedDate,
            RoleClaims = claims.Select(c => new RoleClaim { Type = c.Type, Value = c.Value }).ToArray()
        };
    }
}
