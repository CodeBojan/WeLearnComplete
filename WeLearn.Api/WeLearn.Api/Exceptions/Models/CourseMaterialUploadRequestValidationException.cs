using System.Runtime.Serialization;

namespace WeLearn.Api.Exceptions.Models;

[Serializable]
public class CourseMaterialUploadRequestValidationException : Exception
{
    public CourseMaterialUploadRequestValidationException()
    {
    }

    public CourseMaterialUploadRequestValidationException(string? message) : base(message)
    {
    }

    public CourseMaterialUploadRequestValidationException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    protected CourseMaterialUploadRequestValidationException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}
