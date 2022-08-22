using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace WeLearn.Importers.Exceptions;

[Serializable]
public class NotificationNotFoundException : Exception
{
    public NotificationNotFoundException()
    {
    }

    public NotificationNotFoundException(string? message) : base(message)
    {
    }

    public NotificationNotFoundException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    protected NotificationNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}
