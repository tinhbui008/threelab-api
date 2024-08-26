using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Threelab.Domain.Abstracts
{
    public abstract class ResultObject
    {
        public bool Success { get; set; } = true;
        public string Message { get; set; }
        public int StatusCode { get; set; }
        public ResultObject(string message, int status, bool success)
        {
            Message = message;
            StatusCode = status;
            Success = success;
        }
    }

    public class SuccessResult<T> : ResultObject
    {
        public object Metadata { get; set; }
        public SuccessResult(string message, int status, bool success, object data) : base(message, status, success)
        {
            Message = message;
            StatusCode = status;
            Metadata = data;
        }
    }

    public class FailedResult : ResultObject
    {
        public FailedResult(string message, int status, bool success) : base(message, status, success)
        {
            Message = message;
            StatusCode = status;
        }
    }

    //public class ExceptionResult : ExceptionObject
    //{
    //    public ExceptionResult(string message, int status) : base(message, status)
    //    {
    //    }
    //}
}
