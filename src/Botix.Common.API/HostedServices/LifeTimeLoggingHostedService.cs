using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Botix.Common.API.HostedServices
{
    public class LifeTimeLoggingHostedService : BackgroundService
    {
        private readonly IHostApplicationLifetime _applicationLifetime;
        private readonly ILogger<LifeTimeLoggingHostedService> _logger;

        public LifeTimeLoggingHostedService(IHostApplicationLifetime applicationLifetime, ILogger<LifeTimeLoggingHostedService> logger)
        {
            _applicationLifetime = applicationLifetime ?? throw new ArgumentNullException(nameof(applicationLifetime));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _applicationLifetime.ApplicationStarted.Register(OnStarted);
            _applicationLifetime.ApplicationStopping.Register(OnStopping);
            _applicationLifetime.ApplicationStopped.Register(OnStopped);
            return Task.CompletedTask;
        }

        public override Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;

        private void OnStarted() => this._logger.LogInformation("Application started.");

        private void OnStopping() => this._logger.LogInformation("Application stopping.");

        private void OnStopped() => this._logger.LogInformation("Application stopped.");
    }
}
