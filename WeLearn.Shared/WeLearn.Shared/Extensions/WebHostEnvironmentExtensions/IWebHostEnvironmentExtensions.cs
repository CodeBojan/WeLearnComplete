using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace WeLearn.Shared.Extensions.WebHostEnvironmentExtensions;

public static class IWebHostEnvironmentExtensions
{
    public static bool IsLocal(this IWebHostEnvironment env)
    {
        return env.IsEnvironment("Local");
    }
}
