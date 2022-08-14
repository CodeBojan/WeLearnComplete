using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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

    public async Task<(string, string)> GetFileHashAsync(string uri, CancellationToken cancellationToken)
    {
        if (!System.IO.File.Exists(uri))
            throw new FileNotFoundException(uri);

        using var algo = SHA256.Create();
        using var fs = new FileStream(uri, FileMode.Open);
        var hashBytes = await algo.ComputeHashAsync(fs, cancellationToken);

        var hash = BitConverter.ToString(hashBytes).Replace("-", string.Empty).ToUpper();
        return (hash, nameof(SHA256));
    }
}
