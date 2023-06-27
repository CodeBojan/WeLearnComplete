using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeLearn.Data.Models.Content;

public class Document : Content
{
    public Document(
        string? externalId,
        string? externalUrl,
        string? body,
        string? title,
        string? author,
        bool isImported,
        Guid? courseId,
        Guid? creatorId,
        Guid? externalSystemId,
        DateTime? externalCreatedDate,
        string fileName,
        string uri,
        string? version,
        long? size,
        string? hash,
        string? hashAlgorithm,
        Guid? documentContainerId,
        Guid? courseMaterialUploadRequestId,
        string fileExtension) : base(
            externalId,
            externalUrl,
            body,
            title,
            author,
            isImported,
            courseId,
            ContentType.Document.Value(),
            creatorId,
            externalSystemId,
            externalCreatedDate)
    {
        FileName = fileName;
        Uri = uri;
        Version = version;
        Size = size;
        Hash = hash;
        HashAlgorithm = hashAlgorithm;
        DocumentContainerId = documentContainerId;
        CourseMaterialUploadRequestId = courseMaterialUploadRequestId;
        FileExtension = fileExtension;
    }

    public string FileName { get; set; }
    public string Uri { get; set; }
    public string? Version { get; set; }
    public long? Size { get; set; }
    public string? Hash { get; set; }
    public string? HashAlgorithm { get; set; }
    public Guid? DocumentContainerId { get; set; }
    public Guid? CourseMaterialUploadRequestId { get; set; }
    public string FileExtension { get; private set; }

    public virtual DocumentContainer? DocumentContainer { get; set; }
    public virtual CourseMaterialUploadRequest? CourseMaterialUploadRequest { get; set; }

    // TODO update method
}
