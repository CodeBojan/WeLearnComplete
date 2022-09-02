using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeLearn.Data.Models.Content;
using WeLearn.Data.Models.Content.Notices;
using WeLearn.Data.Persistence;
using WeLearn.Importers.Services.File;
using WeLearn.Importers.Services.Notification;

namespace WeLearn.Importers.Services.Importers.Content.Database.Notice;

public abstract class HttpDbNoticeImporter<TDto> : HttpDbImporter<Data.Models.Content.Notice, TDto>
    where TDto : class
{
    protected HttpDbNoticeImporter(
        HttpClient httpClient,
        ApplicationDbContext dbContext,
        IFilePersistenceService filePersistenceService,
        ILogger logger,
        INotificationService notificationService) : base(
            httpClient,
            dbContext,
            filePersistenceService,
            logger,
            notificationService)
    {
    }

    protected override IQueryable<Data.Models.Content.Notice> IncludeEntitiesBeforeUpdate(DbSet<Data.Models.Content.Notice> dbSet)
    {
        var baseIncluded = base.IncludeEntitiesBeforeUpdate(dbSet);

        return baseIncluded.Include(n => n.Documents);
    }

    protected override async Task<IEnumerable<Guid>> GetFollowingUsersIdsAsync(Data.Models.Content.Notice content, CancellationToken cancellationToken)
    {
        List<Guid> followingUserIds = new();
        if (content.CourseId is not null)
        {
            followingUserIds = await DbContext.FollowedCourses
            .AsNoTracking()
            .Where(f => f.CourseId == content.CourseId)
            .Select(f => f.AccountId)
            .ToListAsync(cancellationToken);
        }
        else if (content is StudyYearNotice syn) // TODO move elsewhere
        {
            followingUserIds = await DbContext.FollowedStudyYears
                .AsNoTracking()
                .Where(fsy => fsy.StudyYearId == syn.StudyYearId)
                .Select(fsy => fsy.AccountId)
                .ToListAsync(cancellationToken: cancellationToken);
        }

        return followingUserIds;
    }
}
