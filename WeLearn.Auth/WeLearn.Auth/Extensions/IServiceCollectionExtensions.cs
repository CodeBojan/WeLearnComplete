using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WeLearn.Auth.Authorization.Handlers;
using WeLearn.Auth.Authorization.Mvc.Filters;
using WeLearn.Data.Models;

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

    public static IServiceCollection AddAuthorizationHandlers(this IServiceCollection services)
    {
        services.AddSingleton<IAuthorizationHandler, StudyYearAdminAuthorizationHandler>();
        services.AddSingleton<IAuthorizationHandler, CourseAdminClaimAuthorizationHandler>();
        services.AddSingleton<IAuthorizationHandler, SystemAdminStudyYearAuthorizationHandler>();
        services.AddSingleton<IAuthorizationHandler, SystemAdminCourseAuthorizationHandler>();

        return services;
    }
}
