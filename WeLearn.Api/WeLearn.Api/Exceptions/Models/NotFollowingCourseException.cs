using System;
using System.Runtime.Serialization;

namespace WeLearn.Api.Exceptions.Models;

[Serializable]
public class NotFollowingCourseException : Exception
{
    public NotFollowingCourseException()
    {
    }

    public NotFollowingCourseException(string? message) : base(message)
    {
    }

    public NotFollowingCourseException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    protected NotFollowingCourseException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}
