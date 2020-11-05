using Telegram.Bot;

namespace Botix.Bot.Telegram
{
    public interface ITelegramBotClientFactory
    {
        ITelegramBotClient CreateClient(string token);
    }
}
