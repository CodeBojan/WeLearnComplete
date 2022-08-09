using System.ComponentModel.DataAnnotations;

namespace WeLearn.IdentityServer.Pages.Admin.Configuration;

public class ConfigurationPair
{
    [Required]
    [StringLength(maximumLength: 200, MinimumLength = 3)]
    public string Key { get; set; }
    public string Value { get; set; }
}
