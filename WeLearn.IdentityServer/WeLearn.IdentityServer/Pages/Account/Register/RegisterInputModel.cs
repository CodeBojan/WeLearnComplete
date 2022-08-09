using System.ComponentModel.DataAnnotations;

namespace WeLearn.IdentityServer.Pages.Account.Register;

public class RegisterInputModel
{
    [Required]
    public string Username { get; set; }
    [Required]
    public string Email { get; set; }
    [Required]
    public string Password { get; set; }
    [Compare(nameof(Password))]
    public string ConfirmPassword { get; set; }
    [Required]
    [DataType(DataType.Date)]
    public DateTime BirthDate { get; set; }

    public string Button { get; set; }
    public string ReturnUrl { get; set; }
}
