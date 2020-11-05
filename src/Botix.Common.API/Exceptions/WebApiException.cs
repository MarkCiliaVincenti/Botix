using System;
using System.Net;

namespace Botix.Common.API.Exceptions
{
    public class WebApiException : Exception
    {
        public WebApiException(HttpStatusCode statusCode, string message)
        {
            StatusCode = statusCode;
            Message = message;
        }

        public HttpStatusCode StatusCode { get; }
        public string Message { get; }
    }
}
