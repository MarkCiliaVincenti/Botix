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
            dynamic response = context.Exception switch
            {
                UnauthorizedAccessException exception => new
                    {StatusCode = StatusCodes.Status404NotFound, exception.Message},
                OperationCanceledException exception => new
                    {StatusCode = StatusCodes.Status205ResetContent, exception.Message},
                WebApiException exception => new {exception.StatusCode, exception.Message},

                _ => new {StatusCode = StatusCodes.Status500InternalServerError, Message = "Internal server error"}
            };
            context.HttpContext.Response.StatusCode = (int)response.StatusCode;
            context.Result = new JsonResult(response);
            context.ExceptionHandled = true;
        }
    }
}
