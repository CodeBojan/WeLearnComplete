using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeLearn.Data.Models.Content;

public class Document : Content
{
    public Document(
        long? externalId,
        string? externalUrl,
        string body,
        string title,
        string? author,
        bool isImported,
        Guid? courseId,
        Guid? creatorId,
        Guid? externalSystemId,
        string fileName,
        string uri,
        long? size,
        string? hash,
        string? hashAlgorithm,
        Guid? studyMaterialId,
        Guid? courseMaterialUploadRequestId) : base(
            externalId,
            externalUrl,
            body,
            title,
            author,
            isImported,
            courseId,
            ContentType.Document.Value(),
            creatorId,
            externalSystemId)
    {
        FileName = fileName;
        Uri = uri;
        Size = size;
        Hash = hash;
        HashAlgorithm = hashAlgorithm;
        StudyMaterialId = studyMaterialId;
        CourseMaterialUploadRequestId = courseMaterialUploadRequestId;
    }

    public string FileName { get; set; }
    public string Uri { get; set; }
    public long? Size { get; set; }
    public string? Hash { get; set; }
    public string? HashAlgorithm { get; set; }
    public Guid? StudyMaterialId { get; set; }
    public Guid? CourseMaterialUploadRequestId { get; set; }

    public virtual StudyMaterial? StudyMaterial { get; set; }
    public virtual CourseMaterialUploadRequest? CourseMaterialUploadRequest { get; set; }
}
