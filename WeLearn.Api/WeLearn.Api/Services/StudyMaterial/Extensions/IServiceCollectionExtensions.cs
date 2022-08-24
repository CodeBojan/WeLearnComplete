namespace WeLearn.Api.Services.StudyMaterial.Extensions;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddStudyMaterialServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IStudyMaterialService, StudyMaterialService>();

        return services;
    }
}
