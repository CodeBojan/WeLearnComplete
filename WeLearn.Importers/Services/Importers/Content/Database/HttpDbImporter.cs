using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeLearn.Data.Persistence;
using WeLearn.Data.Models.Content;

namespace WeLearn.Importers.Services.Importers.Content.Database;

// TODO remove setters from interfaces
public abstract class HttpDbImporter<TContent, TDto> : TypedContentImporter<TContent, TDto>
    where TContent : ContentBase
    where TDto : class
{
    protected HttpClient HttpClient { get; set; }
    protected ApplicationDbContext DbContext { get; set; }

    protected override async Task SaveCurrentContentAsync()
    {
        if (IsFinished)
            return;

        var dbSet = DbContext.Set<TContent>();
        if (!(CurrentContent?.Any() ?? false))
            return;

        // TODO check if each entry of current content exists
        dbSet.AddRange(CurrentContent);

        await DbContext.SaveChangesAsync();
    }
}
