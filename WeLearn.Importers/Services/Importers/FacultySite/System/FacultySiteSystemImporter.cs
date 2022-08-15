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
    public override string Name => "Faculty Site";
    private List<IFacultySiteImporter> importers;

    public FacultySiteSystemImporter(IEnumerable<IFacultySiteImporter> importers)
    {
        this.importers = importers?.ToList() ?? new();
    }

    public override IEnumerable<IContentImporter> ContentImporters => importers;

}
