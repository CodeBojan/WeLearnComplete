using System.ComponentModel.DataAnnotations;

namespace WeLearn.IdentityServer.Pages.Admin.Initialize;

public class AdminCredentialsInputModel
{
    [Required]
    public string Email { get; set; }
    [Required]
    [StringLength(20, MinimumLength = 3)]
    public string Username { get; set; }
    [Required]
    public string Password { get; set; }
    [Required]
    [Compare(nameof(Password))]
    public string ConfirmPassword { get; set; }
}
