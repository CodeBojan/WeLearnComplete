using System.Text.Json;
using WeLearn.IdentityServer.Pages.Admin.Configuration;

namespace WeLearn.IdentityServer.Configuration.Providers;

// TODO use DTOs instead and in the Razor page map between the page model and the service DTOs

public class JsonBackedInMemoryConfigurationSource : FileConfigurationSource, IInMemoryConfigurationSource
{
    private JsonBackedInMemoryConfigurationProvider provider;

    public override IConfigurationProvider Build(IConfigurationBuilder builder)
    {
        EnsureDefaults(builder);
        provider = new(this);
        return provider;
    }

    public Dictionary<string, string> ConfigurationDict => new(provider.KeyPairs ?? new Dictionary<string, string>());

    public void Set(string key, string value)
    {
        var currentDictionary = ConfigurationDict;

        if (value is null)
            currentDictionary.Remove(key);
        else
            currentDictionary[key] = value;

        var json = JsonSerializer.Serialize(currentDictionary);

        var fileInfo = FileProvider.GetFileInfo(Path);
        var path = fileInfo.PhysicalPath;
        File.WriteAllText(path, json);
    }

    ConfigurationPair[] IInMemoryConfigurationSource.GetConfigurationPairs()
    {
        return ConfigurationDict
            .Select(p => new ConfigurationPair { Key = p.Key, Value = p.Value })
            .ToArray();
    }

    public void Delete(string key)
    {
        Set(key, null);
    }
}
