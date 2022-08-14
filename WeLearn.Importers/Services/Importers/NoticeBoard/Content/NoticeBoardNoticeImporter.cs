﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
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
using WeLearn.Importers.Services.Importers.NoticeBoard.Dtos;
using WeLearn.Shared.Services.CourseTitleCleaner;
using WeLearn.Shared.Services.StringMatcher;

namespace WeLearn.Importers.Services.Importers.NoticeBoard.Content;

public class NoticeBoardNoticeImporter : HttpDbNoticeImporter<GetNoticeBoardNoticeDto>, INoticeBoardNoticeImporter
{
    private const string persistenceKey = "attachment";
    private NoticeBoardNoticeImporterSettings _settings;
    private List<Notice> currentContent;
    private List<GetNoticeBoardNoticeDto> currentDtos;
    private readonly IStringMatcherService _stringMatcher;
    private readonly ICourseTitleCleanerService _titleCleaner;
    private readonly IOptionsMonitor<NoticeBoardNoticeImporterSettings> _settingsMonitor;
    private readonly IFilePersistenceService _filePersistenceService;
    private readonly ILogger _logger;

    public NoticeBoardNoticeImporter(
        IOptionsMonitor<NoticeBoardNoticeImporterSettings> settingsMonitor,
        ApplicationDbContext dbContext,
        HttpClient httpClient,
        ILogger<NoticeBoardNoticeImporter> logger,
        IStringMatcherService stringMatcher,
        ICourseTitleCleanerService courseTitleCleanerService,
        IFilePersistenceService filePersistenceService)
    {
        _settings = settingsMonitor.CurrentValue;
        DbContext = dbContext;
        HttpClient = httpClient;
        _settingsMonitor = settingsMonitor;
        _logger = logger;
        _stringMatcher = stringMatcher;
        _titleCleaner = courseTitleCleanerService;
        _filePersistenceService = filePersistenceService;

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

    protected override ILogger Logger => _logger;

    protected override IFilePersistenceService FilePersistence => _filePersistenceService;

    public override void Reset()
    {
        _settings = _settingsMonitor.CurrentValue;
        _logger.LogWarning("Resetting");
        // TODO reset
    }

    protected override async Task<IEnumerable<GetNoticeBoardNoticeDto>> GetNextDtoAsync(CancellationToken cancellationToken)
    {
        var resultDtos = new List<GetNoticeBoardNoticeDto>();

        foreach (var boardId in _settings.BoardIds)
        {
            _logger.LogInformation("Fetching Notice Board {@NoticeBoardId}", boardId);

            var dtos = await HttpClient.GetFromJsonAsync<IEnumerable<GetNoticeBoardNoticeDto>>(GetBoardNoticesRoute(boardId), cancellationToken);

            if (dtos is null)
            {
                _logger.LogWarning("No notices found for board {@NoticeBoardId}", boardId);
                continue;
            }

            _logger.LogInformation("Fetched NoticeBoard {@NoticeBoardId}", boardId);
            resultDtos.AddRange(dtos);
        }

        IsFinished = true;

        return resultDtos;
    }

    private static string GetBoardNoticesRoute(string boardId)
    {
        return $"/api/public/oglasne-ploce/{boardId}";
    }

    private static string GetAttachmentDownloadRoute(string attachmentId)
    {
        return $"/api/public/oglasi/{attachmentId}/download";
    }

    private string GetAbsoluteUrl(string route)
    {
        return $"{_settings.BaseUrl}{route}";
    }

    protected override async Task<IEnumerable<Notice>> MapDtoAsync(CancellationToken cancellationToken)
    {
        var notices = new List<Notice>();
        var externalSystem = await InitializeExternalSystemAsync(cancellationToken);
        if (externalSystem is null)
        {
            _logger.LogError("External system {@ExternalSystemName} not initialized", Constants.NoticeBoardSystemName);
            return notices;
        }

        var courseNameIdDict = await InitializeCourseNamesDictAsync(cancellationToken);
        var yearNameIdDict = await InitializeStudyYearsDictAsync(cancellationToken);

        var currentDtos = CurrentDtos;
        var dtoIds = currentDtos.Select(d => d.Id);

        _logger.LogInformation("Mapping DTOs {@DtoIds}", dtoIds);

        foreach (var dto in currentDtos)
        {
            var noticeBoardName = dto.NoticeBoard.BoardName;
            var studyYearName = _settings.BoardNameToStudyYearMapping.GetValueOrDefault(noticeBoardName, string.Empty);
            if (string.IsNullOrWhiteSpace(studyYearName))
            {
                _logger.LogError("No study year name found for board {@NoticeBoardName}", noticeBoardName);
                continue;
            }

            var studyYear = await GetStudyYearByShortNameAsync(studyYearName, cancellationToken);
            if (studyYear is null)
            {
                _logger.LogError("No study year found for name {@StudyYearName}", studyYearName);
                continue;
            }

            var title = dto.Title;
            var cleanedTitle = _titleCleaner.Cleanup(title);

            var (percentage, courseName, courseId) = GetHighestMatchPercentage(cleanedTitle, courseNameIdDict);

            Notice notice;
            bool isCourseNotice = percentage >= _settings.MinTitleMatchPercentage;
            if (isCourseNotice)
            {
                notice = new CourseNotice(dto.Id, GetAbsoluteUrl(GetBoardNoticesRoute(dto.NoticeBoard.Id.ToString())), dto.Body, dto.Title, dto.Author, true, courseId, null, externalSystem.Id, dto.CreatedDate.UtcDateTime, dto.ExpiryDate.ToUniversalTime());
            }
            else
            {
                notice = new StudyYearNotice(dto.Id, GetAbsoluteUrl(GetBoardNoticesRoute(dto.NoticeBoard.Id.ToString())), dto.Body, dto.Title, dto.Author, true, null, externalSystem.Id, dto.CreatedDate.UtcDateTime, dto.ExpiryDate.ToUniversalTime(), studyYear.Id);
            }

            foreach (var attachment in dto.Attachments)
            {
                var attachmentId = attachment.Id;
                var attachmentDownloadUrl = GetAttachmentDownloadRoute(attachmentId.ToString());

                string downloadedAttachmentUri;
                string hash;
                string hashAlgo;
                try
                {
                    using var webStream = await HttpClient.GetStreamAsync(attachmentDownloadUrl, cancellationToken);
                    downloadedAttachmentUri = await PersistFileAsync(webStream, cancellationToken);
                    (hash, hashAlgo) = await _filePersistenceService.GetFileHashAsync(downloadedAttachmentUri, cancellationToken);
                }
                catch (HttpRequestException ex)
                {
                    _logger.LogError(ex, "Error downloading attachment {@AttachmentId}", attachmentId);
                    continue;
                }

                if (!isCourseNotice)
                    courseId = null;

                var document = new Document(attachmentId, GetAbsoluteUrl(GetAttachmentDownloadRoute(attachmentId.ToString())), null, attachment.Title, dto.Author, true, courseId, null, externalSystem.Id, notice.ExternalCreatedDate, attachment.FileName, downloadedAttachmentUri, attachment.ByteSize, hash, hashAlgo, null, null);
                notice.TryAddDocument(document);
            }
            notices.Add(notice);
        }

        _logger.LogInformation("Mapped DTOs {@DtoIds}", currentDtos.Select(d => d.Id));

        return notices;
    }

    private async Task<string> PersistFileAsync(Stream webStream, CancellationToken cancellationToken)
    {
        var downloadedAttachmentUri = await _filePersistenceService.DownloadFileAsync(webStream, persistenceKey, cancellationToken);
        return downloadedAttachmentUri;
    }

    private async Task<ExternalSystem> InitializeExternalSystemAsync(CancellationToken cancellationToken)
    {
        var externalSystem = await DbContext.ExternalSystems.FirstOrDefaultAsync(es => es.Name == Constants.NoticeBoardSystemName, cancellationToken);
        return externalSystem;
    }

    private async Task<StudyYear?> GetStudyYearByShortNameAsync(string studyYearName, CancellationToken cancellationToken)
    {
        var studyYear = await DbContext.StudyYears.FirstOrDefaultAsync(sy => sy.ShortName == studyYearName, cancellationToken);

        return studyYear;
    }

    private async Task<Dictionary<string, Guid>> InitializeStudyYearsDictAsync(CancellationToken cancellationToken)
    {
        var dto = await DbContext.StudyYears
            .AsNoTracking()
            .Select(sy => new { sy.ShortName, sy.Id })
            .ToListAsync(cancellationToken);

        var dict = new Dictionary<string, Guid>(dto.Select(y => new KeyValuePair<string, Guid>(y.ShortName, y.Id)));

        return dict;
    }

    private (double?, string?, Guid?) GetHighestMatchPercentage(string noticeTitle, Dictionary<string, Guid> courseNameIdDict)
    {
        // TODO cache comparison results in dict
        (double? maxPercentage, string? maxPercentageCourseName, Guid? maxPercentageCourseId) = (null, null, null);
        foreach (var nameIdPair in courseNameIdDict)
        {
            var courseName = nameIdPair.Key;
            var courseId = nameIdPair.Value;

            // TODO cache cleanup result
            var cleanedCourseName = _titleCleaner.Cleanup(courseName);
            var percentage = _stringMatcher.GetMatchPercentage(noticeTitle, cleanedCourseName);
            if (percentage > (maxPercentage ?? 0))
            {
                maxPercentage = percentage;
                maxPercentageCourseName = courseName;
                maxPercentageCourseId = courseId;
            }
        }

        return (maxPercentage, maxPercentageCourseName, maxPercentageCourseId);
    }

    private async Task<Dictionary<string, Guid>> InitializeCourseNamesDictAsync(CancellationToken cancellationToken)
    {
        var dto = await DbContext.Courses
            .AsNoTracking()
            .Select(c => new { c.FullName, c.Id }).ToListAsync(cancellationToken);

        var dict = new Dictionary<string, Guid>(dto.Select(c => new KeyValuePair<string, Guid>(c.FullName, c.Id)));

        return dict;
    }

    private void InitializeCourseNames()
    {
        throw new NotImplementedException();
    }
}