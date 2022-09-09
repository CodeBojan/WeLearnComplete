namespace WeLearn.Importers.Services.Importers.Content;

public interface IDtoNoticeImporter<TDto> 
    : INoticeImporter, ITypedContentImporter<Data.Models.Content.Notice, TDto> 
    where TDto : class
{
}