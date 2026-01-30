namespace Tools.Result
{
    public class Error
    {

        public static CqsError Validation(string message, Object? details=null)
         => new() { Code = "VALIDATION_ERROR", Message = message, StatusCode = 400, Details = details };

        public static CqsError NotFound(string message)
            => new() { Code = "NOT_FOUND", Message = message, StatusCode = 404 };

        public static CqsError Database(string message)
            => new() { Code = "DATABASE_ERROR", Message = message, StatusCode = 500 };

        public static CqsError Unknown(string message)
            => new() { Code = "UNKNOWN_ERROR", Message = message, StatusCode = 500 };




    }
}
