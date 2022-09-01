namespace WeLearn.Api.Dtos.ExternalSystem;

public class GetExternalSystemDto
{
    public Guid Id { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }
    public string Name { get; set; }
    public string? FriendlyName { get; set; }
    public string? Url { get; set; }
    public string? LogoUrl { get; set; }
}
