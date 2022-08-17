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

namespace WeLearn.IdentityServer;

internal static class HostingExtensions
{
    private const string authority = "https://localhost:7230"; // TODO read from config

    public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
    {
        var configuration = builder.Configuration;
        var environment = builder.Environment;
        var services = builder.Services;

        var mvcBuilder = services.AddRazorPages();
        if (environment.IsLocal())
            mvcBuilder.AddRazorRuntimeCompilation();
        services.AddControllers();
        services.AddControllersWithViews();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "WeLearn",
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
            .AddAspNetIdentity<ApplicationUser>()
            ;
        //.AddApiAuthorization<ApplicationUser, ApplicationDbContext>();
        //.AddProfileService<ProfileService>();

        var authentication = services.AddAuthentication()
        .AddJwtBearer(options =>
        {
            options.Authority = authority; // TODO use configuration

            options.TokenValidationParameters.ValidateAudience = false;

            options.TokenValidationParameters.ValidTypes = new[] { "at+jwt" };

            options.Events = new JwtBearerEvents
            {
                OnTokenValidated = async (context) =>
                {
                    //context.HttpContext.RequestServices // TODO use
                    // TODO use usermanager here with sub claim
                    if (context.SecurityToken is JwtSecurityToken jwt)
                    {
                        var accessToken = jwt.RawData;
                        var oidcClient = new OidcClient(new OidcClientOptions
                        {
                            Authority = authority,
                        });
                        var userInfoResult = await oidcClient.GetUserInfoAsync(accessToken);
                        if (userInfoResult.IsError)
                            throw new Exception(userInfoResult.ErrorDescription); // TODO

                        var claims = userInfoResult.Claims;
                        var claimsIdentity = new ClaimsIdentity(claims); 
                        context.Principal.AddIdentity(claimsIdentity);
                    }
                }
            };
        });

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
            // TODO note: define "ApiController" policy - requires jwt auth - apply to every controller (maybe using reflection)
        });

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
        app.MapControllers()
            .RequireAuthorization();

        app.UseWeLearnSeeding();

        return app;
    }
}