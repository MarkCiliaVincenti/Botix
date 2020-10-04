using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Botix.TelegramBot.Core.Handlers
{
    public abstract class MessageHandlerBase : IMessageHandler
    {
        private readonly CancellationToken _token;

        protected MessageHandlerBase(CancellationToken token = default)
        {
            _token = token;
        }

        public Task HandleAsync(Message message)
        {
            return HandleExecute(message, _token);
        }

        public abstract MessageType MessageType { get; }
        protected abstract Task HandleExecute(Message message, CancellationToken cancellationToken);
    }
}
