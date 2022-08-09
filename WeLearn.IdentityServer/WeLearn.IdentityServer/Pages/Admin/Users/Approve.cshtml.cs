using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using WeLearn.IdentityServer.Exceptions;
using WeLearn.IdentityServer.Services.Users;

namespace WeLearn.IdentityServer.Pages.Admin.Users;

public class ApproveModel : PageModel
{
    private readonly IUserApprovalService _userApprovalService;

    public ApproveModel(IUserApprovalService userApprovalService)
    {
        _userApprovalService = userApprovalService;
    }

    [Required]
    [BindProperty]
    public string UserId { get; set; }

    public async Task<IActionResult> OnGet()
    {
        return Page();
    }

    public async Task<IActionResult> OnPostApprove()
    {
        try
        {
            await _userApprovalService.ApproveUserAsync(UserId);
        }
        catch (UserNotFoundException)
        {
            // TODO test
            ModelState.AddModelError("User", "User not found.");
        }

        return RedirectToPage("/Admin/Users/Index");
    }
}
