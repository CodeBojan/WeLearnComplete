namespace WeLearn.IdentityServer.Configuration.Services.Register;

public class UserApprovalServiceSettings
{
    public const string SectionName = nameof(UserApprovalServiceSettings);

    public bool ApproveAll { get; set; }
    public string ApprovedDomain { get; set; }
}