using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace WeLearn.IdentityServer.Extensions.ModelState;

public static class ModelStateExtensions
{
    public static void TryAddModelStateErrors(this ModelStateDictionary modelState, IdentityResult addClaimResult)
    {
        if (addClaimResult.Errors.Any())
            foreach (var error in addClaimResult.Errors)
            {
                modelState.AddModelError(error.Code, error.Description);
            }
    }
}
