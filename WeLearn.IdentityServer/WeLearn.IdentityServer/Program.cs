using WeLearn.IdentityServer;
using Serilog;
using WeLearn.IdentityServer.Data.Seeding;
using WeLearn.Shared.Extensions.Logging;
using WeLearn.Shared.Extensions.WebHostEnvironmentExtensions;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();

Log.Information("Starting up");

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.AddLogging();

    var app = builder
        .ConfigureServices()
        .ConfigurePipeline();

    var configuration = app.Configuration;

    if (app.Environment.IsDevelopment() || app.Environment.IsLocal())
    {
        Log.Information("Application configured by {Configuration}", (configuration as IConfigurationRoot).GetDebugView());
    }

    // this seeding is only for the template to bootstrap the DB and users.
    // in production you will likely want a different approach.
    if (args.Contains("/seed"))
    {
        Log.Information("Seeding database...");
        SeedData.EnsureSeedData(app);
        Log.Information("Done seeding database. Exiting.");
        return;
    }

    app.Run();
}
catch (Exception ex) when (ex.GetType().Name is not "StopTheHostException") // https://github.com/dotnet/runtime/issues/60600
{
    Log.Fatal(ex, "Unhandled exception");
}
finally
{
    Log.Information("Shut down complete");
    Log.CloseAndFlush();
}