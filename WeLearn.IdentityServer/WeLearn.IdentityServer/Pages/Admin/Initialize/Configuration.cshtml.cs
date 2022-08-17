using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using System.ComponentModel.DataAnnotations;
using WeLearn.IdentityServer.Configuration.Providers;
using WeLearn.IdentityServer.Configuration.Services.Register;

namespace WeLearn.IdentityServer.Pages.Admin.Initialize
{
    public class ConfigurationModel : PageModel
    {
        private readonly UserApprovalServiceSettings settings;
        private readonly IInMemoryConfigurationSource _configurationSource;
        public ConfigurationModel(
            IOptions<UserApprovalServiceSettings> options,
            IInMemoryConfigurationSource configurationSource)
        {
            settings = options.Value;
            _configurationSource = configurationSource;
        }

        [Required]
        [BindProperty]
        public bool ApproveAll { get; set; }
        [BindProperty]
        public string ApprovedDomain { get; set; }

        public async Task<IActionResult> OnGet()
        {
            ApproveAll = settings.ApproveAll;
            ApprovedDomain = settings.ApprovedDomain;
            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
                return Page();

            _configurationSource.Set($"{UserApprovalServiceSettings.SectionName}:{nameof(UserApprovalServiceSettings.ApproveAll)}", ApproveAll.ToString());
            _configurationSource.Set($"{UserApprovalServiceSettings.SectionName}:{nameof(UserApprovalServiceSettings.ApprovedDomain)}", ApprovedDomain);

            return RedirectToPage("/Admin/Index");
        }
    }
}
