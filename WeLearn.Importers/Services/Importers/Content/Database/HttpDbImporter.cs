﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeLearn.Data.Persistence;
using WeLearn.Data.Models.Content;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace WeLearn.Importers.Services.Importers.Content.Database;

// TODO remove setters from interfaces
public abstract class HttpDbImporter<TContent, TDto> : TypedContentImporter<TContent, TDto>
    where TContent : Data.Models.Content.Content
    where TDto : class
{
    protected HttpClient HttpClient { get; set; }
    protected ApplicationDbContext DbContext { get; set; }

    protected override async Task SaveCurrentContentAsync(CancellationToken cancellationToken)
    {
        var dbSet = DbContext.Set<TContent>();
        if (!(CurrentContent?.Any() ?? false))
            return;

        foreach (var content in CurrentContent)
        {
            var externalId = content.ExternalId;
            var externalSystemId = content.ExternalSystemId;
            var existingContent = await dbSet.FirstOrDefaultAsync(e =>
            e.ExternalId == externalId
            && e.ExternalSystemId == externalSystemId, cancellationToken);

            if (existingContent is null)
            {
                dbSet.Add(content);
                Logger.LogInformation("Added Content {@ContentId} with ExternalId {@ExternalId}", content.Id, content.ExternalId);
            }
            else
            {
                existingContent.Update(content);
                Logger.LogInformation("Updating Content {@ContentId} with ExternalId {@ExternalId}", content.Id, content.ExternalId);
            }
        }

        if (DbContext.ChangeTracker.HasChanges())
        {
            Logger.LogInformation("Saving changes to database");
            await DbContext.SaveChangesAsync();
        }
        else
            Logger.LogInformation("No changes to save");

        // clear after saving
        CurrentContent = new List<TContent>();
        CurrentDtos = new List<TDto>();
    }
}
