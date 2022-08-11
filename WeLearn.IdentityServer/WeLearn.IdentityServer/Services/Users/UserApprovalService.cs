using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WeLearn.Auth.Authorization.Roles;
using WeLearn.Data.Models;
using WeLearn.Data.Persistence;
using WeLearn.IdentityServer.Exceptions;

namespace WeLearn.IdentityServer.Services.Users;

public class UserApprovalService : IUserApprovalService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ApplicationDbContext _dbContext;

    public UserApprovalService(
        ApplicationDbContext dbContext,
        UserManager<ApplicationUser> userManager)
    {
        _dbContext = dbContext;
        _userManager = userManager;
    }

    public async Task ApproveUserAsync(string userId)
    {
        var guid = new Guid(userId);
        var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == guid);
        if (user is null)
            throw new UserNotFoundException();

        user.Approved = true;

        var addToRoleResult = await _userManager.AddToRoleAsync(user, Roles.User);

        await _dbContext.SaveChangesAsync();
    }
}
