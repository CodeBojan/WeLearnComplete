using System.ComponentModel.DataAnnotations;

namespace WeLearn.IdentityServer.Pages.Admin.Users;

public class AddUserRoleClaimModel
{
    [Required]
    public string UserId { get; set; }
    [Required]
    public string ClaimType { get; set; }
    [Required]
    public string EntityId { get; set; }
}
