using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeLearn.Importers.Services.Importers.Content;
using WeLearn.Importers.Services.Importers.System;

namespace WeLearn.Importers.Services.Importers.FacultySite.System;

public class FacultySiteSystemImporter : SystemImporter, IFacultySiteSystemImporter
{
    private readonly SystemImporterSettings _settings;
    public override string Name => "Faculty Site";
    private List<IFacultySiteImporter> importers;

    public FacultySiteSystemImporter(
        IEnumerable<IFacultySiteImporter> importers,
         IOptions<SystemImporterSettings> options)
    {
        this.importers = importers?.ToList() ?? new();
        _settings = options.Value;

        IsEnabled = _settings.IsEnabled;
    }

    public override IEnumerable<IContentImporter> ContentImporters => importers;
}
