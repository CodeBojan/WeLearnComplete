using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeLearn.Shared.Services.StudyYear.Extensions;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddStudyYearServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IStudyYearService, StudyYearService>();

        return services;
    }
}
