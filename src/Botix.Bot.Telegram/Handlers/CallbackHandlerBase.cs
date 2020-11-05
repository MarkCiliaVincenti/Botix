using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;

namespace Botix.Bot.Telegram.Handlers
{
    public abstract class CallbackHandlerBase : ICallbackHandler
    {
        public Task HandleAsync(CallbackQueryEventArgs args, CancellationToken cancellationToken) =>
            HandleExecute(args, cancellationToken);

        public abstract MessageType MessageType { get; }
        protected abstract Task HandleExecute(CallbackQueryEventArgs args, CancellationToken cancellationToken);
    }
}
