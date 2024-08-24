using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Threelab.Domain.Abstracts
{
    public abstract class ResultObject : Exception
    {
        public bool IsError { get; set; } = false;
        public string Message { get; set; }
    }

    public class SuccessResult<T> : ResultObject
    {
        public T? Data { get; set; }
        public int StatusCode { get; set; }

        public SuccessResult(T? data, int statusCode, string message = "Successful")
        {
            Message = message;
            Data = data;
            StatusCode = statusCode;
        }
    }

    public class FailedResult : ResultObject
    {
        public int StatusCode { get; set; }

        public FailedResult(string message, int statusCode)
        {
            IsError = true;
            Message = message;
            StatusCode = statusCode;
        }
    }
}
