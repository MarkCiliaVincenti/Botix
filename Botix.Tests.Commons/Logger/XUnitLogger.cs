using System;
using Microsoft.Extensions.Logging;
using Xunit.Abstractions;

namespace Botix.Tests.Commons.Logger
{
    public class XUnitLogger : ILogger
    {
        private readonly ITestOutputHelper _output;
        private readonly LogLevel _logLevel;

        public XUnitLogger(ITestOutputHelper output, LogLevel logLevel)
        {
            _output = output ?? throw new ArgumentNullException(nameof(output));
            _logLevel = logLevel;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception,
            Func<TState, Exception, string> formatter)
        {
            if (!IsEnabled(logLevel))
                return;

            if (formatter == null)
                throw new ArgumentNullException(nameof(formatter));

            var message = formatter(state, exception);
            if (string.IsNullOrEmpty(message) && exception == null)
                return;

            var line = $"{DateTime.Now:HH':'mm':'ss'.'fffffff} {logLevel}: {message}";

            _output.WriteLine(line);

            if (exception != null)
                _output.WriteLine(exception.ToString());
        }

        public bool IsEnabled(LogLevel logLevel) =>
            _logLevel <= logLevel;

        public IDisposable BeginScope<TState>(TState state) =>
            new XUnitScope();
    }
}
