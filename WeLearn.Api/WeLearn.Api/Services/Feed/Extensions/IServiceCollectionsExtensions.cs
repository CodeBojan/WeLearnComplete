namespace WeLearn.Api.Services.Feed.Extensions;

public static class IServiceCollectionsExtensions
{
    public static IServiceCollection AddFeedServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IFeedService, FeedService>();

        return services;
    }
}
