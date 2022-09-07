using Microsoft.AspNetCore.Identity;

namespace WeLearn.IdentityServer.Services.Identity;

public interface IRoleManager
{
    Task<IdentityResult> AddCourseAdminRoleAsync(Guid userId, Guid courseId);
    Task<IdentityResult> AddStudyYearAdminRoleAsync(Guid userId, Guid studyYearId);
    Task<IdentityResult> RemoveCourseAdminRoleAsync(Guid userId, Guid courseId);
    Task<IdentityResult> RemoveStudyYearAdminRoleAsync(Guid userId, Guid studyYearId);
}