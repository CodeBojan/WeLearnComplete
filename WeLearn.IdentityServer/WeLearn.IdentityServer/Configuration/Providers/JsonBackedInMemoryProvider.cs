using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using System.Text;
using System.Text.Json;

namespace WeLearn.IdentityServer.Configuration.Providers;

public class JsonBackedInMemoryConfigurationProvider : FileConfigurationProvider
{
    public JsonBackedInMemoryConfigurationProvider(JsonBackedInMemoryConfigurationSource source) : base(source)
    {
    }

    public override void Load(Stream stream)
    {
        Data = JsonSerializer.Deserialize<Dictionary<string, string>>(stream);
    }

    internal IDictionary<string, string> KeyPairs => Data;
}
