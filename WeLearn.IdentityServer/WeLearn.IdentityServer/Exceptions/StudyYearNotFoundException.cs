using System.Runtime.Serialization;

namespace WeLearn.IdentityServer.Exceptions;

[Serializable]
public class StudyYearNotFoundException : Exception
{
    public StudyYearNotFoundException()
    {
    }

    public StudyYearNotFoundException(string message) : base(message)
    {
    }

    public StudyYearNotFoundException(string message, Exception innerException) : base(message, innerException)
    {
    }

    protected StudyYearNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}
