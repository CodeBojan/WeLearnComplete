using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Reflection;
using WeLearn.Api.Data.Seeding;
using WeLearn.Api.Extensions;
using WeLearn.Auth.Extensions;
using WeLearn.Auth.SwaggerGen.OperationFilters;
using WeLearn.Data.Extensions;
using WeLearn.Data.Persistence;
using WeLearn.Importers.Extensions;
using WeLearn.Shared.Extensions.Logging;
using WeLearn.Shared.Extensions.WebHostEnvironmentExtensions;
using Swashbuckle.AspNetCore.JsonMultipartFormDataSupport.Extensions;
using Swashbuckle.AspNetCore.JsonMultipartFormDataSupport.Integrations;

const string authority = "https://localhost:7230"; // TODO read from config

var builder = WebApplication.CreateBuilder(args);

builder.AddLogging();

// Add services to the container.

ConfigureServices(builder);

var app = builder.Build();

Configure(app);

SeedData.EnsureSeedData(app);

app.Run();

static void ConfigureServices(WebApplicationBuilder builder)
{
    var services = builder.Services;
    var configuration = builder.Configuration;

    services.AddControllers();

    // Swagger
    services.AddEndpointsApiExplorer();

    services.AddSwaggerGen(options =>
    {
        options.SwaggerDoc("v1",
        new OpenApiInfo
        {
            Title = "WeLearn API",
            Version = "v1"
        });

        var jwtSecurityScheme = new OpenApiSecurityScheme()
        {
            BearerFormat = "JWT",
            Type = SecuritySchemeType.Http,
            In = ParameterLocation.Header,
            Scheme = JwtBearerDefaults.AuthenticationScheme,
            Reference = new OpenApiReference
            {
                Id = JwtBearerDefaults.AuthenticationScheme,
                Type = ReferenceType.SecurityScheme
            },
            Description = "JWT Bearer authorization. Insert just the JWT token from the Authorization header."
        };

        options.AddSecurityDefinition(
               name: JwtBearerDefaults.AuthenticationScheme,
               securityScheme: jwtSecurityScheme);

        options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        jwtSecurityScheme, Array.Empty<string>()
                    }
            });

        options.OperationFilter<SwaggerJsonIgnoreFilter>();

        var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        options.IncludeXmlComments(xmlPath);
    });

    services.AddHttpLogging(logging =>
    {
        logging.LoggingFields = Microsoft.AspNetCore.HttpLogging.HttpLoggingFields.None;
    });

    services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddIdentityServerAuthentication(authority);

    services.AddAuthorization(options =>
    {
        options.AddAuthorizationPolicies();
    });

    services.AddAuthorizationHandlers();

    services.AddApiServices(configuration);

    services.AddCors(options =>
    {
        options.AddWeLearnCors(configuration);
    });
}

static void Configure(WebApplication app)
{
    var configuration = app.Configuration;
    app.UseRouting();
    app.UseCors();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment() || app.Environment.IsLocal())
    {
        Log.Logger.Information($"Configuration:{Environment.NewLine}{(configuration as IConfigurationRoot).GetDebugView()}");

        app.UseHttpLogging();
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    if (configuration.GetSection("Authentication:Enabled").Get<bool>())
    {
        // TODO
        Log.Logger.Information("Authentication enabled");
        app.UseAuthentication();
    }
    if (configuration.GetSection("Authorization:Enabled").Get<bool>())
    {
        // TODO
        Log.Logger.Information("Authorization enabled");
        app.UseAuthorization();
    }

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();
}