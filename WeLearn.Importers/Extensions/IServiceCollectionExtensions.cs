using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeLearn.Importers.Services;
using WeLearn.Importers.Services.Importers.FacultySite.Extensions;
using WeLearn.Importers.Services.Importers.NoticeBoard.Extensions;
using WeLearn.Importers.Services.Importers.NoticeBoard.System;
using WeLearn.Importers.Services.System;

namespace WeLearn.Importers.Extensions;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddWeLearnImporterServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<BackgroundContentImporterConsumerServiceSettings>(
             configuration.GetSection(
                 nameof(BackgroundContentImporterConsumerServiceSettings)));
        services.AddHostedService<BackgroundContentImporterConsumerService>();

        services.AddNoticeBoardServices(configuration);
        services.AddFacultySiteServices(configuration);

        return services;
    }
}
