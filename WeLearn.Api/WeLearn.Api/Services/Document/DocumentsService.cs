using WeLearn.Api.Dtos.Document;
using WeLearn.Data.Persistence;

namespace WeLearn.Api.Services.Document;

public class DocumentsService : IDocumentsService
{
    private readonly ApplicationDbContext _dbContext;

    public DocumentsService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<GetDocumentDto> GetDocumentAsync(Guid documentId)
    {
        throw new NotImplementedException();
    }
}
