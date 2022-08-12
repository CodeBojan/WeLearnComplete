using WeLearn.Data.Models.Content;

namespace WeLearn.Importers.Services.Importers.Content
{
    public interface ITypedContentImporter<TContent, TDto> : IContentImporter
        where TContent : Data.Models.Content.Content
        where TDto : class
    {
    }
}