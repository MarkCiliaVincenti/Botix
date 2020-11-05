using System;
using Botix.Common.API.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Botix.Common.API.Filters
{
    public class ApiExceptionFilter : ExceptionFilterAttribute
    {
        /// <summary>Processing API methods exceptions.</summary>
        /// <param name="context">Exception context.</param>
        public override void OnException(ExceptionContext context)
        {
            dynamic response;
            switch (context.Exception)
            {
                case UnauthorizedAccessException exception:
                    response = new
                    {
                        StatusCode = StatusCodes.Status404NotFound,
                        exception.Message
                    };
                    break;
                case OperationCanceledException exception:
                    response = new
                    {
                        StatusCode = StatusCodes.Status205ResetContent,
                        exception.Message
                    };
                    break;
                case WebApiException exception:
                    response = new
                    {
                        exception.StatusCode,
                        exception.Message
                    };
                    break;
                default:
                    response = new
                    {
                        StatusCode = StatusCodes.Status500InternalServerError,
                        Message = "Internal server error"
                    };
                    break;
            }
            context.HttpContext.Response.StatusCode = (int)response.StatusCode;
            context.Result = new JsonResult(response);
            context.ExceptionHandled = true;
        }
    }
}
