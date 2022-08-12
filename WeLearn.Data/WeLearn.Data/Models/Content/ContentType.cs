using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeLearn.Data.Models.Content;

public enum ContentType
{
    Notice = 1,
    Post = 2,
    Document = 3,
    StudyMaterial = 4
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
            default:
                throw new NotImplementedException();
        }
    }
}
