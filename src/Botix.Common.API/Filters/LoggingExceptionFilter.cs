using System;
using System.Net;
using Botix.Common.API.Exceptions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace Botix.Common.API.Filters
{
    public class LoggingExceptionFilter : ExceptionFilterAttribute
    {
        private ILogger _logger;

        /// <summary>
        /// Фильтр API для обработки исключений
        /// </summary>
        public LoggingExceptionFilter(ILogger<LoggingExceptionFilter> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Обработка исключений синхронных методов API
        /// </summary>
        /// <param name="context">Контекст исключения</param>
        public override void OnException(ExceptionContext context)
        {
            var request = context.HttpContext.Request;
            if (context.Exception is WebApiException exception)
            {
                var httpStatus = exception.StatusCode;

                switch (httpStatus)
                {
                    case HttpStatusCode.NotFound:
                        _logger.LogWarning($"Not found error while try access API method. URL: {request.Path}{request.QueryString}. Reason: {exception.Message}");
                        return;

                    case HttpStatusCode.UnprocessableEntity:
                        _logger.LogWarning($"Unprocessable error while try access API method. URL: {request.Path}{request.QueryString}. Reason: {exception.Message}");
                        return;
                }
            }

            _logger.LogError(context.Exception, $"Error while try access API method. URL: {request.Path}{request.QueryString}");
        }
    }
}
