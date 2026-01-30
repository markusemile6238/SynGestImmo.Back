using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools.Result;

namespace Identity.Service.Domain.Exceptions
{


    public class IdentityServiceException : Exception, IServiceException
    {
        public string ErrorCode { get; } = String.Empty;
        public int StatusCode { get; } = 400;



        public IdentityServiceException()
        {
        }

        public IdentityServiceException(string? message) : base(message)
        {
        }

        public IdentityServiceException(string? message, Exception? innerException)
            : base(message, innerException)
        {
        }

        public IdentityServiceException(string errorCode, string? message)
            : base(message)
        {
            ErrorCode = errorCode;
        }

        public IdentityServiceException(string errorCode, string? message, int statusCode)
            : base(message)
        {
            ErrorCode = errorCode;
            StatusCode = statusCode;
        }

        public IdentityServiceException(string errorCode, string? message, Exception? innerException)
            : base(message, innerException)
        {
            ErrorCode = errorCode;
        }


    }
}
