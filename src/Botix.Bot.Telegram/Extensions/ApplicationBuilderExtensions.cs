using System.Linq;
using System.Threading;
using Botix.Bot.Telegram.Handlers;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Telegram.Bot;

namespace Botix.Bot.Telegram.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder StartTelegramBot(this IApplicationBuilder builder)
        {
            var provider = builder.ApplicationServices;
            var client = provider.GetService<ITelegramBotClient>();
            
            client.OnMessage += async (_, args) => await provider.GetServices<IMessageHandler>()
                .First(x => x.MessageType == args.Message.Type)
                .HandleAsync(args.Message, CancellationToken.None);

            client.OnCallbackQuery += async (_, args) => await provider.GetServices<ICallbackHandler>()
                .First(x => x.MessageType == args.CallbackQuery.Message.Type)
                .HandleAsync(args, CancellationToken.None);

            client.StartReceiving();

            return builder;
        }

    }
}
