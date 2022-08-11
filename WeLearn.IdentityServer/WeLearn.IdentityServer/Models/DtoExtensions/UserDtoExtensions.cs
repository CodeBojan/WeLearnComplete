using System.Linq.Expressions;
using WeLearn.Data.Models;
using WeLearn.IdentityServer.Pages.Admin.Users;

namespace WeLearn.IdentityServer.Models.DtoExtensions;

public static class UserQueryableExtensions
{
    public static IQueryable<UserListItemDto> MapToListItemDto(this IQueryable<ApplicationUser> users)
    {
        return users.Select(u => new UserListItemDto { Id = u.Id.ToString(), Username = u.UserName, Email = u.Email, Approved = u.Approved, CreatedDate = u.CreatedDate });
    }
}
