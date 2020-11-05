using System;
using System.Net.Http;
using System.Net.Sockets;
using Botix.Bot.Telegram.Infrastructure;
using Botix.Bot.Telegram.Infrastructure.Configurations;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Polly;
using Telegram.Bot;

namespace Botix.Bot.Telegram.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection TelegramBotConfiguration(this IServiceCollection services, Action<ConfigurationBotOption> options)
        {
            services.AddTransient<CircuitBreakerMessageHandler>();

            services.AddHttpClient<TelegramBotClientFactory>()
                .AddHttpMessageHandler<CircuitBreakerMessageHandler>();

            services.AddSingleton<ITelegramBotClient>(provider =>
            {
                var logger = provider.GetService<ILogger<ConfigurationBotOption>>();
                var settings = provider.GetRequiredService<AccessTokenSettings>();
                var clientFactory = provider.GetRequiredService<TelegramBotClientFactory>();

                return Policy
                    .Handle<HttpRequestException>()
                    .Or<SocketException>()
                    .WaitAndRetryForeverAsync(
                        sleepDurationProvider: i => TimeSpan.FromMilliseconds(100),
                        onRetry: (exception, span) => logger.LogError(exception, "Telegram connection exception"))
                    .ExecuteAsync(async () =>
                    {
                        logger.LogInformation("Try connected...");

                        var client = clientFactory.CreateClient(settings.Token);
                        var isConnected = await client.TestApiAsync();

                        if (isConnected)
                            logger.LogInformation("Connection success!");

                        return client;
                    }).GetAwaiter().GetResult();
            });

            var telegramBotOption = new ConfigurationBotOption(services);
            options(telegramBotOption);

            return services;
        }
    }
}
