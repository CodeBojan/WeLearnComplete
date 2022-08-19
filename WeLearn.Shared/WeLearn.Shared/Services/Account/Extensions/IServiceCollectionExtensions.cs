using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeLearn.Shared.Services.Account.Extensions;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddAccountServices(this IServiceCollection services)
    {
        services.AddScoped<IAccountStore, AccountStore>();
        return services;
    }
}
