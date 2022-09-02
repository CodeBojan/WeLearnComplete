using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeLearn.Data.Persistence;
using WeLearn.Data.Models.Content;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WeLearn.Importers.Services.File;
using WeLearn.Importers.Services.Notification;
using WeLearn.Data.Models.Content.Notices;
using WeLearn.Data.Models.Notifications;

namespace WeLearn.Importers.Services.Importers.Content.Database;

public abstract class HttpDbImporter<TContent, TDto> : TypedContentImporter<TContent, TDto>
    where TContent : Data.Models.Content.Content
    where TDto : class
{

    public HttpDbImporter(
        HttpClient httpClient,
        ApplicationDbContext dbContext,
        IFilePersistenceService filePersistenceService,
        ILogger logger,
        INotificationService notificationService)
    {
        HttpClient = httpClient;
        DbContext = dbContext;
        FilePersistenceService = filePersistenceService;
        Logger = logger;
        NotificationService = notificationService;
    }
    // TODO notification service
    protected HttpClient HttpClient { get; init; }
    protected ApplicationDbContext DbContext { get; init; }
    protected IFilePersistenceService FilePersistenceService { get; init; }
    protected INotificationService NotificationService { get; init; }
    protected ILogger Logger { get; init; }

    protected override async Task SaveCurrentContentAsync(CancellationToken cancellationToken)
    {
        var dbSet = DbContext.Set<TContent>();
        if (!(CurrentContent?.Any() ?? false))
            return;

        foreach (var content in CurrentContent)
        {
            var externalId = content.ExternalId;
            var externalSystemId = content.ExternalSystemId;
            var existingContent = await IncludeEntitiesBeforeUpdate(dbSet)
                .FirstOrDefaultAsync(e =>
            e.ExternalId == externalId
            && e.ExternalSystemId == externalSystemId, cancellationToken);

            if (existingContent is null)
            {
                dbSet.Add(content);
                Logger.LogInformation("Added Content {@ContentId} with ExternalId {@ExternalId}", content.Id, content.ExternalId);
                await NotifyUsersOfCreatedContent(content, cancellationToken);
            }
            else
            {
                Logger.LogInformation("Updating Content {@ContentId} with ExternalId {@ExternalId}", content.Id, content.ExternalId);
                try
                {
                    existingContent.Update(content);
                }
                catch (ArgumentException ex)
                {
                    Logger.LogError(ex, "Updating Content {@ContentId} with ExternalId {@ExternalId} failed", content.Id, content.ExternalId);
                }
            }
        }

        if (DbContext.ChangeTracker.HasChanges())
        {
            var contentEntries = DbContext.ChangeTracker
                            .Entries<TContent>();

            var updatedContent = contentEntries
                .Where(e => e.State == EntityState.Modified)
                .ToList();
            var createdContent = contentEntries
                .Where(ce => ce.State == EntityState.Added)
                .ToList();

            if (updatedContent.Any())
                foreach (var content in updatedContent)
                    await NotifyUsersOfUpdatedContent(content.Entity, cancellationToken);

            if (createdContent.Any())
                foreach (var content in createdContent)
                    await NotifyUsersOfCreatedContent(content.Entity, cancellationToken);

            Logger.LogInformation("Saving changes to database");
            await DbContext.SaveChangesAsync(cancellationToken);
        }
        else
            Logger.LogInformation("No changes to save");

        CurrentContent = new List<TContent>();
        CurrentDtos = new List<TDto>();
    }

    private async Task NotifyUsersOfContentOperation(TContent content, NotificationOperationType operationType, CancellationToken cancellationToken)
    {
        var followingUserIds = await GetFollowingUsersIdsAsync(content, cancellationToken);

        foreach (var accountId in followingUserIds)
        {
            await CreateContentNotificationAsync(content, operationType.FriendlyName(), accountId);
        }
        Logger.LogInformation("Notifying {@AccountIds} of {@OperationType} content with id {@ContentId}", followingUserIds, operationType, content.Id);
    }

    protected async Task NotifyUsersOfCreatedContent(TContent content, CancellationToken cancellationToken)
    {
        await NotifyUsersOfContentOperation(content, NotificationOperationType.Create, cancellationToken);
    }

    protected async Task NotifyUsersOfUpdatedContent(TContent content, CancellationToken cancellationToken)
    {
        await NotifyUsersOfContentOperation(content, NotificationOperationType.Update, cancellationToken);
    }

    protected virtual async Task CreateContentNotificationAsync(TContent content, string operationType, Guid accountId)
    {
        string contentFriendlyName = Enum.Parse<ContentType>(content.Type).FriendlyName();

        string? author = null;
        if (!string.IsNullOrWhiteSpace(content.Author))
            author = content.Author;
        string bodyHeading;
        if (author is not null)
        {
            bodyHeading = $"{author} has {operationType} a new {contentFriendlyName}.";
        }
        else
            bodyHeading = $"A new {contentFriendlyName} has been {operationType}.";

        // TODO problem - what if html tag and split incorrectly
        var bodyLength = content.Body?.Length ?? 0;
        var bodyCharCount = Math.Min(bodyLength, 150);
        var notifBody = bodyHeading + Environment.NewLine + content.Body?.Substring(0, bodyCharCount) ?? string.Empty;

        // TODO ensure title isnt null

        await NotificationService.CreateContentNotificationAsync(content.Title, notifBody, content.ExternalUrl, content?.ExternalSystem?.LogoUrl, accountId, Data.Models.Notifications.NotificationOperationType.Create, content.Id, false);
    }

    protected abstract Task<IEnumerable<Guid>> GetFollowingUsersIdsAsync(TContent content, CancellationToken cancellationToken);

    protected virtual IQueryable<TContent> IncludeEntitiesBeforeUpdate(DbSet<TContent> dbSet)
    {
        return dbSet;
    }
}
