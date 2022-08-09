namespace WeLearn.IdentityServer.Services.Users;

public interface IUserApprovalService
{
    Task ApproveUserAsync(string userId);
}