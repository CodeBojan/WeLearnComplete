using HtmlAgilityPack;
using HtmlAgilityPack.CssSelectors.NetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WeLearn.Data.Models;
using WeLearn.Data.Models.Content;
using WeLearn.Data.Models.Content.Notices;
using WeLearn.Data.Persistence;
using WeLearn.Importers.Services.File;
using WeLearn.Importers.Services.Importers.Content;
using WeLearn.Importers.Services.Importers.Content.Database;
using WeLearn.Importers.Services.Importers.Content.Database.Notice;
using WeLearn.Importers.Services.Importers.FacultySite.Dtos;
using WeLearn.Importers.Util.Uri;

namespace WeLearn.Importers.Services.Importers.FacultySite.Content;

public class FacultySiteNoticeImporter : HttpDbNoticeImporter<GetFacultySiteNoticeDto>, IFacultySiteNoticeImporter
{
    private readonly IOptionsMonitor<FacultySiteNoticeImporterSettings> _settingsMonitor;
    private FacultySiteNoticeImporterSettings settings;
    private List<Notice> currentContent;
    private List<GetFacultySiteNoticeDto> currentDtos;
    private readonly CultureInfo cultureInfo = CultureInfo.GetCultureInfo("sr");
    private readonly Regex externalIdRegex = new("novosti\\/(\\d+)");

    public FacultySiteNoticeImporter(
        IOptionsMonitor<FacultySiteNoticeImporterSettings> settingsMonitor,
        HttpClient httpClient,
        ApplicationDbContext dbContext,
        IFilePersistenceService filePersistenceService,
        ILogger<FacultySiteNoticeImporter> logger) : base(
            httpClient,
            dbContext,
            filePersistenceService,
            logger)
    {
        _settingsMonitor = settingsMonitor;
        settings = _settingsMonitor.CurrentValue;

        currentContent = new List<Notice>();
        currentDtos = new List<GetFacultySiteNoticeDto>();

        ConfigureHttpClient();
    }

    private void ConfigureHttpClient()
    {
    }

    public override string Name => nameof(FacultySiteNoticeImporter);

    protected override IEnumerable<Notice> CurrentContent { get => currentContent; set => currentContent = new List<Notice>(value); }
    protected override IEnumerable<GetFacultySiteNoticeDto> CurrentDtos { get => currentDtos; set => currentDtos = new List<GetFacultySiteNoticeDto>(value); }

    public override void Reset()
    {
        settings = _settingsMonitor.CurrentValue;
        Logger.LogWarning("Resetting");
        // TODO reset
    }

