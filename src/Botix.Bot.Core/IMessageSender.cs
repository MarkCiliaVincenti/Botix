using System.Threading;
using System.Threading.Tasks;

namespace Botix.Bot.Core
{
    public interface IMessageSender
    {
        Task SendMessage<TChatId>(TChatId chatId, string message, CancellationToken cancellationToken);
        
        Task<TCallBackMessage> SendMessage<TChatId, TCallBackMessage>(TChatId chatId, YesNoMessage message,  CancellationToken cancellationToken);
    }
}
