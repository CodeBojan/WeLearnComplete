using System.Runtime.Serialization;

namespace WeLearn.Shared
{
    [Serializable]
    internal class CredentialsNotFoundException : Exception
    {
        public CredentialsNotFoundException()
        {
        }

        public CredentialsNotFoundException(string? message) : base(message)
        {
        }

        public CredentialsNotFoundException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected CredentialsNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}