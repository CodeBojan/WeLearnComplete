using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
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
    }

    public override string Name => nameof(NoticeBoardNoticeImporter);

    protected override IEnumerable<Notice> CurrentContent { get => currentContent; set => currentContent = value.ToList(); }
    protected override IEnumerable<GetNoticeBoardNoticeDto> CurrentDto { get => currentDtos; set => currentDtos = value.ToList(); }

    public override void Reset()
    {
        _logger.LogWarning("Not implemented");
    }

    protected override async Task<IEnumerable<GetNoticeBoardNoticeDto>> GetNextDtoAsync()
    {
        _logger.LogWarning("Not implemented");
        IsFinished = true;
        return new List<GetNoticeBoardNoticeDto>();
    }

    protected override async Task<IEnumerable<Notice>> MapDtoAsync()
    {
        _logger.LogWarning("Not implemented");
        return new List<Notice>();
    }
}
