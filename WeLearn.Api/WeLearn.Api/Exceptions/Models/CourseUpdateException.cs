using System.Runtime.Serialization;

namespace WeLearn.Api.Exceptions.Models;

[Serializable]
public class CourseUpdateException : Exception
{
    public CourseUpdateException()
    {
    }

    public CourseUpdateException(string? message) : base(message)
    {
    }

    public CourseUpdateException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    protected CourseUpdateException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}
