using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WeLearn.Importers.Services.Importers.Content;
using WeLearn.Importers.Services.Importers.FacultySite.Content;
using WeLearn.Importers.Services.Importers.FacultySite.System;
using WeLearn.Importers.Services.System;

namespace WeLearn.Importers.Services.Importers.FacultySite.Extensions;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddFacultySiteServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<FacultySiteSystemImporterSettings>(configuration.GetSection(nameof(FacultySiteSystemImporterSettings)));
        services.AddScoped<FacultySiteSystemImporter>();
        services.AddScoped<ISystemImporter, FacultySiteSystemImporter>(sp => sp.GetRequiredService<FacultySiteSystemImporter>());
        services.AddScoped<IFacultySiteSystemImporter, FacultySiteSystemImporter>(sp => sp.GetRequiredService<FacultySiteSystemImporter>());

        services.Configure<FacultySiteNoticeImporterSettings>(configuration.GetSection(nameof(FacultySiteNoticeImporterSettings)));
        services.AddScoped<FacultySiteNoticeImporter>();
        services.AddScoped<IFacultySiteImporter, FacultySiteNoticeImporter>(sp => sp.GetRequiredService<FacultySiteNoticeImporter>());
        services.AddScoped<IFacultySiteNoticeImporter, FacultySiteNoticeImporter>(sp => sp.GetRequiredService<FacultySiteNoticeImporter>());
        services.AddScoped<INoticeImporter, FacultySiteNoticeImporter>(sp => sp.GetRequiredService<FacultySiteNoticeImporter>());

        return services;
    }
}
