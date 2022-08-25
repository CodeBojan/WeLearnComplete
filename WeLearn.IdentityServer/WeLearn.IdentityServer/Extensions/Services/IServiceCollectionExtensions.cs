using Duende.IdentityServer.Stores;
using WeLearn.Auth.Authorization.Mvc.Filters;
using WeLearn.Auth.Extensions;
using WeLearn.IdentityServer.Configuration.Providers;
using WeLearn.IdentityServer.Configuration.Services.Register;
using WeLearn.IdentityServer.Services.Identity;
using WeLearn.IdentityServer.Services.Identity.Grants;
using WeLearn.IdentityServer.Services.Register;
using WeLearn.IdentityServer.Services.Users;
using WeLearn.Shared.Services.Account;

namespace WeLearn.IdentityServer.Extensions.Services;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddConfigurationServices(this IServiceCollection services, ConfigurationManager configuration)
    {
        // TODO read from IConfiguration which needs to be in its own, temporary, servicecollection
        configuration.AddJsonBackedInMemory(services, null, "I:\\downloads\\welearn_data\\configuration.json", optional: true, reloadOnChange: true);

        return services;
    }

    public static IServiceCollection AddUserApprovalValidationServices(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.Configure<UserApprovalServiceSettings>(configuration.GetSection(UserApprovalServiceSettings
            .SectionName));
        services.AddScoped<IUserApprovalValidatorService, UserApprovalValidatorService>();

        return services;
    }

    public static IServiceCollection AddUserApprovalServices(this IServiceCollection services)
    {
        services.AddScoped<IUserApprovalService, UserApprovalService>();

        return services;
    }

    public static IServiceCollection AddDbPersistedGrantService(this IServiceCollection services)
    {
        services.AddTransient<IPersistedGrantStore, DbPersistedGrantStoreService>();

        return services;
    }

    public static IServiceCollection AddWeLearnIdentityServerServices(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.AddConfigurationServices(configuration);
        services.AddUserApprovalValidationServices(configuration);
        services.AddUserApprovalServices();
        services.AddDbPersistedGrantService();

        services.AddScoped<WeLearnUserManager>();

        services.AddIpWhitelistFilter<IpWhitelistSettings>(configuration);
        services.AddAuthorizationHandlers();

        return services;
    }
}
