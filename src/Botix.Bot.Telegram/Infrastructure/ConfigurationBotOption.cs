using System;
using Botix.Bot.Telegram.Handlers;
using Microsoft.Extensions.DependencyInjection;

namespace Botix.Bot.Telegram.Infrastructure
{
    public class ConfigurationBotOption
    {
        private readonly IServiceCollection _collection;

        public ConfigurationBotOption(IServiceCollection collection)
        {
            _collection = collection ?? throw new ArgumentNullException(nameof(collection));
        }

        public void AddMessageHandler<TImplementation>() where TImplementation : class, IMessageHandler
        {
            _collection.AddTransient<IMessageHandler, TImplementation>();
        }

        public void AddCallbackHandler<TImplementation>() where TImplementation : class, ICallbackHandler
        {
            _collection.AddTransient<ICallbackHandler, TImplementation>();
        }
    }
}
