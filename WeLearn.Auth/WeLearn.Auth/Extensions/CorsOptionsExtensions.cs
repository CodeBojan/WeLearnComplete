using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeLearn.Auth.Extensions;

public static class CorsOptionsExtensions
{
    public static CorsOptions AddWeLearnCors(this CorsOptions options, IConfiguration configuration)
    {
        options.AddDefaultPolicy(policy =>
        {
            policy.WithOrigins(configuration.GetSection("Cors:AllowedOrigins").Get<string[]>())
                .AllowAnyHeader()
                .AllowAnyMethod();
        });

        return options;
    }
}
