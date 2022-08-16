using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeLearn.Shared.Services.CourseTitleCleaner.Extensions;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddCourseTitleCleanerServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<CourseTitleCleanerServiceSettings>(configuration.GetSection(nameof(CourseTitleCleanerServiceSettings)));
        services.AddSingleton<ICourseTitleCleanerService, CourseTitleCleanerService>();

        return services;
    }
}
