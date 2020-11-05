using System.Threading;
using System.Threading.Tasks;
using Botix.Bot.Telegram.Handlers;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Botix.Bot.Telegram.API.Handlers
{
    public class PhotoMessageHandler : MessageHandlerBase
    {
        public override MessageType MessageType => MessageType.Photo;
        protected override Task HandleExecute(Message message, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
