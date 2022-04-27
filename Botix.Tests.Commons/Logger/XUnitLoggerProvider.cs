using System;
using Microsoft.Extensions.Logging;
using Xunit.Abstractions;

namespace Botix.Tests.Commons.Logger
{
    public class XUnitLoggerProvider : ILoggerProvider
    {
        private readonly ITestOutputHelper _output;
        private readonly LogLevel _logLevel;

        public XUnitLoggerProvider(ITestOutputHelper output, LogLevel logLevel)
        {
            _output = output ?? throw new ArgumentNullException(nameof(output));
            _logLevel = logLevel;
        }

        public void Dispose()
        {

        }

        public ILogger CreateLogger(string categoryName) =>
            new XUnitLogger(_output, _logLevel);
    }
}
