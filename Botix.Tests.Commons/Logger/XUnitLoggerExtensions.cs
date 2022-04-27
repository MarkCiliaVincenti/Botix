using Microsoft.Extensions.Logging;
using Xunit.Abstractions;

namespace Botix.Tests.Commons.Logger
{
    public static class XUnitLoggerExtensions
    {
        public static ILogger<T> CreateLogger<T>(this ILoggerProvider provider) =>
            new Logger<T>(new LoggerFactory(new[] { provider }));

        public static ILoggerProvider CreateLoggerProvider(this ITestOutputHelper helper, LogLevel logLevel) =>
            new XUnitLoggerProvider(helper, logLevel);
    }
}
