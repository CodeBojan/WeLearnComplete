using WeLearn.Importers.Services.Importers.NoticeBoard.Dtos;

namespace WeLearn.Importers.Services.Importers.NoticeBoard.Content;

public class NoticeBoardNoticeImporterSettings
{
    public string FriendlyExternalUrl { get; set; }
    public string BaseUrl { get; set; }
    public string[] BoardIds { get; set; }
    public double MinTitleMatchPercentage { get; set; }
    public Dictionary<NoticeBoardBoardName, string> BoardNameToStudyYearMapping { get; set; }
    public string ExternalSystemName { get; set; }
}