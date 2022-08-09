using Microsoft.Extensions.FileProviders;

namespace WeLearn.IdentityServer.Configuration.Providers;

public static class JsonBackedInMemoryProviderExtensions
{
    public static IConfigurationBuilder AddJsonBackedInMemory(this IConfigurationBuilder builder, string path)
    {
        return AddJsonBackedInMemory(builder, provider: null, path: path, optional: false, reloadOnChange: false);
    }

    public static IConfigurationBuilder AddJsonBackedInMemory(this IConfigurationBuilder builder, string path, bool optional)
    {
        return AddJsonBackedInMemory(builder, provider: null, path: path, optional: optional, reloadOnChange: false);
    }

    public static IConfigurationBuilder AddJsonBackedInMemory(this IConfigurationBuilder builder, string path, bool optional, bool reloadOnChange)
    {
        return AddJsonBackedInMemory(builder, provider: null, path: path, optional: optional, reloadOnChange: reloadOnChange);
    }

    public static IConfigurationBuilder AddJsonBackedInMemory(this IConfigurationBuilder builder, IServiceCollection services, string path, bool optional, bool reloadOnChange)
    {
        return AddJsonBackedInMemory(builder, services, provider: null, path: path, optional: optional, reloadOnChange: reloadOnChange);
    }

    public static IConfigurationBuilder AddJsonBackedInMemory(this IConfigurationBuilder builder, IFileProvider provider, string path, bool optional, bool reloadOnChange)
    {
        JsonBackedInMemoryConfigurationSource source = CreateSource(ref provider, ref path, optional, reloadOnChange);
        builder.Add(source);

        return builder;
    }

    private static JsonBackedInMemoryConfigurationSource CreateSource(ref IFileProvider provider, ref string path, bool optional, bool reloadOnChange)
    {
        if (provider == null && Path.IsPathRooted(path))
        {
            provider = new PhysicalFileProvider(Path.GetDirectoryName(path));
            path = Path.GetFileName(path);
        }
        var source = new JsonBackedInMemoryConfigurationSource
        {
            FileProvider = provider,
            Path = path,
            Optional = optional,
            ReloadOnChange = reloadOnChange
        };
        return source;
    }

    public static IConfigurationBuilder AddJsonBackedInMemory(this IConfigurationBuilder builder, IServiceCollection services, IFileProvider provider, string path, bool optional, bool reloadOnChange)
    {
        var source = CreateSource(ref provider, ref path, optional, reloadOnChange);
        builder.Add(source);

        services.AddSingleton<IInMemoryConfigurationSource, JsonBackedInMemoryConfigurationSource>(sp => source);

        return builder;
    }
}
