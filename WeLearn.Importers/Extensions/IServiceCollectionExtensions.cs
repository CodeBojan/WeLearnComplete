using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeLearn.Importers.Services;
using WeLearn.Importers.Services.Importers.NoticeBoard.Extensions;
using WeLearn.Importers.Services.Importers.NoticeBoard.System;
using WeLearn.Importers.Services.System;

namespace WeLearn.Importers.Extensions;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddWeLearnImporterServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHostedService<BackgroundContentImporterConsumerService>();

        // TODO move to API extension
        services.AddNoticeBoardServices(configuration);

        return services;
    }
}
