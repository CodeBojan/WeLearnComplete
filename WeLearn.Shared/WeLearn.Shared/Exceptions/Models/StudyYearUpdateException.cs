﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace WeLearn.Shared.Exceptions.Models;

[Serializable]
public class StudyYearUpdateException : Exception
{
    public StudyYearUpdateException()
    {
    }

    public StudyYearUpdateException(string? message) : base(message)
    {
    }

    public StudyYearUpdateException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    protected StudyYearUpdateException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}
