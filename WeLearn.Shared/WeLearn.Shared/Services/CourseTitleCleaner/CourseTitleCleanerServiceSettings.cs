namespace WeLearn.Shared.Services.CourseTitleCleaner;

public class CourseTitleCleanerServiceSettings
{
    public Dictionary<string,string> Regexes { get; set; }
    public bool ShouldConvertToLatin { get; set; }
    public bool ShouldConvertToCyrillic { get; set; }
}