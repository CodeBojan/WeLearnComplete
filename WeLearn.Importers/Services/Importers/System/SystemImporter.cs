using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeLearn.Importers.Services.Importers.Content;
using WeLearn.Importers.Services.System;

namespace WeLearn.Importers.Services.Importers.System;

public abstract class SystemImporter : ISystemImporter
{
    public abstract string Name { get; }
    public abstract IEnumerable<IContentImporter> ContentImporters { get; }
    public void ResetImporters()
    {
        foreach (var importer in ContentImporters)
            importer.Reset();
    }
}
