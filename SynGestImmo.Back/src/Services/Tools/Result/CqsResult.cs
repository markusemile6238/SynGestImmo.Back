using Identity.Service.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
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
        public string? ErrorCode { get; }
        public int? StatusCode { get; }
        public TResult Data { get; }


        public CqsResult(bool isSuccess, TResult data,string? errorMessage=null, string? errorCode=null, int? statusCode=null)
        {
            IsSuccess = isSuccess;
            Data = data;
            ErrorMessage = errorMessage;
            ErrorCode = errorCode;
            StatusCode = statusCode;
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

        public static CqsResult<TResult> Failure(Error error) 
        { 
            if(error == null) throw new ArgumentNullException("Failure method nead and Error parameters");
            return new CqsResult<TResult>(false,default!,error.ErrorMessage,error.ErrorCode,error.StatusCode);
        }

        public static implicit operator CqsResult<TResult>(Error error)
        {
            if(error == null) throw new ArgumentNullException("Failure method nead and Error parameters");
            return Failure(error);
        }

        public static implicit operator CqsResult<TResult>(IdentityServiceException ex)
        {
            return new CqsResult<TResult>(false, default!, ex.Message, ex.ErrorCode, ex.StatusCode);
        }

    }
}
