using System;
using System.Threading.Tasks;
using Duende.IdentityServer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using WeLearn.IdentityServer.Configuration.Auth.Logout;

namespace WeLearn.IdentityServer.Pages.Logout;

[SecurityHeaders]
[AllowAnonymous]
public class LoggedOut : PageModel
{
    private readonly IIdentityServerInteractionService _interactionService;
    private readonly IOptionsSnapshot<LogoutSettings> _optionsSnapshot;

    public LoggedOutViewModel View { get; set; }

    public LoggedOut(IIdentityServerInteractionService interactionService, IOptionsSnapshot<LogoutSettings> optionsSnapshot)
    {
        _interactionService = interactionService;
        _optionsSnapshot = optionsSnapshot;
    }

    public async Task OnGet(string logoutId)
    {
        // get context information (client name, post logout redirect URI and iframe for federated signout)
        var logout = await _interactionService.GetLogoutContextAsync(logoutId);

        View = new LoggedOutViewModel
        {
            AutomaticRedirectAfterSignOut = _optionsSnapshot.Value.AutomaticRedirectAfterSignOut,
            PostLogoutRedirectUri = logout?.PostLogoutRedirectUri,
            ClientName = String.IsNullOrEmpty(logout?.ClientName) ? logout?.ClientId : logout?.ClientName,
            SignOutIframeUrl = logout?.SignOutIFrameUrl
        };
    }
}