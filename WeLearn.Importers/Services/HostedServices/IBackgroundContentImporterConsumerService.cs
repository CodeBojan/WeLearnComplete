namespace WeLearn.Importers.Services.HostedServices
{
    public interface IBackgroundContentImporterConsumerService
    {
        Task ImportAllAsync(CancellationToken cancellationToken);
    }
}