using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeLearn.Importers.Services.File;

public interface IFilePersistenceService
{
    Task<string> DownloadFileAsync(Stream stream, string key, CancellationToken cancellationToken);
    Task<(string, string)> GetFileHashAsync(string uri, CancellationToken cancellationToken);
    Task<Stream> GetDocumentStreamAsync(string uri);
}
