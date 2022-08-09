using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WeLearn.IdentityServer.Configuration.Providers;

namespace WeLearn.IdentityServer.Pages.Admin.Configuration;

public class IndexModel : PageModel
{
    private readonly IInMemoryConfigurationSource _configurationSource;

    public IndexModel(IInMemoryConfigurationSource configurationSource)
    {
        _configurationSource = configurationSource;
    }

    public ConfigurationViewModel View { get; set; }

    [BindProperty]
    public ConfigurationPostModel? Post { get; set; }
    [BindProperty]
    public string? DeleteKey { get; set; }

    public IActionResult OnGet()
    {
        PrepareView();

        return Page();
    }

    private void PrepareView()
    {
        View = new ConfigurationViewModel { Configuration = _configurationSource.GetConfigurationPairs() };
    }

    public async Task<IActionResult> OnPostCreate()
    {
        if (!ModelState.IsValid)
        {
            PrepareView();
            return Page();
        }

        var key = Post.Key;
        var value = Post.Value;

        _configurationSource.Set(key, value);

        return RedirectToPage("Index");
    }

    public async Task<IActionResult> OnPostDelete()
    {
        var key = DeleteKey;
        if (key is null)
        {
            PrepareView();
            return Page();
        }

        _configurationSource.Delete(DeleteKey);
        PrepareView();

        return RedirectToPage("Index");
    }
}
