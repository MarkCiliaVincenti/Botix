using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Botix.Bot.Core;
using Botix.Bot.Core.Domains;
using Botix.Bot.Infrastructure.Application;
using Botix.Common.Logging.Context;
using Microsoft.Extensions.Logging;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace Botix.Bot.Telegram.Application
{
    public class MessageSender : IMessageSender
    {
        private readonly ITelegramBotClient _client;
        private readonly ICallBackButtonProvider _callBackButtonProvider;
        private readonly ILogger<MessageSender> _logger;

        public MessageSender(ITelegramBotClient client, ICallBackButtonProvider callBackButtonProvider, ILogger<MessageSender> logger)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
            _callBackButtonProvider = callBackButtonProvider ?? throw new ArgumentNullException(nameof(callBackButtonProvider));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task SendMessage<TChatId>(TChatId chatId, string message, CancellationToken cancellationToken)
        {
            using (_logger.DebugLoggingScope("Sending message", nameof(SendMessage), message))
            {
                var chat = chatId as ChatId;
                await _client.SendTextMessageAsync(chat, message, ParseMode.Default, cancellationToken: cancellationToken);
            }
        }

        public async Task SendInlineKeyBoardMessage<TChatId>(TChatId chatId, ButtonMessage message, CancellationToken cancellationToken)
        {
            using (_logger.DebugLoggingScope("Sending message", nameof(SendInlineKeyBoardMessage), message.ToString()))
            {
                var chat = chatId as ChatId;
                await _client.SendTextMessageAsync(chat, message.Text, ParseMode.Default, replyMarkup: BuildInlineKeyBoardMarkup(message.Buttons), cancellationToken: cancellationToken);
                await AddCallBackGroup(message, cancellationToken);
            }
        }

        public async Task SendReplayKeyBoardMessage<TChatId>(TChatId chatId, ButtonMessage message, CancellationToken cancellationToken)
        {
            using (_logger.DebugLoggingScope("Sending message", nameof(SendReplayKeyBoardMessage), message.ToString()))
            {
                var chat = chatId as ChatId;
                await AddCallBackGroup(message, cancellationToken);
                await _client.SendTextMessageAsync(chat, message.Text, ParseMode.Html, replyMarkup: BuildMultilineKeyBoardMarkup(message.Buttons), cancellationToken: cancellationToken);
            }
        }

        private async Task AddCallBackGroup(ButtonMessage message, CancellationToken cancellationToken)
        {
            var callBack = new CallBackGroup(message.MessageCallBack);
            callBack.AddButtons(message.Buttons.Select(x => (x.Text, x.PressCallBack))
                .ToArray());

            await _callBackButtonProvider.AddCallBackGroup(callBack, cancellationToken);
        }

        private static InlineKeyboardMarkup BuildInlineKeyBoardMarkup(IList<Button> buttons) =>
            new InlineKeyboardMarkup(buttons.Select(x => InlineKeyboardButton.WithCallbackData(x.Text, x.PressCallBack)));

        private static InlineKeyboardMarkup BuildMultilineKeyBoardMarkup(IList<Button> buttons, decimal columns = 3)
        {
            var range = buttons.Count / columns;
            var row = range % 1 == 0 ? (int)range : (int)Math.Round(range, MidpointRounding.AwayFromZero) + 1;

            var inlineKeyboard = new List<List<InlineKeyboardButton>>();
            inlineKeyboard.AddRange(Enumerable.Range(0, row).Select(x => new List<InlineKeyboardButton>()));
            var queue = new Queue<Button>(buttons);
            for (var i = 0; i < row; i++)
            {
                while (inlineKeyboard[i].Count < columns)
                {
                    if (!queue.TryDequeue(out var button))
                        break;

                    inlineKeyboard[i].Add(new InlineKeyboardButton { Text = button.Text, CallbackData = button.PressCallBack });
                }
            }

            return new InlineKeyboardMarkup(inlineKeyboard);
        }
    }
}
