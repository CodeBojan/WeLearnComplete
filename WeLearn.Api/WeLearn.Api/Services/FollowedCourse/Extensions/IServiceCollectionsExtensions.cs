namespace WeLearn.Api.Services.FollowedCourse.Extensions;

public static class IServiceCollectionsExtensions
{
    public static IServiceCollection AddFollowedCourseServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IFollowedCourseService, FollowedCourseService>();

        return services;
    }
}
