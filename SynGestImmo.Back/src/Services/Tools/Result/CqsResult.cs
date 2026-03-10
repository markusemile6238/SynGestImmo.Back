

namespace Tools.Result
{
    public class CqsResult
    {
        public bool IsSuccess { get; init; }
        public int StatusCode { get; init; }
        
        public string? Message { get; init; }
        public CqsError? Error { get; init; }

        public static CqsResult Success(int statusCode = 200)
            => new() { IsSuccess = true, StatusCode = statusCode };
        public static CqsResult Success(string? message="")
        {
            return
                new()
                {                    
                    IsSuccess = true,
                    StatusCode = 200,
                    Message = message

                };
                
        }

        public static CqsResult Failure(CqsError error)
            => new() { IsSuccess = false, StatusCode = error.StatusCode, Error = error };
    }

    public class CqsResult<T> : CqsResult
    {

        public T? Data { get; init; }


        public static CqsResult<T> Success(T data)
            => new() { IsSuccess = true, StatusCode = 200, Data = data };
        public static CqsResult<T> Success(string message="success")
            => new() { IsSuccess = true, StatusCode = 200, Message=message };
        public static CqsResult<T> Failure(CqsError error)
            => new() { IsSuccess = false, StatusCode = error.StatusCode, Error = error };

    }

    public class CqsError
    {
        public string Code { get; init; } = string.Empty;
        public string Message { get; init; } = string.Empty;
        public int StatusCode { get; init; }    
        public Object? Details { get; init; }
    }
}