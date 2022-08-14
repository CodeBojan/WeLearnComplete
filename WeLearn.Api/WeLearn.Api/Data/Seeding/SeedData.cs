using Microsoft.EntityFrameworkCore;
using WeLearn.Data.Models;
using WeLearn.Data.Persistence;
using WeLearn.Importers.Services.Importers.NoticeBoard;

namespace WeLearn.Api.Data.Seeding;

public class SeedData
{
    public static void EnsureSeedData(WebApplication app)
    {
        using (var scope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope())
        {
            var serviceProvider = scope.ServiceProvider;
            var logger = serviceProvider.GetRequiredService<ILogger<SeedData>>();
            var dbContext = serviceProvider.GetRequiredService<ApplicationDbContext>();

            logger.LogDebug("Migrating database");
            dbContext.Database.Migrate();

            const string noticeBoardSystemName = Constants.NoticeBoardSystemName;
            var noticeBoardSystem = dbContext.ExternalSystems.FirstOrDefault(es => es.Name == noticeBoardSystemName);
            if (noticeBoardSystem is null)
            {
                noticeBoardSystem = new ExternalSystem(noticeBoardSystemName);
                dbContext.ExternalSystems.Add(noticeBoardSystem);
                logger.LogInformation($"Added {noticeBoardSystemName} to ExternalSystems");
            }

            dbContext.SaveChanges();
        }
    }
}