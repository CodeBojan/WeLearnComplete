using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeLearn.Importers.Services.Importers.Content;

namespace WeLearn.Importers.Services.System;

// TODO abstract class that implements reset importers
public interface ISystemImporter
{
    public bool IsEnabled { get; }
    public string Name { get; }
    public IEnumerable<IContentImporter> ContentImporters { get; }
    public void ResetImporters();
}
