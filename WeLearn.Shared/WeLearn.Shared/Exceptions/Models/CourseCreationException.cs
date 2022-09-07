using System.Runtime.Serialization;

namespace WeLearn.Shared.Exceptions.Models;

[Serializable]
public class CourseCreationException : Exception
{
    public CourseCreationException()
    {
    }

    public CourseCreationException(string? message) : base(message)
    {
    }

    public CourseCreationException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    protected CourseCreationException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}
