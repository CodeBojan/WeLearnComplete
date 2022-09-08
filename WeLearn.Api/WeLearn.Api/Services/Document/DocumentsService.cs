using Microsoft.EntityFrameworkCore;
using WeLearn.Api.Dtos.Document;
using WeLearn.Api.Exceptions.Models;
using WeLearn.Data.Persistence;
using WeLearn.Importers.Services.File;

namespace WeLearn.Api.Services.Document;

public class DocumentsService : IDocumentsService
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IFilePersistenceService _filePersistenceService;

    public DocumentsService(
        ApplicationDbContext dbContext,
        IFilePersistenceService filePersistenceService)
    {
        _dbContext = dbContext;
        _filePersistenceService = filePersistenceService;
    }

    public Task<GetDocumentDto> GetDocumentAsync(Guid documentId)
    {
        throw new NotImplementedException();
    }

    public async Task<(Stream, string, string)> GetDocumentStreamAsync(Guid documentId)
    {
        var document = await _dbContext.Documents.AsNoTracking().FirstOrDefaultAsync(d => d.Id == documentId);
        if (document is null)
            throw new DocumentNotFoundException();

        return (await _filePersistenceService.GetDocumentStreamAsync(document.Uri), document.FileName, document.FileExtension);
    }
}
