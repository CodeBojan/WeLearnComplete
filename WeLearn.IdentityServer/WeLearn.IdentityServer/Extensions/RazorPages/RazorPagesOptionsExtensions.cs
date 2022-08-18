using Microsoft.AspNetCore.Mvc.RazorPages;
using WeLearn.Auth.Policy;

namespace WeLearn.IdentityServer.Extensions.RazorPages;

public static class RazorPagesOptionsExtensions
{
    public static RazorPagesOptions ApplyAuthPolicies(this RazorPagesOptions options)
    {
        options.Conventions.AuthorizeFolder("/Admin", Policies.IsAdmin);

        return options;
    }
}
