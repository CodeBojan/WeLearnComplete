﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WeLearn.Importers.Services.Importers.NoticeBoard.Content;
using WeLearn.Importers.Services.Importers.NoticeBoard.System;
using WeLearn.Importers.Services.System;

namespace WeLearn.Importers.Services.Importers.NoticeBoard.Extensions;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddNoticeBoardServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<NoticeBoardSystemImporter>();
        services.AddScoped<ISystemImporter, NoticeBoardSystemImporter>(sp => sp.GetRequiredService<NoticeBoardSystemImporter>());
        services.AddScoped<INoticeBoardSystemImporter, NoticeBoardSystemImporter>(sp => sp.GetRequiredService<NoticeBoardSystemImporter>());

        services.Configure<NoticeBoardNoticeImporterSettings>(configuration.GetSection(nameof(NoticeBoardNoticeImporterSettings)));
        services.AddHttpClient<NoticeBoardNoticeImporter>();
        services.AddScoped<NoticeBoardNoticeImporter>();
        services.AddScoped<INoticeBoardImporter, NoticeBoardNoticeImporter>(sp => sp.GetRequiredService<NoticeBoardNoticeImporter>());
        services.AddScoped<INoticeBoardNoticeImporter, NoticeBoardNoticeImporter>(sp => sp.GetRequiredService<NoticeBoardNoticeImporter>());

        return services;
    }
}