namespace WeLearn.Api.Services.CourseMaterialUploadRequest.Extensions;

public static class ISeviceCollectionExtensions
{
    public static IServiceCollection AddCourseMaterialUploadRequestServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<ICourseMaterialUploadRequestService, CourseMaterialUploadRequestService>();

        return services;
    }
}