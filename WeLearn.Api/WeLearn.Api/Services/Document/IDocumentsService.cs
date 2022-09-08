using WeLearn.Api.Dtos.Document;

namespace WeLearn.Api.Services.Document
{
    public interface IDocumentsService
    {
        Task<GetDocumentDto> GetDocumentAsync(Guid documentId);
        Task<(Stream, string, string)> GetDocumentStreamAsync(Guid documentId);
    }
}