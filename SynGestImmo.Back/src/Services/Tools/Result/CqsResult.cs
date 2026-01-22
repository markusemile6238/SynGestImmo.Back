using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tools.Result
{
    public class CqsResult
    {

        public bool IsSuccess { get; }
        public bool IsFailure => !IsSuccess;
        public string? ErrorMessage { get; }

        public CqsResult(bool isSuccess, string? errorMessage=null)
        {
            IsSuccess = isSuccess;
            ErrorMessage = errorMessage;
        }

        public static CqsResult Success()
        {
            return new CqsResult(true);
        }

        public static CqsResult Failure(string ErrorMessage) 
        { 
            ArgumentException.ThrowIfNullOrWhiteSpace(ErrorMessage);
            return new CqsResult(false, ErrorMessage);
        }

        public static implicit operator CqsResult(Error error)
        {
            return Failure(error.ErrorMessage);
        }

        public static implicit operator CqsResult(Exception ex)
        {
            return Failure(ex.Message);
        }

    }

    public class CqsResult<TResult>
    {

        public bool IsSuccess { get; }
        public bool IsFailure => !IsSuccess;
        public string? ErrorMessage { get; }

        public TResult Data { get; }

        public CqsResult(bool isSuccess,TResult data, string? errorMessage=null)
        {
            IsSuccess = isSuccess;
            ErrorMessage = errorMessage;
            Data = data;
        }

        public static CqsResult<TResult> Success(TResult data)
        {
            return new CqsResult<TResult>(true,data);
        }

        public static CqsResult<TResult> Failure(string ErrorMessage) 
        { 
            ArgumentException.ThrowIfNullOrWhiteSpace(ErrorMessage);
            return new CqsResult<TResult>(false, default!,ErrorMessage);
        }

        public static implicit operator CqsResult<TResult>(Error error)
        {
            return Failure(error.ErrorMessage);
        }

        public static implicit operator CqsResult<TResult>(Exception ex)
        {
            return Failure(ex.Message);
        }

    }
}
