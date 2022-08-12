using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeLearn.Data.Models.Content;

namespace WeLearn.Importers.Services.Importers.Content;

public abstract class TypedContentImporter<TContent, TDto> : ITypedContentImporter<TContent, TDto>
    where TContent : Data.Models.Content.ContentBase
    where TDto : class
{
    public virtual bool IsFinished { get; set; }
    public virtual async Task ImportNextAsync(CancellationToken cancellationToken)
    {
        CurrentDtos = await GetNextDtoAsync(cancellationToken);

        CurrentContent = await MapDtoAsync(cancellationToken);

        await SaveCurrentContentAsync(cancellationToken);
    }

    public abstract void Reset();
    public abstract string Name { get; }

    protected abstract IEnumerable<TContent> CurrentContent { get; set; }
    protected abstract IEnumerable<TDto> CurrentDtos { get; set; }

    protected abstract Task<IEnumerable<TDto>> GetNextDtoAsync(CancellationToken cancellationToken);
    protected abstract Task<IEnumerable<TContent>> MapDtoAsync(CancellationToken cancellationToken);

    protected virtual int ContentCountPerImport { get; set; }
    protected abstract Task SaveCurrentContentAsync(CancellationToken cancellationToken);
}
