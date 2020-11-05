using System;
using Microsoft.Extensions.Logging;

namespace Botix.Bot.Infrastructure
{
    public static class LoggerExtensions
    {
        public static IDisposable DebugLogingScope(this ILogger logger, string message, string methodName, params string[] parameters)
        {
            var parameter = parameters.Length == 0 ? string.Empty : string.Join(',', parameters);
            var method = $"Method: {methodName}";

            logger.LogDebug($"{method}; {parameter}; {message}");
            return new Dispose(logger, method);
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
                _logger.LogDebug($"{_methodName} is done");
            }
        }
    }
}
