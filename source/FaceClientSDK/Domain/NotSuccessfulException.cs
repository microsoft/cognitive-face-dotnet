using System;
using System.Runtime.Serialization;

namespace FaceClientSDK.Domain
{
    [Serializable]
    public class NotSuccessfulException : Exception
    {
        public NotSuccessfulException()
        {
        }

        public NotSuccessfulException(string message) : base(message)
        {
        }

        public NotSuccessfulException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NotSuccessfulException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}