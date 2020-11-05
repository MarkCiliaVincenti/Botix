using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Botix.Bot.Telegram.Handlers
{
    public abstract class MessageHandlerBase : IMessageHandler
    {
        public abstract MessageType MessageType { get; }

        public Task HandleAsync(Message message, CancellationToken cancellationToken) =>
            HandleExecute(message, cancellationToken);

        protected abstract Task HandleExecute(Message message, CancellationToken cancellationToken);
    }
}
