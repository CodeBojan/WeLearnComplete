using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeLearn.Importers.Services.Importers.Content;
using WeLearn.Importers.Services.System;

namespace WeLearn.Importers.Services.Importers.System;

// TODO add isenabled and monitor from IOptionsMonitor of settings
public abstract class SystemImporter : ISystemImporter
{
    public bool IsEnabled { get; set; }
    public abstract string Name { get; }
    public abstract IEnumerable<IContentImporter> ContentImporters { get; }
    public void ResetImporters()
    {
        foreach (var importer in ContentImporters)
            importer.Reset();
    }
}
