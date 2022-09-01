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

            var systemNames = new List<Tuple<string, string>>() { new Tuple<string, string>(Constants.NoticeBoardSystemName, "EFEE.ETF"), new Tuple<string, string>(Importers.Services.Importers.FacultySite.Constants.FacultySystemName, "ETF.UNIBL") };
            foreach (var systemName in systemNames)
                InitializeExternalSystem(logger, dbContext, systemName.Item1, systemName.Item2);

            dbContext.SaveChanges();
        }
    }

    private static void InitializeExternalSystem(ILogger<SeedData> logger, ApplicationDbContext dbContext, string name, string? friendlyName)
    {
        var noticeBoardSystem = dbContext.ExternalSystems.FirstOrDefault(es => es.Name == name);
        if (noticeBoardSystem is null)
        {
            noticeBoardSystem = new ExternalSystem(name, friendlyName);
            dbContext.ExternalSystems.Add(noticeBoardSystem);
            logger.LogInformation($"Added {name} to ExternalSystems");
        }
    }
}