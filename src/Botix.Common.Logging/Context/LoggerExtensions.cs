using System;
using Microsoft.Extensions.Logging;

namespace STC.Common.Hosting.Context
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
            var corellationId = Guid.NewGuid().ToString();
            CallContext.State = ContextProperties.Construct(builder => builder.CorrelationId(corellationId));

            return logger.BeginScope(null);
        }
    }
}
