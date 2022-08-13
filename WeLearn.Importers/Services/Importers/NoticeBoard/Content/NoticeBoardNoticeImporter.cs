using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using WeLearn.Data.Models.Content;
using WeLearn.Data.Persistence;
using WeLearn.Importers.Services.Importers.Content;
using WeLearn.Importers.Services.Importers.Content.Database;
using WeLearn.Importers.Services.Importers.Content.Database.Notice;
using WeLearn.Importers.Services.Importers.NoticeBoard.Dtos;

namespace WeLearn.Importers.Services.Importers.NoticeBoard.Content;

public class NoticeBoardNoticeImporter : HttpDbNoticeImporter<GetNoticeBoardNoticeDto>, INoticeBoardNoticeImporter
{
    private readonly NoticeBoardNoticeImporterSettings _settings;
    private List<Notice> currentContent;
    private List<GetNoticeBoardNoticeDto> currentDtos;
    private readonly ILogger _logger;

    public NoticeBoardNoticeImporter(
        IOptions<NoticeBoardNoticeImporterSettings> settingsOptions,
        ApplicationDbContext dbContext,
        HttpClient httpClient,
        ILogger<NoticeBoardNoticeImporter> logger)
    {
        _settings = settingsOptions.Value;
        DbContext = dbContext;
        HttpClient = httpClient;
        _logger = logger;

        currentContent = new List<Notice>();
        currentDtos = new List<GetNoticeBoardNoticeDto>();

        ConfigureHttpClient();
    }

    private void ConfigureHttpClient()
    {
        HttpClient.BaseAddress = new Uri(_settings.BaseUrl);
    }

    public override string Name => nameof(NoticeBoardNoticeImporter);

    protected override IEnumerable<Notice> CurrentContent { get => currentContent; set => currentContent = value.ToList(); }
    protected override IEnumerable<GetNoticeBoardNoticeDto> CurrentDtos { get => currentDtos; set => currentDtos = value.ToList(); }

    public override void Reset()
    {
        _logger.LogWarning("Not implemented");
    }

    protected override async Task<IEnumerable<GetNoticeBoardNoticeDto>> GetNextDtoAsync(CancellationToken cancellationToken)
    {
        var resultDtos = new List<GetNoticeBoardNoticeDto>();

        foreach (var boardId in _settings.BoardIds)
        {
            _logger.LogInformation("Fetching Notice Board {@NoticeBoardId}", boardId);

            var dtos = await HttpClient.GetFromJsonAsync<IEnumerable<GetNoticeBoardNoticeDto>>("/api/public/oglasne-ploce/3", cancellationToken);

            if (dtos is null)
            {
                _logger.LogWarning("No notices found for board {@NoticeBoardId}", boardId);
                continue;
            }

            _logger.LogInformation("Fetched {@NoticeBoardId}", boardId);
            resultDtos.AddRange(dtos);
        }

        IsFinished = true;

        return resultDtos;
    }

    protected override async Task<IEnumerable<Notice>> MapDtoAsync(CancellationToken cancellationToken)
    {
        var notices = new List<Notice>();

        var currentDtos = CurrentDtos;
        var dtoIds = currentDtos.Select(d => d.Id);

        _logger.LogInformation("Mapping DTOs {@DtoIds}", dtoIds);

        foreach (var dto in currentDtos)
        {
            var title = dto.Title
                .Trim()
                .ToUpper();
            // TODO add other properties to Notice
            foreach (var attachment in dto.Attachments)
            {
                var attachmentId = attachment.Id;
                
            }
            // TODO fetch and save attachments
            // TODO Convert datetime to utc
            notices.Add(new Notice() { Id = Guid.NewGuid(), ExternalId = dto.Id });
        }

        _logger.LogInformation("Mapped DTOs {@DtoIds}", currentDtos.Select(d => d.Id));

        return notices;
    }
}
