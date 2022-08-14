using System.Text.Json.Serialization;

namespace WeLearn.Importers.Services;

public class BackgroundContentImporterConsumerServiceSettings
{
    public long PreImportTimeoutMillis { get; set; }
    public long PostImportTimeoutMillis { get; set; }

    public TimeSpan PreImportTimeout => TimeSpan.FromMilliseconds(PreImportTimeoutMillis);
    public TimeSpan PostImportTimeout => TimeSpan.FromMilliseconds(PostImportTimeoutMillis);
}