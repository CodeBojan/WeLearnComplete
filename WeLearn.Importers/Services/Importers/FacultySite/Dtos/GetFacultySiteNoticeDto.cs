namespace WeLearn.Importers.Services.Importers.FacultySite.Dtos;

public class GetFacultySiteNoticeDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime PublishedDate { get; set; }
    public IEnumerable<GetFacultySiteNoticeAttachmentDto> Attachments { get; set; }
}