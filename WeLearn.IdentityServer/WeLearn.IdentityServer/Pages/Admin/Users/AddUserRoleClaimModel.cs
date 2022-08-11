using System.ComponentModel.DataAnnotations;

namespace WeLearn.IdentityServer.Pages.Admin.Users;

public class AddUserRoleClaimModel
{
    [Required]
    public string UserId { get; set; }
    [Required]
    public string ClaimType { get; set; }
    [Required]
    [RegularExpression("^[{]?[0-9a-fA-F]{8}-([0-9a-fA-F]{4}-){3}[0-9a-fA-F]{12}[}]?$", ErrorMessage = "The specified entity ID must be in the form of a GUID")]
    public string EntityId { get; set; }
}
