using System;
using HelpCenter.TelegramBot.Core.Handlers;
using Microsoft.Extensions.DependencyInjection;

namespace HelpCenter.TelegramBot.Core.Application
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
