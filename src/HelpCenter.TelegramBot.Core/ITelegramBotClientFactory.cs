using Telegram.Bot;

namespace HelpCenter.TelegramBot.Core
{
    public interface ITelegramBotClientFactory
    {
        ITelegramBotClient CreateClient(string token);
    }
}
