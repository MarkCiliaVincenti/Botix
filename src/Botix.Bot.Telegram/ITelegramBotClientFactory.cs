using Telegram.Bot;

namespace Botix.TelegramBot.Core
{
    public interface ITelegramBotClientFactory
    {
        ITelegramBotClient CreateClient(string token);
    }
}
