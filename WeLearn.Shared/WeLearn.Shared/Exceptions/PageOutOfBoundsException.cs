using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace WeLearn.Shared.Exceptions;

[Serializable]
public class PageOutOfBoundsException : Exception
{
    private int page;

    public PageOutOfBoundsException()
    {
    }

    public PageOutOfBoundsException(int page)
    {
        this.page = page;
    }

    public PageOutOfBoundsException(string? message) : base(message)
    {
    }

    public PageOutOfBoundsException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    protected PageOutOfBoundsException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}