namespace WeLearn.Importers.Services.HostedServices;

public class BackgroundContentImporterConsumerServiceSettings
{
    public long PreImportTimeoutMillis { get; set; }
    public long PostImportTimeoutMillis { get; set; }

    public TimeSpan PreImportTimeout => TimeSpan.FromMilliseconds(PreImportTimeoutMillis);
    public TimeSpan PostImportTimeout => TimeSpan.FromMilliseconds(PostImportTimeoutMillis);
}