using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using WeLearn.Api.Exceptions.Models;
using WeLearn.Api.Services.Document;
using WeLearn.Auth.Controllers;

namespace WeLearn.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DocumentsController : UserAuthorizedController
{
    private readonly IDocumentsService _documentsService;

    public DocumentsController(IDocumentsService documentsService)
    {
        _documentsService = documentsService;
    }

    [HttpGet("{documentId}")]
    public async Task<IActionResult> GetDownloadDocumentAsync([FromRoute] Guid documentId)
    {
        try
        {
            Stream documentStream;
            string fileName;
            string extension;
            (documentStream, fileName, extension) = await _documentsService.GetDocumentStreamAsync(documentId);

            var mimeType = MimeTypes.GetMimeType(extension);
            if (!Path.HasExtension(fileName))
                fileName += extension;

            return File(documentStream, mimeType, fileName);
        }
        catch (DocumentNotFoundException)
        {
            return NotFound();
        }
    }
}
