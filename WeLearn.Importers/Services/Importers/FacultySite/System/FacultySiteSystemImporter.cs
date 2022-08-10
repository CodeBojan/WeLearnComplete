using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeLearn.Importers.Services.Importers.Content;

namespace WeLearn.Importers.Services.Importers.FacultySite.System;

public class FacultySiteSystemImporter : IFacultySiteSystemImporter
{
    public IEnumerable<IContentImporter> ContentImporters { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    public string Name => "Faculty Site";

    public void ResetImporters()
    {
        throw new NotImplementedException();
    }
}
