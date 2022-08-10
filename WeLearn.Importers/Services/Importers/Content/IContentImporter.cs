namespace WeLearn.Importers.Services.Importers.Content;

public interface IContentImporter
{
    public string Name { get; }
    bool IsFinished { get; }
    Task ImportNextAsync();
    void Reset();
}