using System.Runtime.Serialization;

namespace WeLearn.Api.Exceptions.Models;

[Serializable]
public class NotFollowingStudyYearException : Exception
{
    public NotFollowingStudyYearException()
    {
    }

    public NotFollowingStudyYearException(string? message) : base(message)
    {
    }

    public NotFollowingStudyYearException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    protected NotFollowingStudyYearException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}
