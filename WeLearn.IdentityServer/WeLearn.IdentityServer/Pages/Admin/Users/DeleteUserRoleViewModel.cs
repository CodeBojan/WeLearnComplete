using System.ComponentModel.DataAnnotations;

namespace WeLearn.IdentityServer.Pages.Admin.Users;

public class DeleteUserRoleViewModel
{
    [Required]
    public string UserId { get; set; }
    [Required]
    public string ClaimType { get; set; }
    [Required]
    public string ClaimValue { get; set; }
}
