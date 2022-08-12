using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeLearn.Data.Models;
using WeLearn.Data.Models.Content;

namespace WeLearn.Importers.Services.Importers.Content.Database;

public abstract class AuthorizedHttpDbContentImporter<TContent, TDto> : HttpDbImporter<TContent, TDto>
    where TContent : Data.Models.Content.Content
    where TDto : class
{
    // TODO add authorization settings?
    public Guid ExternalSystemId { get; protected set; }
    public abstract Task<IEnumerable<Credentials>> GetCredentialsAsync();
    public abstract Task PrepareHttpClientAsync();
}
