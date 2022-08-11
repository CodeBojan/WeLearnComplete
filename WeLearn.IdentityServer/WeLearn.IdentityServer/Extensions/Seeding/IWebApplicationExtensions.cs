using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using WeLearn.Auth.Authorization.Roles;
using WeLearn.Data.Persistence;

namespace WeLearn.IdentityServer.Extensions.Seeding;

public static class IWebApplicationExtensions
{
    private const string AdminRole = Roles.Admin;
    private const string UserRole = Roles.User;

    // TODO extract to Auth package


    public static WebApplication UseWeLearnSeeding(this WebApplication app)
    {
        var services = app.Services;
        using var scope = services.CreateScope();
        var serviceProvider = scope.ServiceProvider;

        // TODO log
        var dbContext = serviceProvider.GetRequiredService<ApplicationDbContext>();

        var adminRole = dbContext.Roles.FirstOrDefault(r => r.NormalizedName == AdminRole.ToUpper());
        if (adminRole is null)
        {
            adminRole = new IdentityRole<Guid>(AdminRole) { NormalizedName = AdminRole.ToUpper() };
            dbContext.Roles.Add(adminRole);
        }

        var userRole = dbContext.Roles.FirstOrDefault(r => r.NormalizedName == UserRole.ToUpper());
        if (userRole is null)
        {
            userRole = new IdentityRole<Guid>(UserRole) { NormalizedName = UserRole.ToUpper() };
            dbContext.Roles.Add(userRole);
        }

        //var adminUser = dbContext.UserRoles.FirstOrDefault(ur => ur.RoleId == adminRole.Id);
        //if(adminUser is null)
        //{

        //}

        dbContext.SaveChanges();

        return app;
    }
}
