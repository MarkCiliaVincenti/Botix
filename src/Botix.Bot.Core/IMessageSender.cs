using System.Threading;
using System.Threading.Tasks;

namespace Botix.Bot.Core
{
    public interface IMessageSender
    {
        Task SendMessage<TChatId>(TChatId chatId, string message, CancellationToken cancellationToken);

        Task SendInlineKeyBoardMessage<TChatId>(TChatId chatId, ButtonMessage message, CancellationToken cancellationToken);

        Task SendReplayKeyBoardMessage<TChatId>(TChatId chatId, ButtonMessage message, CancellationToken cancellationToken);
    }
}
