using WeLearn.Api.Services.FollowedCourse.Extensions;
using WeLearn.Data.Extensions;
using WeLearn.Importers.Extensions;
using WeLearn.Shared.Extensions.Services;

namespace WeLearn.Api.Extensions;

public static class IServiceCollectionsExtensions
{
    public static IServiceCollection AddApiServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSharedServices(configuration);

        services.AddWeLearnDbContext(configuration.GetConnectionString("DefaultConnection"));
        services.AddWeLearnFileSystemPersistence(configuration);

        services.AddWeLearnImporterServices(configuration);

        return services;
    }

    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddFollowedCourseServices(configuration);

        return services;
    }
}
