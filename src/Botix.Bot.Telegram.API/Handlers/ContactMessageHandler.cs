using System.Threading;
using System.Threading.Tasks;
using Botix.Bot.Telegram.Handlers;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Botix.Bot.Telegram.API.Handlers
{
    public class ContactMessageHandler : MessageHandlerBase
    {
        public override MessageType MessageType => MessageType.Contact;
        protected override Task HandleExecute(Message message, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
