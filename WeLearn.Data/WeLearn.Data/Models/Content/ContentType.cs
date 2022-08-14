using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeLearn.Data.Models.Content;

public enum ContentType
{
    Unknown = 0,
    Notice = 1,
    Post = 2,
    Document = 3,
    StudyMaterial = 4,
    NoticeGeneral = 5,
    NoticeStudyYear = 6,
    NoticeCourse = 7,
    DocumentContainer = 8,
    
}

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
}
