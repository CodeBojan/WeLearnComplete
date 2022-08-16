using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WeLearn.Auth.Authorization.Mvc.Filters;

namespace WeLearn.Auth.Extensions;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddIpWhitelistFilter<TOptions>(this IServiceCollection services, IConfiguration configuration)
        where TOptions : IpWhitelistSettings
    {
        var section = configuration.GetSection(typeof(TOptions).Name);
        services.Configure<TOptions>(section);
        services.AddScoped<IpWhitelistFilter<TOptions>>();

        return services;
    }
}
