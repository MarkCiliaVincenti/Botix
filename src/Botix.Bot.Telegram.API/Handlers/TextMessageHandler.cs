using System;
using System.Threading;
using System.Threading.Tasks;
using Botix.Bot.Infrastructure.Application;
using Botix.Bot.Telegram.Handlers;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using User = Botix.Bot.Core.Domains.User;

namespace Botix.Bot.Telegram.API.Handlers
{
    public class TextMessageHandler : MessageHandlerBase
    {
        private readonly IUserProvider _userProvider;

        public TextMessageHandler(IUserProvider userProvider)
        {
            _userProvider = userProvider ?? throw new ArgumentNullException(nameof(userProvider));
        }

        public override MessageType MessageType => MessageType.Text;
        protected override async Task HandleExecute(Message message, CancellationToken cancellationToken)
        {
            if (message.Text == "/start")
            {
                var @from = message.From;

                var user = User.Create(@from.Id, @from.Username);
                user.UpdateFirstName(@from.FirstName);

                await _userProvider.SetOrUpdateUser(user, cancellationToken);
            }
        }
    }
}
