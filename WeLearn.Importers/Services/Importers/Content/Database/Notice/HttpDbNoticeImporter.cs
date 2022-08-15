using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeLearn.Data.Models.Content;
using WeLearn.Data.Persistence;
using WeLearn.Importers.Services.File;

namespace WeLearn.Importers.Services.Importers.Content.Database.Notice;

public abstract class HttpDbNoticeImporter<TDto> : HttpDbImporter<Data.Models.Content.Notice, TDto>
    where TDto : class
{
    protected HttpDbNoticeImporter(
        HttpClient httpClient,
        ApplicationDbContext dbContext,
        IFilePersistenceService filePersistenceService,
        ILogger logger) : base(
            httpClient,
            dbContext,
            filePersistenceService,
            logger)
    {
    }

    protected override IQueryable<Data.Models.Content.Notice> IncludeEntitiesBeforeUpdate(DbSet<Data.Models.Content.Notice> dbSet)
    {
        var baseIncluded = base.IncludeEntitiesBeforeUpdate(dbSet);

        return baseIncluded.Include(n => n.Documents);
    }
}
