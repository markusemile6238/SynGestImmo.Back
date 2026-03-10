using Tools.Result;


namespace Entity.Service.Domain.ExceptionService
{
    public class EntityServiceExceptions : Exception, IServiceException
    {
        public string ErrorCode { get; } = String.Empty;
        public int StatusCode { get; } = 400;



        public  EntityServiceExceptions()
        {
        }

        public  EntityServiceExceptions(string? message) : base(message)
        {
        }

        public  EntityServiceExceptions(string? message, Exception? innerException)
            : base(message, innerException)
        {
        }

        public  EntityServiceExceptions(string errorCode, string? message)
            : base(message)
        {
            ErrorCode = errorCode;
        }

        public  EntityServiceExceptions(string errorCode, string? message, int statusCode)
            : base(message)
        {
            ErrorCode = errorCode;
            StatusCode = statusCode;
        }

        public  EntityServiceExceptions(string errorCode, string? message, Exception? innerException)
            : base(message, innerException)
        {
            ErrorCode = errorCode;
        }
    }
}
