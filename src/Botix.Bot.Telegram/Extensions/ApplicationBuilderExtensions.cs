using System.Linq;
using Botix.TelegramBot.Core.Handlers;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Telegram.Bot;

namespace Botix.TelegramBot.Core.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder StartTelegramBot(this IApplicationBuilder builder)
        {
            var provider = builder.ApplicationServices;
            var client = provider.GetService<ITelegramBotClient>();

            client.OnMessage += async (_, args) => await provider.GetServices<IMessageHandler>()
                .First(x => x.MessageType == args.Message.Type)
                .HandleAsync(args.Message);

            client.OnCallbackQuery += async (_, args) => await provider.GetService<ICallbackHandler>()
                .HandleAsync(args);

            client.StartReceiving();

            return builder;
        }
    }
}
