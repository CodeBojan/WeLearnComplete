using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using WeLearn.IdentityServer.Configuration.Services.Register;

namespace WeLearn.IdentityServer.Services.Register;

public class UserApprovalValidatorService : IUserApprovalValidatorService
{
    private readonly UserApprovalServiceSettings settings;

    public UserApprovalValidatorService(IOptionsSnapshot<UserApprovalServiceSettings> userApprovalServiceSettingsOptions)
    {
        var userApprovalServiceSettings = userApprovalServiceSettingsOptions.Value;
        settings = userApprovalServiceSettings;
    }

    private string ApprovedDomain => settings.ApprovedDomain;
    private bool ApproveAll => settings.ApproveAll;

    public async Task<IdentityResult> GetUserEmailApprovalAsync(string email)
    {
        if (ApproveAll)
            return IdentityResult.Success;

        if (string.IsNullOrWhiteSpace(ApprovedDomain))
            return IdentityResult.Failed(new IdentityError { Code = "", Description = "Not approved." });

        var splitEmail = email.Split('@');
        var domain = splitEmail[1];
        if (domain == ApprovedDomain)
            return IdentityResult.Success;

        return IdentityResult.Failed(new IdentityError { Code = "", Description = "Not approved." });
    }
}
