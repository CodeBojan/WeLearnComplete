using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeLearn.Shared.Services.CourseTitleCleaner;
using WeLearn.Shared.Services.CourseTitleCleaner.Extensions;
using WeLearn.Shared.Services.Credentials.Extensions;
using WeLearn.Shared.Services.StringMatcher;
using WeLearn.Shared.Services.StringMatcher.Extensions;
using WeLearn.Shared.Services.StudyYear.Extensions;

namespace WeLearn.Shared.Extensions.Services;

public static class ISeviceCollectionExtensions
{
    public static IServiceCollection AddSharedServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddCredentialsServices(configuration);

        services.AddStringMatcherServices(configuration);

        services.AddCourseTitleCleanerServices(configuration);

        services.AddStudyYearServices(configuration);

        return services;
    }
}
