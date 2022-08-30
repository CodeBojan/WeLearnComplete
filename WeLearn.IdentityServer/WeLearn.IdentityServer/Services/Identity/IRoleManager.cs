using Microsoft.AspNetCore.Identity;

namespace WeLearn.IdentityServer.Services.Identity;

public interface IRoleManager
{
    Task<IdentityResult> AddStudyYearAdminRoleAsync(Guid userId, Guid studyYearId);
    Task<IdentityResult> RemoveStudyYearAdminRoleAsync(Guid userId, Guid studyYearId);
}