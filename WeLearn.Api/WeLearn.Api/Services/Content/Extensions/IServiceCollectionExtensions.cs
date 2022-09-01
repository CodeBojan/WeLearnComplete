namespace WeLearn.Api.Services.Content.Extensions;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddContentServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IContentService, ContentService>();

        return services;
    }
}
