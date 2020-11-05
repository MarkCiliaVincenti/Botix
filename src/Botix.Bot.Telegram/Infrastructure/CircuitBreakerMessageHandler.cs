using System;
using System.Net.Http;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using Botix.Bot.Telegram.Infrastructure.Configurations;
using Microsoft.Extensions.Logging;
using Polly;

namespace Botix.Bot.Telegram.Infrastructure
{
    public class CircuitBreakerMessageHandler : DelegatingHandler
    {
        private readonly RetryPolicySettings _settings;
        private readonly ILogger<CircuitBreakerMessageHandler> _logger;

        public CircuitBreakerMessageHandler(RetryPolicySettings settings, ILogger<CircuitBreakerMessageHandler> logger)
        {
            _settings = settings ?? throw new ArgumentNullException(nameof(settings));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
            CancellationToken cancellationToken) =>

            Policy.Handle<HttpRequestException>()
                .Or<SocketException>()
                .WaitAndRetryForeverAsync(i => _settings.OnFailDelay,
                    (exception, span) =>
                        _logger.LogError(exception, $"{nameof(CircuitBreakerMessageHandler)} exception"))
                .ExecuteAsync(() => base.SendAsync(request, cancellationToken));
    }
}
