using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeLearn.Data.Models.Content;

namespace WeLearn.Importers.Services.Importers.Content;

public abstract class TypedContentImporter<TContent, TDto> : ITypedContentImporter<TContent, TDto>
    where TContent : ContentBase
    where TDto : class
{
    public virtual bool IsFinished { get; set; }
    public virtual async Task ImportNextAsync()
    {
        CurrentDto = await GetNextDtoAsync();

        CurrentContent = await MapDtoAsync();

        await SaveCurrentContentAsync();
    }

    public abstract void Reset();
    public abstract string Name { get; }

    protected abstract IEnumerable<TContent> CurrentContent { get; set; }
    protected abstract IEnumerable<TDto> CurrentDto { get; set; }

    protected abstract Task<IEnumerable<TDto>> GetNextDtoAsync();
    protected abstract Task<IEnumerable<TContent>> MapDtoAsync();

    protected virtual int ContentCountPerImport { get; set; }
    protected abstract Task SaveCurrentContentAsync();
}
