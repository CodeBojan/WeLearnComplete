using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeLearn.Importers.Services.System;

namespace WeLearn.Importers.Services;

public class BackgroundContentImporterConsumerService : BackgroundService, IBackgroundContentImporterConsumerService
{
    private BackgroundContentImporterConsumerServiceSettings settings;
    private readonly ILogger _logger;
    private readonly IServiceProvider _serviceProvider;
    private IEnumerable<ISystemImporter> systemImporters = new List<ISystemImporter>();

    public BackgroundContentImporterConsumerService(ILogger<BackgroundContentImporterConsumerService> logger,
        IServiceProvider serviceProvider,
       IOptionsMonitor<BackgroundContentImporterConsumerServiceSettings> settings)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
        this.settings = settings.CurrentValue;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("{@ServiceName} is starting", nameof(BackgroundContentImporterConsumerService));
        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation("{@ServiceName} is sleeping for {@Timeout}", nameof(BackgroundContentImporterConsumerService), settings.PreImportTimeout);
            await Task.Delay(settings.PreImportTimeout, stoppingToken);


            _logger.LogInformation("{@ServiceName} is consuming", nameof
(BackgroundContentImporterConsumerService));
            using var scope = _serviceProvider.CreateScope();
            var serviceProvider = scope.ServiceProvider;

            systemImporters = serviceProvider.GetRequiredService<IEnumerable<ISystemImporter>>();

            if (!systemImporters.Any())
            {
                _logger.LogInformation("{@ServiceName} has no system importers", nameof(BackgroundContentImporterConsumerService));
                continue;
            }

            // TODO use importer reset
            // TODO add timeouts
            // TODO parallelization options
            foreach (var systemImporter in systemImporters)
            {
                var contentImporters = systemImporter.ContentImporters;
                if (!(contentImporters?.Any() ?? false))
                {
                    _logger.LogInformation("{@SystemImporter} has no Content Importer instances", systemImporter.Name);
                    continue;
                }

                foreach (var contentImporter in contentImporters)
                {
                    _logger.LogInformation("{@ContentImporter} beginning importing", contentImporter.Name);
                    while (!contentImporter.IsFinished)
                    {
                        _logger.LogInformation("{@ContentImporter} importing batch", contentImporter.Name);

                        await contentImporter.ImportNextAsync(stoppingToken);

                        _logger.LogInformation("{@ContentImporter} imported batch", contentImporter.Name);
                    }
                    _logger.LogInformation("{@ContentImporter} finished", contentImporter.Name);
                }
            }
        }
    }

    Task IBackgroundContentImporterConsumerService.ImportAllAsync(CancellationToken cancellationToken)
    {
        return ExecuteAsync(cancellationToken);
    }
}
