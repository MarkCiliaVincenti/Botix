using System;
using System.Threading;
using System.Threading.Tasks;
using Botix.Bot.Infrastructure.Application;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Botix.Bot.Telegram.API.Infrastructure.HostedServices
{
    public class CallBackInvalidatorHostedService : BackgroundService
    {
        private readonly ICallBackButtonProvider _callBackButtonProvider;
        private readonly ILogger<CallBackInvalidatorHostedService> _logger;

        public CallBackInvalidatorHostedService(ICallBackButtonProvider callBackButtonProvider, ILogger<CallBackInvalidatorHostedService> logger)
        {
            _callBackButtonProvider = callBackButtonProvider ?? throw new ArgumentNullException(nameof(callBackButtonProvider));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                await _callBackButtonProvider.InvalidateCallBacks(cancellationToken);
                await Task.Delay(TimeSpan.FromHours(1), cancellationToken);
            }
        }

        public override Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}
