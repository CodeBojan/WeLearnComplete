using Microsoft.Extensions.Options;
using WeLearn.Importers.Services.Importers.Content;
using WeLearn.Importers.Services.Importers.NoticeBoard.Content;
using WeLearn.Importers.Services.Importers.System;
using WeLearn.Importers.Services.System;

namespace WeLearn.Importers.Services.Importers.NoticeBoard.System;

public class NoticeBoardSystemImporter : SystemImporter, INoticeBoardSystemImporter
{
    private readonly NoticeBoardSystemImporterSettings _settings;
    public override string Name => "Notice Board";
    private List<INoticeBoardImporter> importers;

    public NoticeBoardSystemImporter(IEnumerable<INoticeBoardImporter> importers, IOptionsSnapshot<NoticeBoardSystemImporterSettings> options)
    {
        this.importers = importers?.ToList() ?? new();
        _settings = options.Value;

        IsEnabled = _settings.IsEnabled;
    }

    public override IEnumerable<IContentImporter> ContentImporters => importers;
}
