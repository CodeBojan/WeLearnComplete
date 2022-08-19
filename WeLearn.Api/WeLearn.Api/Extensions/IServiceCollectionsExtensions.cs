﻿using WeLearn.Api.Services.Course.Extensions;
using WeLearn.Api.Services.CourseMaterialUploadRequest.Extensions;
using WeLearn.Api.Services.Feed.Extensions;
using WeLearn.Api.Services.FollowedCourse.Extensions;
using WeLearn.Api.Services.FollowedStudyYear.Extensions;
using WeLearn.Data.Extensions;
using WeLearn.Importers.Extensions;
using WeLearn.Importers.Services.Notification.Extensions;
using WeLearn.Shared.Extensions.Services;

namespace WeLearn.Api.Extensions;

public static class IServiceCollectionsExtensions
{
    public static IServiceCollection AddApiServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSharedServices(configuration);

        services.AddWeLearnDbContext(configuration.GetConnectionString("DefaultConnection"));
        services.AddWeLearnFileSystemPersistence(configuration);

        services.AddWeLearnImporterServices(configuration);

        services.AddNotificationServices(configuration);

        services.AddBusinessServices(configuration);

        return services;
    }

    public static IServiceCollection AddBusinessServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddFollowedCourseServices(configuration);
        services.AddFollowedStudyYearServices(configuration);
        services.AddCourseServices(configuration);
        services.AddCourseMaterialUploadRequestServices(configuration);
        services.AddFeedServices(configuration);

        return services;
    }
}
