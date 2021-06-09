using System;
using System.Runtime.Serialization;

namespace Micro.Service.Base.Exceptions
{
    [Serializable]
    public class ServiceException : Exception
    {
        public string Details { get; }
        public int? StatusCode { get; }

        public ServiceException(string message) : base(message) { }

        public ServiceException(string message, string details) : base(message)
        {
            Details = details;
        }

        public ServiceException(string message, string details, int statusCode) : base(message)
        {
            Details = details;
            StatusCode = statusCode;
        }

        public ServiceException(string message, Exception innerException) : base(message, innerException)
        {

        }

        protected ServiceException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
