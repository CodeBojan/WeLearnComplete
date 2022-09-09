namespace WeLearn.Data.Models.Content;

// TODO move
public static class ContentTypeExtensions
{
    public static string Value(this ContentType contentType)
    {
        switch (contentType)
        {
            case ContentType.Notice:
                return "Notice";
            case ContentType.Post:
                return "Post";
            case ContentType.Document:
                return "Document";
            case ContentType.StudyMaterial:
                return "StudyMaterial";
            case ContentType.NoticeGeneral:
                return "NoticeGeneral";
            case ContentType.NoticeStudyYear:
                return "NoticeStudyYear";
            case ContentType.NoticeCourse:
                return "NoticeCourse";
            case ContentType.DocumentContainer:
                return "DocumentContainer";
            default:
                throw new NotImplementedException();
        }
    }

    public static string FriendlyName(this ContentType contentType)
    {
        switch (contentType)
        {
            case ContentType.Unknown:
                return "content";
            case ContentType.Notice:
                return "notice";
            case ContentType.Post:
                return "post";
            case ContentType.Document:
                return "document";
            case ContentType.StudyMaterial:
                return "study material";
            case ContentType.NoticeGeneral:
                return "general notice";
            case ContentType.NoticeStudyYear:
                return "study year notice";
            case ContentType.NoticeCourse:
                return "course notice";
            case ContentType.DocumentContainer:
                return "document container";
            default:
                throw new NotImplementedException();
        }
    }
}
