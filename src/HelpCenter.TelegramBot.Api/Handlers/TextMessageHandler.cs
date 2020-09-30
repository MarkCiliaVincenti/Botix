using System.Threading;
using System.Threading.Tasks;
using HelpCenter.TelegramBot.Core.Handlers;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace HelpCenter.TelegramBot.API.Handlers
{
    public class TextMessageHandler : MessageHandlerBase
    {
        public override MessageType MessageType => MessageType.Text;
        protected override Task HandleExecute(Message message, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
