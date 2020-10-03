using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;

namespace Botix.TelegramBot.Core.Handlers
{
    public abstract class CallbackHandlerBase : ICallbackHandler
    {
        private readonly CancellationToken _token;


        protected CallbackHandlerBase(CancellationToken token = default)
        {
            _token = token;
        }

        public Task HandleAsync(CallbackQueryEventArgs args) => 
            HandleExecute(args, _token);

        public abstract MessageType MessageType { get; }
        protected abstract Task HandleExecute(CallbackQueryEventArgs args, CancellationToken cancellationToken);
    }
}
