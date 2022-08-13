using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeLearn.Shared.Services.CourseTitleCleaner;
using WeLearn.Shared.Services.StringMatcher;

namespace WeLearn.Shared.Extensions.Services;

public static class ISeviceCollectionExtensions
{
    public static IServiceCollection AddSharedServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<StringMatcherServiceSettings>(configuration.GetSection(nameof(StringMatcherServiceSettings)));
        services.AddSingleton<IStringMatcherService, StringMatcherService>();

        services.Configure<CourseTitleCleanerServiceSettings>(configuration.GetSection(nameof(CourseTitleCleanerServiceSettings)));
        services.AddSingleton<ICourseTitleCleanerService, CourseTitleCleanerService>();

        return services;
    }
}
