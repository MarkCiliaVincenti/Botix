using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Botix.TelegramBot.Core.Handlers
{
    public interface IMessageHandler
    {
        MessageType MessageType { get; }
        Task HandleAsync(Message message);
    }
}
