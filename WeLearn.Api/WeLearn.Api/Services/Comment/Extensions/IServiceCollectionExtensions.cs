namespace WeLearn.Api.Services.Comment.Extensions;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddCommentServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<ICommentService, CommentService>();

        return services;
    }
}
