using System.Threading.Tasks;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;

namespace HelpCenter.TelegramBot.Core.Handlers
{
    public interface ICallbackHandler
    {
        MessageType MessageType { get; }
        Task HandleAsync(CallbackQueryEventArgs arg);
    }
}
