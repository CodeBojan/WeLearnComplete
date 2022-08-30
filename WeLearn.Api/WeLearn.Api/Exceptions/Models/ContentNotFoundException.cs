using System.Runtime.Serialization;

namespace WeLearn.Api.Exceptions.Models;

[Serializable]
public class ContentNotFoundException : Exception
{
    public ContentNotFoundException()
    {
    }

    public ContentNotFoundException(string? message) : base(message)
    {
    }

    public ContentNotFoundException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    protected ContentNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}
