using Duende.IdentityServer;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;
using WeLearn.Data.Extensions;
using WeLearn.Data.Models;
using WeLearn.Data.Persistence;
using WeLearn.IdentityServer.Configuration.Auth.Google;
using WeLearn.IdentityServer.Extensions;
using WeLearn.IdentityServer.Extensions.Seeding;
using WeLearn.IdentityServer.Extensions.Services;
using WeLearn.IdentityServer.Services.Identity;

namespace WeLearn.IdentityServer;

internal static class HostingExtensions
{
    public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
    {
        var configuration = builder.Configuration;
        var environment = builder.Environment;
        var services = builder.Services;

        var mvcBuilder = services.AddRazorPages();
        if (environment.IsLocal())
            mvcBuilder.AddRazorRuntimeCompilation();
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        services.Configure<ForwardedHeadersOptions>(options =>
        {
            options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto | ForwardedHeaders.XForwardedHost;

            options.KnownNetworks.Clear();
            options.KnownProxies.Clear();
        });

        services.AddWeLearnDbContext(configuration.GetConnectionString("DefaultConnection"));

        services.AddIdentity<ApplicationUser, IdentityRole<Guid>>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddUserManager<WeLearnUserManager>()
            .AddDefaultTokenProviders();

        services
            .AddIdentityServer(options =>
            {
                options.Events.RaiseErrorEvents = true;
                options.Events.RaiseInformationEvents = true;
                options.Events.RaiseFailureEvents = true;
                options.Events.RaiseSuccessEvents = true;

                // see https://docs.duendesoftware.com/identityserver/v6/fundamentals/resources/
                options.EmitStaticAudienceClaim = true;
            })
            .AddInMemoryIdentityResources(Config.IdentityResources)
            .AddInMemoryApiScopes(Config.ApiScopes)
            .AddInMemoryClients(Config.Clients)
            .AddAspNetIdentity<ApplicationUser>();

        var authentication = services.AddAuthentication();
        var googleAuthSettings = configuration.GetSection("Auth").GetSection(GoogleAuthSettings.SectionName)
                    .Get<GoogleAuthSettings>();
        if (googleAuthSettings is not null && googleAuthSettings.Enabled)
        {
            authentication = authentication
            .AddGoogle(options =>
            {
                options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;

                // register your IdentityServer with Google at https://console.developers.google.com
                // enable the Google+ API
                // set the redirect URI to https://localhost:5051/signin-google
                options.ClientId = googleAuthSettings.ClientId;
                options.ClientSecret = googleAuthSettings.ClientSecret;
            });
            Log.Logger.Information("Google auth detected.");
        }

        services.AddWeLearnServices(configuration);

        return builder.Build();
    }

    public static WebApplication ConfigurePipeline(this WebApplication app)
    {
        var environment = app.Environment;

        app.UseSerilogRequestLogging();

        app.UseForwardedHeaders();

        if (environment.IsDevelopment() || environment.IsLocal())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseStaticFiles();
        app.UseRouting();
        app.UseIdentityServer();
        app.UseAuthorization();

        app.MapRazorPages()
            .RequireAuthorization();
        app.MapControllers();

        app.UseWeLearnSeeding();

        return app;
    }
}