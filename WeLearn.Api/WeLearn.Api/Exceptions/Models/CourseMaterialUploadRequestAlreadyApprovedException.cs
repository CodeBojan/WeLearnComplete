using System.Runtime.Serialization;

namespace WeLearn.Api.Exceptions.Models;

[Serializable]
public class CourseMaterialUploadRequestAlreadyApprovedException : Exception
{
    public CourseMaterialUploadRequestAlreadyApprovedException()
    {
    }

    public CourseMaterialUploadRequestAlreadyApprovedException(string? message) : base(message)
    {
    }

    public CourseMaterialUploadRequestAlreadyApprovedException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    protected CourseMaterialUploadRequestAlreadyApprovedException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}
