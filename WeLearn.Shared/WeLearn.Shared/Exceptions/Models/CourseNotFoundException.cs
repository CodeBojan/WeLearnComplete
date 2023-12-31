﻿using System.Runtime.Serialization;

namespace WeLearn.Shared.Exceptions.Models;

[Serializable]
public class CourseNotFoundException : Exception
{
    public CourseNotFoundException()
    {
    }

    public CourseNotFoundException(string message) : base(message)
    {
    }

    public CourseNotFoundException(string message, Exception innerException) : base(message, innerException)
    {
    }

    protected CourseNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}
