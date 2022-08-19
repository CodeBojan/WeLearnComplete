using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace WeLearn.Shared.Exceptions.Models;

[Serializable]
public class AccountNotFoundException : Exception
{
    public AccountNotFoundException()
    {
    }

    public AccountNotFoundException(string? message) : base(message)
    {
    }

    public AccountNotFoundException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    protected AccountNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}
