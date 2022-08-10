namespace WeLearn.Importers.Services
{
    public interface IBackgroundContentImporterConsumerService
    {
        Task ImportAllAsync(CancellationToken cancellationToken);
    }
}