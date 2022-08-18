using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace WeLearn.Shared.Exceptions.Models;

[Serializable]
public class StudyYearAlreadyExistsException : Exception
{
    public StudyYearAlreadyExistsException()
    {
    }

    public StudyYearAlreadyExistsException(string? message) : base(message)
    {
    }

    public StudyYearAlreadyExistsException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    protected StudyYearAlreadyExistsException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}
