using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using WeLearn.Api.Exceptions.Models;
using WeLearn.Api.Services.Document;

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
            (documentStream, fileName) = await _documentsService.GetDocumentStreamAsync(documentId);
            return File(documentStream, MediaTypeNames.Application.Octet, fileName);
        }
        catch (DocumentNotFoundException)
        {
            return NotFound();
        }
    }
}
