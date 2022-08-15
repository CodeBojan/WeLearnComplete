﻿using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeLearn.Data.Models;
using WeLearn.Data.Models.Content;
using WeLearn.Data.Persistence;
using WeLearn.Importers.Services.File;

namespace WeLearn.Importers.Services.Importers.Content.Database;

public abstract class AuthorizedHttpDbContentImporter<TContent, TDto> : HttpDbImporter<TContent, TDto>
    where TContent : Data.Models.Content.Content
    where TDto : class
{
    protected AuthorizedHttpDbContentImporter(
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

    // TODO add authorization settings?
    public Guid ExternalSystemId { get; protected set; }
    public abstract Task<IEnumerable<Credentials>> GetCredentialsAsync();
    public abstract Task PrepareHttpClientAsync();
}
