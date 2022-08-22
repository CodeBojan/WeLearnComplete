namespace WeLearn.Api.Dtos.Document;

public class PostDocumentDto
{
    public string? Body { get; set; }
    public string? Title { get; set; }
    public string? Author { get; set; }
    public Guid CourseId { get; set; }
    public string? Version { get; set; }
}
