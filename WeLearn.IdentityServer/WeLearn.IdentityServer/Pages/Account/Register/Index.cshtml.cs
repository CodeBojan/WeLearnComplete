using Duende.IdentityServer.Events;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;
using Duende.IdentityServer.Stores;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using WeLearn.Auth.Authorization.Roles;
using WeLearn.Data.Models;
using WeLearn.IdentityServer.Events.Register;
using WeLearn.IdentityServer.Services.Register;

namespace WeLearn.IdentityServer.Pages.Account.Register;

[SecurityHeaders]
[AllowAnonymous]
public class IndexModel : PageModel
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly IIdentityServerInteractionService _interaction;
    private readonly IClientStore _clientStore;
    private readonly IEventService _events;
    private readonly IAuthenticationSchemeProvider _schemeProvider;
    private readonly IIdentityProviderStore _identityProviderStore;
    private readonly IUserApprovalValidatorService _userApprovalService;
    public IndexModel(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        IIdentityServerInteractionService interaction,
        IClientStore clientStore,
        IEventService events,
        IAuthenticationSchemeProvider schemeProvider,
        IIdentityProviderStore identityProviderStore,
        IUserApprovalValidatorService userApprovalService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _interaction = interaction;
        _clientStore = clientStore;
        _events = events;
        _schemeProvider = schemeProvider;
        _identityProviderStore = identityProviderStore;
        _userApprovalService = userApprovalService;
    }

    public RegisterViewModel View { get; set; }

    [BindProperty]
    public RegisterInputModel Input { get; set; }

    public async Task<IActionResult> OnGet(string returnUrl)
    {
        await BuildModelAsync(returnUrl);

        return Page();
    }

    public async Task<IActionResult> OnPost()
    {
        // check if we are in the context of an authorization request
        var context = await _interaction.GetAuthorizationContextAsync(Input.ReturnUrl);

        if (Input.Button != "register")
        {
            if (context != null)
            {
                await _interaction.DenyAuthorizationAsync(context, AuthorizationError.AccessDenied);

                if (context.IsNativeClient())
                {
                    return this.LoadingPage(Input.ReturnUrl);
                }

                return Redirect(Input.ReturnUrl);
            }
            else
                return Redirect("~/");
        }

        if (ModelState.IsValid)
        {
            var emailApproval = await _userApprovalService.GetUserEmailApprovalAsync(Input.Email);
            bool approved = emailApproval.Succeeded;
            var user = new ApplicationUser(Input.Username, Input.Email, approved);

            var registerResult = await _userManager.CreateAsync(user, Input.Password);
            if (registerResult.Succeeded)
            {
                var addBirthResult = await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.DateOfBirth, Input.BirthDate.ToShortDateString()));
                await _events.RaiseAsync(new UserRegisterSuccessEvent(Input.Email, clientId: context?.Client.ClientId));
                // TODO check result
                // TODO create User for API
                var loginResult = await _signInManager.PasswordSignInAsync(user, Input.Password, false, lockoutOnFailure: false);
                await _events.RaiseAsync(new UserLoginSuccessEvent(user.UserName, user.Id, user.UserName, clientId: context?.Client.ClientId));
                // TODO move to options

                if (approved)
                {
                    var addToUserResult = await _userManager.AddToRoleAsync(user, Roles.User);
                }

                if (context != null)
                {
                    if (context.IsNativeClient())
                    {
                        // The client is native, so this change in how to
                        // return the response is for better UX for the end UserRole.
                        return this.LoadingPage(Input.ReturnUrl);
                    }

                    // we can trust model.ReturnUrl since GetAuthorizationContextAsync returned non-null
                    return Redirect(Input.ReturnUrl);
                }

                // request for a local page
                if (Url.IsLocalUrl(Input.ReturnUrl))
                {
                    return Redirect(Input.ReturnUrl);
                }
                else if (string.IsNullOrEmpty(Input.ReturnUrl))
                {
                    return Redirect("~/");
                }
                else
                {
                    // user might have clicked on a malicious link - should be logged
                    throw new Exception("invalid return URL");
                }
            }

            foreach (var error in registerResult.Errors)
                ModelState.AddModelError(string.Empty, error.Description); // TODO add for any error
        }

        await _events.RaiseAsync(new UserRegisterFailedEvent(Input.Email, clientId: context?.Client.ClientId));

        await BuildModelAsync(Input);
        return Page();
    }

    private async Task BuildModelAsync(RegisterInputModel input)
    {
        View = new RegisterViewModel { };

        Input = new RegisterInputModel { ReturnUrl = input.ReturnUrl };
    }

    private async Task BuildModelAsync(string returnUrl)
    {
        View = new RegisterViewModel { };

        Input = new RegisterInputModel { ReturnUrl = returnUrl };
    }
}
