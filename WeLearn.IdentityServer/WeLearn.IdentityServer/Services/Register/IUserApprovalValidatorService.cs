using Microsoft.AspNetCore.Identity;

namespace WeLearn.IdentityServer.Services.Register;

public interface IUserApprovalValidatorService
{
    Task<IdentityResult> GetUserEmailApprovalAsync(string email);
}