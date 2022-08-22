namespace WeLearn.Api.Services.Document.Extensions;

public static class IServiceCollectionsExtensions
{
    public static IServiceCollection AddDocumentsServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IDocumentsService, DocumentsService>();

        return services;
    }
}
