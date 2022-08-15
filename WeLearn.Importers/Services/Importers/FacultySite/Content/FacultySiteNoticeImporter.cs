using HtmlAgilityPack;
using HtmlAgilityPack.CssSelectors.NetCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WeLearn.Data.Models.Content;
using WeLearn.Data.Persistence;
using WeLearn.Importers.Services.File;
using WeLearn.Importers.Services.Importers.Content;
using WeLearn.Importers.Services.Importers.Content.Database;
using WeLearn.Importers.Services.Importers.Content.Database.Notice;
using WeLearn.Importers.Services.Importers.FacultySite.Dtos;

namespace WeLearn.Importers.Services.Importers.FacultySite.Content;

public class FacultySiteNoticeImporter : HttpDbNoticeImporter<GetFacultySiteNoticeDto>, IFacultySiteNoticeImporter
{
    private readonly IOptionsMonitor<FacultySiteNoticeImporterSettings> _optionsMonitor;
    private FacultySiteNoticeImporterSettings settings;
    private List<Notice> currentContent;
    private List<GetFacultySiteNoticeDto> currentDtos;
    private readonly CultureInfo cultureInfo = CultureInfo.GetCultureInfo("sr");

    public FacultySiteNoticeImporter(
        IOptionsMonitor<FacultySiteNoticeImporterSettings> optionsMonitor,
        HttpClient httpClient,
        ApplicationDbContext dbContext,
        IFilePersistenceService filePersistenceService,
        ILogger<FacultySiteNoticeImporter> logger) : base(
            httpClient,
            dbContext,
            filePersistenceService,
            logger)
    {
        _optionsMonitor = optionsMonitor;
        settings = _optionsMonitor.CurrentValue;

        currentContent = new List<Notice>();
        currentDtos = new List<GetFacultySiteNoticeDto>();

        ConfigureHttpClient();
    }

    private void ConfigureHttpClient()
    {
    }

    public override string Name => nameof(FacultySiteNoticeImporter);

    protected override IEnumerable<Notice> CurrentContent { get => currentContent; set => currentContent = new List<Notice>(value); }
    protected override IEnumerable<GetFacultySiteNoticeDto> CurrentDtos { get => currentDtos; set => new List<GetFacultySiteNoticeDto>(value); }

    public override void Reset()
    {
    }

    protected override async Task<IEnumerable<GetFacultySiteNoticeDto>> GetNextDtoAsync(CancellationToken cancellationToken)
    {
        var resultDtos = new List<GetFacultySiteNoticeDto>();

        try
        {
            var homeHtml = await HttpClient.GetStringAsync(settings.HomePageUrl, cancellationToken);
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
                try
                {
                    // TODO take innerhtml for hyperlinks
                    var noticeHtml = await HttpClient.GetStringAsync(link);
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
                        // TODO get Id from link
                        CreatedDate = createdDate,
                        PublishedDate = publishedDate,
                        Title = title,
                        Attachments = attachments,
                    };
                    resultDtos.Add(dto);
                }
                catch (HttpRequestException ex)
                {
                    Logger.LogError(ex, $"Error getting Notice html from {link}");
                    throw;
                }

            }


        }
        catch (HttpRequestException ex)
        {
            Logger.LogError(ex, "Failed to get {@Url}", settings.HomePageUrl);
        }


        return resultDtos;
    }

    protected override async Task<IEnumerable<Notice>> MapDtoAsync(CancellationToken cancellationToken)
    {
        var notices = new List<Notice>();

        return notices;
    }
}