    protected override async Task<IEnumerable<GetFacultySiteNoticeDto>> GetNextDtoAsync(CancellationToken cancellationToken)
    {
        var resultDtos = new List<GetFacultySiteNoticeDto>();

        string homePageUrl = settings.HomePageUrl;
        Logger.LogInformation("Fetching FacultySite {@FacultySiteUrl}", homePageUrl);
        try
        {
            var homeHtml = await HttpClient.GetStringAsync(homePageUrl, cancellationToken);
            var homeHtmlDocument = new HtmlDocument();
            homeHtmlDocument.LoadHtml(homeHtml);

            var aNodes = homeHtmlDocument.DocumentNode.QuerySelectorAll(".mod-articles-category-title");
            if (!aNodes.Any())
            {
                Logger.LogWarning("No notice links found");
                return resultDtos;
            }
            foreach (var aNode in aNodes)
            {
                var link = aNode.Attributes["href"].Value;
                Logger.LogInformation("Fetching FacultySite Notice {@NoticeUrl}", link);
                try
                {
                    var noticeId = int.Parse(externalIdRegex.Match(link).Groups[1].Value);
                    var noticeHtml = await HttpClient.GetStringAsync(link, cancellationToken);
                    var noticeHtmlDocument = new HtmlDocument();
                    noticeHtmlDocument.LoadHtml(noticeHtml);

                    var titleNode = noticeHtmlDocument.QuerySelector(".item-page > h1");
                    var createdDateNode = noticeHtmlDocument.QuerySelector(".article-info .create");
                    var publishedDateNode = noticeHtmlDocument.QuerySelector(".article-info .published");
                    var articleNode = noticeHtmlDocument.QuerySelector("article");
                    var documentNodes = noticeHtmlDocument.QuerySelectorAll(".attachmentsList tbody tr");

                    var title = titleNode.InnerText.Trim();
                    var createdDate = DateTime.Parse(createdDateNode.InnerText.Split(":")[1].Trim(), cultureInfo);
                    var publishedDate = DateTime.Parse(publishedDateNode.InnerText.Split(" ")[1].Trim(), cultureInfo);

                    string body = "";
                    var articleBodyNodes = articleNode?.ChildNodes.Skip(4);
                    if (articleBodyNodes?.Any() ?? false)
                    {
                        foreach (var node in articleBodyNodes)
                        {
                            body += node.OuterHtml;
                        }
                    }
                    body = body.Trim();

                    var attachments = new List<GetFacultySiteNoticeAttachmentDto>();
                    if (documentNodes?.Any() ?? false)
                    {
                        foreach (var documentNode in documentNodes)
                        {
                            var documentFileNode = documentNode.QuerySelector(".at_filename .at_url");
                            var documentSizeNode = documentNode.QuerySelector(".at_file_size");
                            var documentCreatedDateNode = documentNode.QuerySelector(".at_created_date");

                            var documentPreviewName = documentFileNode.InnerText;
                            var documentLink = documentFileNode.Attributes["href"].Value;
                            var documentSize = long.Parse(documentSizeNode.InnerText.Split(" ")[0]) * 1000;
                            var documentCreatedDate = DateTime.Parse(documentCreatedDateNode.InnerText, cultureInfo);

                            attachments.Add(new GetFacultySiteNoticeAttachmentDto
                            {
                                FileSize = documentSize,
                                PreviewName = documentPreviewName,
                                Url = documentLink,
                                CreatedDate = documentCreatedDate
                            });
                        }
                    }

                    var dto = new GetFacultySiteNoticeDto
                    {
                        Id = noticeId,
                        CreatedDate = createdDate,
                        PublishedDate = publishedDate,
                        Title = title,
                        Body = body,
                        Attachments = attachments,
                        Url = link
                    };
                    resultDtos.Add(dto);
                }
                catch (HttpRequestException ex)
                {
                    Logger.LogError(ex, "Error getting Notice from {@NoticeUrl}", link);
                    throw;
                }
            }
        }
        catch (HttpRequestException ex)
        {
            Logger.LogError(ex, "Failed to get {@Url}", homePageUrl);
        }
        finally
        {
            IsFinished = true;
        }

        return resultDtos;
    }

    protected override async Task<IEnumerable<Notice>> MapDtoAsync(CancellationToken cancellationToken)
    {
        var notices = new List<Notice>();

        var externalSystem = await InitializeExternalSystemAsync(cancellationToken);
        if (externalSystem is null)
        {
            Logger.LogError("External system {@ExternalSystemName} not initialized", Constants.FacultySystemName);
            return notices;
        }

        var currentDtos = CurrentDtos;
        var dtoIds = currentDtos.Select(dto => dto.Id);

        Logger.LogInformation("Mapping DTOs {@DtoIds}", dtoIds);

        foreach (var dto in currentDtos)
        {
            var generalNotice = new GeneralNotice(dto.Id.ToString(), dto.Url, dto.Body, dto.Title, null, true, null, externalSystem.Id, dto.PublishedDate.ToUniversalTime(), null);

            foreach (var attachment in dto.Attachments)
            {
                string attachmentExternalId = $"{dto.Id}:{attachment.PreviewName}";

                string downloadedAttachmentUri;
                string hash;
                string hashAlgo;

                string fileExtension = UriUtil.ExtractFileExtensions(attachment.Url);

                try
                {
                    using var webStream = await HttpClient.GetStreamAsync(attachment.Url);
                    downloadedAttachmentUri = await FilePersistenceService.DownloadFileAsync(webStream, "attachment", cancellationToken);
                    (hash, hashAlgo) = await FilePersistenceService.GetFileHashAsync(downloadedAttachmentUri, cancellationToken);
                }
                catch (HttpRequestException ex)
                {
                    Logger.LogError(ex, "Error downloading attachment {@AttachmentId}", attachmentExternalId);
                    continue;
                }

                var document = new Document(attachmentExternalId, attachment.Url, null, null, null, true, null, null, externalSystem.Id, attachment.CreatedDate.ToUniversalTime(), attachment.PreviewName, downloadedAttachmentUri, attachment.FileSize, hash, hashAlgo, generalNotice.Id, null, fileExtension);

                generalNotice.TryAddDocument(document);
            }

            notices.Add(generalNotice);
        }

        Logger.LogInformation("Mapped DTOs {@DtoIds}", dtoIds);

        return notices;
    }

    private async Task<ExternalSystem?> InitializeExternalSystemAsync(CancellationToken cancellationToken)
    {
        var externalSystem = await DbContext.ExternalSystems.FirstOrDefaultAsync(es => es.Name == Constants.FacultySystemName, cancellationToken);
        return externalSystem;
    }
}
