using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;

namespace Botix.Bot.Telegram.Handlers
{
    public interface ICallbackHandler
    {
        MessageType MessageType { get; }
        Task HandleAsync(CallbackQueryEventArgs arg, CancellationToken cancellationToken);
    }
}
