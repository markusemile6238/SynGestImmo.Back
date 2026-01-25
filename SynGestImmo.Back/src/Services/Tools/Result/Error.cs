namespace Tools.Result
{
    public class Error
    {
        

        public string ErrorMessage { get;}
        public string ErrorCode { get;}
        public int? StatusCode { get;}

        private Error(string errorCode, string message, int? statusCode = null)
        {
            ErrorCode = errorCode;
            ErrorMessage = message;
            StatusCode = statusCode;
        }

        public static Error NotFound(string message)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(message);
            return new Error("NOT_FOUND",message,404);
        }

        public static Error Validation(string message) 
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(message);
            return new Error("VALIDATION_ERROR", message, 400);
        }
        public static Error Unauthorized(string message) 
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(message);
            return new Error("UNAUTHORIZED", message, 401);
        }
        public static Error Conflit(string message) 
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(message);
            return new Error("CONFLIT", message, 409);
        }
        public static Error Database(string message) 
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(message);
            return new Error("DATABASE_ERROR", message, 500);
        }




    }
}
