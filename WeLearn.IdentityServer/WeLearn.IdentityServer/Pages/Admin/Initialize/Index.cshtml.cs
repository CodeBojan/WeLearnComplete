using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using WeLearn.Auth.Authorization.Mvc.Filters;
using WeLearn.Data.Models;
using WeLearn.IdentityServer.Extensions.ModelState;
using WeLearn.Shared.Services.Credentials;

namespace WeLearn.IdentityServer.Pages.Admin.Initialize;

[AllowAnonymous]
[ServiceFilter(typeof(IpWhitelistFilter<IpWhitelistSettings>))]
public class IndexModel : PageModel
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ILogger _logger;

    public IndexModel(
        UserManager<ApplicationUser> userManager,
        ILogger<IndexModel> logger)
    {
        _userManager = userManager;
        _logger = logger;
    }

    [BindProperty]
    [Required]
    public AdminCredentialsInputModel AdminCredentialsInputModel { get; set; }

    public async Task<IActionResult> OnGet()
    {
        var adminUsers = await _userManager.GetUsersInRoleAsync(Auth.Authorization.Roles.Roles.Admin);

        if (adminUsers.Any())
        {
            ModelState.AddModelError("", "An admin account already exists");
            var user = adminUsers.First();
            AdminCredentialsInputModel = new AdminCredentialsInputModel
            {
                Username = user.UserName,
                Email = user.Email
            };
        }

        return Page();
    }

    public async Task<IActionResult> OnPost()
    {
        if (!ModelState.IsValid)
            return Page();

        var email = AdminCredentialsInputModel.Email;
        var username = AdminCredentialsInputModel.Username;
        var password = AdminCredentialsInputModel.Password;

        var user = await _userManager.FindByEmailAsync(email);
        if (user is not null)
        {
            var adminRole = Auth.Authorization.Roles.Roles.Admin;
            var isAdmin = await _userManager.IsInRoleAsync(user, adminRole);

            if (!isAdmin)
                ModelState.AddModelError(nameof(AdminCredentialsInputModel.Email), "A non-admin user with this email already exists");

            return Page();
        }

        user = new ApplicationUser(username, email, approved: true);
        var createdUserResult = await _userManager.CreateAsync(user);

        if (!createdUserResult.Succeeded)
        {
            ModelState.TryAddModelStateErrors(createdUserResult);
            return Page();
        }

        var addedRolesResult = await _userManager.AddToRolesAsync(user, new List<string> { Auth.Authorization.Roles.Roles.Admin });
        if (!addedRolesResult.Succeeded)
        {
            ModelState.TryAddModelStateErrors(addedRolesResult);
            return Page();
        }

        return RedirectToPage("/Admin/Initialize/Configuration");
    }
}
