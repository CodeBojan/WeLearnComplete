using System.Runtime.Serialization;

namespace WeLearn.Api.Exceptions.Models;

[Serializable]
public class CourseMaterialUploadRequestNotFoundException : Exception
{
    public CourseMaterialUploadRequestNotFoundException()
    {
    }

    public CourseMaterialUploadRequestNotFoundException(string? message) : base(message)
    {
    }

    public CourseMaterialUploadRequestNotFoundException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    protected CourseMaterialUploadRequestNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}
