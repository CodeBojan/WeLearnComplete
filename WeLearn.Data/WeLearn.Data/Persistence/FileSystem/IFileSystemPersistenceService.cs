using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeLearn.Importers.Services.File;

public interface IFileSystemPersistenceService
{
    Task<string> DownloadFileAsync(Stream stream, string key, CancellationToken cancellationToken);
}
