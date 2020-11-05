using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Botix.Bot.Core;
using Botix.Bot.Infrastructure.Application;
using Botix.Bot.Telegram.API.Models;
using Botix.Common.API.Exceptions;
using Telegram.Bot.Types;

namespace Botix.Bot.Telegram.API.Application
{
    public class TextMessageAdapter
    {
        private readonly IMessageSender _messageSender;
        private readonly IUserProvider _userProvider;

        public TextMessageAdapter(IMessageSender messageSender, IUserProvider userProvider)
        {
            _messageSender = messageSender ?? throw new ArgumentNullException(nameof(messageSender));
            _userProvider = userProvider ?? throw new ArgumentNullException(nameof(userProvider));
        }

        public async Task SendTextMessage(string userName, TextMessageModel message, CancellationToken cancellationToken)
        {
            var user = await _userProvider.GetUser(userName, cancellationToken);

            if (user == null)
                throw new WebApiException(HttpStatusCode.NotFound, $"user by userName not found. actual: {userName}");

            var chatId = new ChatId(user.Identifier);
            await _messageSender.SendMessage(chatId, message.Text, cancellationToken);
        }

        public async Task SendInlineButtonMessage(string userName, TextWithButtonModel message, CancellationToken cancellationToken)
        {
            var user = await _userProvider.GetUser(userName, cancellationToken);

            if (user == null)
                throw new WebApiException(HttpStatusCode.NotFound, $"user by userName not found. actual: {userName}");

            var chatId = new ChatId(user.Identifier);


            var buttons = message.Buttons.Select(x => new Button(x.Caption)).ToList();
            var buttonMessage = new ButtonMessage(message.Text, buttons, message.CallBackData);

            await _messageSender.SendInlineKeyBoardMessage(chatId, buttonMessage, cancellationToken);
        }

        public async Task SendReplyButtonMessage(string userName, TextWithButtonModel message, CancellationToken cancellationToken)
        {
            var user = await _userProvider.GetUser(userName, cancellationToken);

            if (user == null)
                throw new WebApiException(HttpStatusCode.NotFound, $"user by userName not found. actual: {userName}");

            var chatId = new ChatId(user.Identifier);


            var buttons = message.Buttons.Select(x => new Button(x.Caption)).ToList();
            var buttonMessage = new ButtonMessage(message.Text, buttons, message.CallBackData);

            await _messageSender.SendReplayKeyBoardMessage(chatId, buttonMessage, cancellationToken);
        }
    }
}
