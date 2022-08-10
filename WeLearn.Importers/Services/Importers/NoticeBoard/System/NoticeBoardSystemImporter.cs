using WeLearn.Importers.Services.Importers.Content;
using WeLearn.Importers.Services.Importers.NoticeBoard.Content;
using WeLearn.Importers.Services.Importers.System;
using WeLearn.Importers.Services.System;

namespace WeLearn.Importers.Services.Importers.NoticeBoard.System;

public class NoticeBoardSystemImporter : SystemImporter, INoticeBoardSystemImporter
{
    public override string Name => "Notice Board";
    private List<INoticeBoardImporter> importers;

    public NoticeBoardSystemImporter(IEnumerable<INoticeBoardImporter> importers)
    {
        this.importers = importers?.ToList() ?? new();
    }

    public override IEnumerable<IContentImporter> ContentImporters => importers;
}
