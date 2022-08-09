using WeLearn.IdentityServer.Pages.Admin.Configuration;

namespace WeLearn.IdentityServer.Configuration.Providers;

public interface IInMemoryConfigurationSource
{
    void Set(string key, string value);
    ConfigurationPair[] GetConfigurationPairs();
    void Delete(string key);
}