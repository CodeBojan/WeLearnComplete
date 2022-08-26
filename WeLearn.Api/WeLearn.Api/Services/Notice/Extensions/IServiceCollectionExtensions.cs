namespace WeLearn.Api.Services.Notice.Extensions;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddNoticeServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<INoticeService, NoticeService>();

        return services;
    }
}
