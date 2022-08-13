using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeLearn.Importers.Services.File;

public class FileSystemPersistenceService : IFileSystemPersistenceService
{
    private readonly FileSystemPersistenceServiceSettings _settings;

    public FileSystemPersistenceService(IOptions<FileSystemPersistenceServiceSettings> settings)
    {
        _settings = settings.Value;
    }

    public async Task<string> DownloadFileAsync(Stream stream, string key, CancellationToken cancellationToken)
    {
        var directoryPath = Path.Join(_settings.BasePath, key);
        if (!System.IO.File.Exists(directoryPath))
            Directory.CreateDirectory(directoryPath);

        var fileName = $"{Guid.NewGuid()}.bin";
        var filePath = Path.Join(directoryPath, fileName);
        using var fs = new FileStream(filePath, FileMode.CreateNew);
        await stream.CopyToAsync(fs, cancellationToken);

        return filePath;
    }
}
