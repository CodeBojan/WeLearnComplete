using WeLearn.Api.Services.FollowedCourse;
using WeLearn.Data.Extensions;
using WeLearn.Importers.Extensions;
using WeLearn.Shared.Extensions.Services;

namespace WeLearn.Api.Services.FollowedStudyYear.Extensions;

public static class IServiceCollectionsExtensions
{
    public static IServiceCollection AddFollowedStudyYearServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IFollowedStudyYearService, FollowedStudyYearService>();

        return services;
    }
}
