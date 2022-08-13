using Microsoft.EntityFrameworkCore;
using Serilog;
using WeLearn.Api.Extensions;
using WeLearn.Data.Extensions;
using WeLearn.Data.Persistence;
using WeLearn.Importers.Extensions;
using WeLearn.Shared.Extensions.Logging;
using WeLearn.Shared.Extensions.WebHostEnvironmentExtensions;

var builder = WebApplication.CreateBuilder(args);

builder.AddLogging();

// Add services to the container.

ConfigureServices(builder);

var app = builder.Build();

Configure(app);

app.Run();

static void ConfigureServices(WebApplicationBuilder builder)
{
    var services = builder.Services;
    var configuration = builder.Configuration;

    services.AddControllers();

    // Swagger
    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen();

    services.AddHttpLogging(logging =>
    {
        logging.LoggingFields = Microsoft.AspNetCore.HttpLogging.HttpLoggingFields.None;
    });

    services.AddWeLearnDbContext(configuration.GetConnectionString("DefaultConnection"));

    services.AddWeLearnImporterServices(configuration);
}

static void Configure(WebApplication app)
{
    var configuration = app.Configuration;

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
        app.UseAuthentication();
    }
    if (configuration.GetSection("Authorization:Enabled").Get<bool>())
    {
        // TODO
        app.UseAuthorization();
    }

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();
}