using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeLearn.Shared.Services.Credentials.Extensions;

public static class IServiceCollectonExtensions
{
    public static IServiceCollection AddCredentialsServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<ICredentialsService, CredentialsService>();

        return services;
    }
}
