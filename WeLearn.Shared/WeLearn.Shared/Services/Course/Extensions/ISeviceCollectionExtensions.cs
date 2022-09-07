using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace WeLearn.Shared.Services.Course.Extensions;

public static class ISeviceCollectionExtensions
{
    public static IServiceCollection AddCourseServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<ICourseService, CourseService>();

        return services;
    }
}
