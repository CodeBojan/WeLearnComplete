using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WeLearn.Auth.Authorization.Mvc.Filters;

namespace WeLearn.IdentityServer.Pages.Admin.Initialize;

[AllowAnonymous]
[ServiceFilter(typeof(IpWhitelistFilter<IpWhitelistSettings>))]
public class IndexModel : PageModel
{


    public async Task<IActionResult> OnGet()
    {
        return Page();
    }
}
