using Telegram.Bot.Types;

namespace Botix.Bot.Telegram.Application
{
    public class ChatIdEnvelop
    {
        public ChatIdEnvelop(ChatId chatId)
        {
            Identifier = chatId.Identifier;
            UserName = chatId.Username;
        }

        private long Identifier { get; }
        private string UserName { get; }

        public override string ToString()
        {
            return $"{nameof(ChatId)}: {nameof(Identifier)}: {Identifier}, {nameof(UserName)}: {UserName}";
        }
    }
}
