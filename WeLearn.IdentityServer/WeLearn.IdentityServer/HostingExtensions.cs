using Duende.IdentityServer;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Reflection;
using WeLearn.Auth.SwaggerGen.OperationFilters;
using WeLearn.Data.Extensions;
using WeLearn.Data.Models;
using WeLearn.Data.Persistence;
using WeLearn.IdentityServer.Configuration.Auth.Google;
using WeLearn.IdentityServer.Extensions.Seeding;
using WeLearn.IdentityServer.Extensions.Services;
using WeLearn.IdentityServer.Services.Identity;
using WeLearn.Shared.Extensions.WebHostEnvironmentExtensions;
using System.IdentityModel.Tokens.Jwt;
using IdentityModel.OidcClient;
using System.Security.Claims;
using WeLearn.Shared.Extensions.Services;
using WeLearn.Auth.Policy;
using WeLearn.Auth.Authorization.Roles;
using WeLearn.Auth.Extensions;
using WeLearn.IdentityServer.Extensions.RazorPages;
using Hellang.Middleware.ProblemDetails;

namespace WeLearn.IdentityServer;

internal static class HostingExtensions
{
    public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
    {
        var configuration = builder.Configuration;
        var environment = builder.Environment;
        var services = builder.Services;

        var mvcBuilder = services.AddRazorPages(options =>
        {
            options.ApplyAuthPolicies();
        });
        if (environment.IsLocal())
            mvcBuilder.AddRazorRuntimeCompilation();
        services.AddProblemDetails(options =>
        {
            options.IncludeExceptionDetails = (ctx, ex) =>
            {
                var env = ctx.RequestServices.GetRequiredService<IWebHostEnvironment>();
                return env.IsDevelopment() || env.IsLocal();
            };
        });

        services.AddCors(options =>
        {
            options.AddWeLearnCors(configuration);
        });
        services.AddControllers();
        services.AddControllersWithViews();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "WeLearn Identity Server",
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

        services.Configure<ForwardedHeadersOptions>(options =>
        {
            options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto | ForwardedHeaders.XForwardedHost;

            options.KnownNetworks.Clear();
            options.KnownProxies.Clear();
        });

        services.AddWeLearnDbContext(configuration.GetConnectionString("DefaultConnection"));

        services.AddIdentity<ApplicationUser, IdentityRole<Guid>>(options =>
        {
            options.User.RequireUniqueEmail = true; // TODO migration for unique index
        })
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
            .AddInMemoryClients(configuration.GetSection("Auth:IdentityServer:Clients"))
            .AddAspNetIdentity<ApplicationUser>();

        var authentication = services.AddAuthentication()
        .AddIdentityServerAuthentication(configuration.GetSection("Auth:IdentityServer:Authority").Get<string>());

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

        services.AddAuthorization(options =>
        {
            options.AddAuthorizationPolicies();
        });

        services.AddAuthorizationHandlers();

        services.AddSharedServices(configuration);
        services.AddWeLearnIdentityServerServices(configuration);

        return builder.Build();
    }

    public static WebApplication ConfigurePipeline(this WebApplication app)
    {
        var environment = app.Environment;

        app.UseSerilogRequestLogging();

        app.UseForwardedHeaders();

        app.UseProblemDetails();

        if (environment.IsDevelopment() || environment.IsLocal())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        // TODO check why commented out
        //app.UseAuthentication();

        app.UseStaticFiles();
        app.UseRouting();
        app.UseCors();
        app.UseIdentityServer();
        app.UseAuthorization();

        app.MapControllers()
            .RequireAuthorization();
        app.MapRazorPages()
            .RequireAuthorization();

        app.UseWeLearnSeeding();

        return app;
    }
}