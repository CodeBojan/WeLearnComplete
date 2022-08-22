namespace WeLearn.Api.Dtos.Document;

public class GetDocumentDto
{
    public Guid Id { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }
    public string? ExternalId { get; set; }
    public string? ExternalUrl { get; set; }
    public string? Body { get; set; }
    public string? Title { get; set; }
    public string? Author { get; set; }
    public bool IsImported { get; set; }
    public Guid? CourseId { get; set; }
    public Guid? CreatorId { get; set; }
    public Guid? ExternalSystemId { get; set; }
    public DateTime? ExternalCreatedDate { get; set; }
    public string FileName { get; set; }
    public string Uri { get; set; }
    public string? Version { get; set; }
    public long? Size { get; set; }
    public string? Hash { get; set; }
    public string? HashAlgorithm { get; set; }
    public Guid? DocumentContainerId { get; set; }
    public Guid? CourseMaterialUploadRequestId { get; set; }
    public string FileExtension { get; set; }
}
