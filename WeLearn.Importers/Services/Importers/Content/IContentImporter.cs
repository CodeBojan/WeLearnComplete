namespace WeLearn.Importers.Services.Importers.Content;

public interface IContentImporter
{
    // TODO add IsEnabled
    public string Name { get; }
    bool IsFinished { get; }
    Task ImportNextAsync(CancellationToken cancellationToken);
    void Reset();
}