using System;
using System.Linq;
using Microsoft.Extensions.Logging;

namespace Botix.Common.Logging.Context
{
    /// <summary>
    ///    Logger extension methods.
    /// </summary>
    public static class LoggerExtensions
    {
        /// <summary>
        ///     Creates logger scope with new correlation id.
        /// </summary>
        /// <param name="logger">The Microsoft.Extensions.Logging.ILogger to create the scope in.</param>
        /// <returns>A disposable scope object. Can be null.</returns>
        public static IDisposable BeginCorrelationScope(this ILogger logger)
        {
            var correlationId = Guid.NewGuid().ToString();
            CallContext.State = ContextProperties.Construct(builder => builder.CorrelationId(correlationId));

            return logger.BeginScope(null);
        }
        
        public static IDisposable DebugLoggingScope(this ILogger logger, string logMessage, string methodName, params string[] parameters)
        {
            var paramStringify = parameters?.Any() == true ? string.Join(", ", parameters) : null;

            var method = string.IsNullOrEmpty(methodName) ? null : $"Method: {methodName};";
            var param = string.IsNullOrEmpty(paramStringify) ? null : $" Parameters: {paramStringify};";
            var message = string.IsNullOrEmpty(logMessage) ? null : $" Message: {logMessage};";

            logger.Log(LogLevel.Debug, $"{method}{param}{message}");
            return new Dispose(logger, methodName);
        }

        private class Dispose : IDisposable
        {
            private readonly ILogger _logger;
            private readonly string _methodName;

            public Dispose(ILogger logger, string methodName)
            {
                _logger = logger ?? throw new ArgumentNullException(nameof(logger));
                _methodName = methodName ?? throw new ArgumentNullException(nameof(methodName));
            }

            void IDisposable.Dispose()
            {
                _logger.Log(LogLevel.Debug, $"{_methodName} is done");
            }
        }
    }
}
