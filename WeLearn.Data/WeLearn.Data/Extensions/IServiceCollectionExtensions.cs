using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WeLearn.Data.Persistence;
using WeLearn.Importers.Services.File;

namespace WeLearn.Data.Extensions;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddWeLearnDbContext(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(connectionString));

        return services;
    }

    public static IServiceCollection AddWeLearnFileSystemPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<FileSystemPersistenceServiceSettings>(configuration.GetSection(nameof(FileSystemPersistenceServiceSettings)));
        services.AddScoped<IFileSystemPersistenceService,
            FileSystemPersistenceService>();

        return services;
    }
}
