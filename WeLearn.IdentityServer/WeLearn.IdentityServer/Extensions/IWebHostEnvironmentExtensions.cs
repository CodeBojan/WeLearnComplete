﻿namespace WeLearn.IdentityServer.Extensions;

public static class IWebHostEnvironmentExtensions
{
    public static bool IsLocal(this IWebHostEnvironment env)
    {
        return env.IsEnvironment("Local");
    }
}
