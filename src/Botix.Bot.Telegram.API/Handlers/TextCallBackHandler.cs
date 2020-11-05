using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Botix.Bot.Core;
using Botix.Bot.Infrastructure.Application;
using Botix.Bot.Telegram.Handlers;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Botix.Bot.Telegram.API.Handlers
{
    public class TextCallBackHandler : CallbackHandlerBase
    {
        private readonly ICallBackButtonProvider _callBackButtonProvider;
        private readonly IMessageSender _messageSender;

        public TextCallBackHandler(ICallBackButtonProvider callBackButtonProvider, IMessageSender messageSender)
        {
            _callBackButtonProvider = callBackButtonProvider ?? throw new ArgumentNullException(nameof(callBackButtonProvider));
            _messageSender = messageSender ?? throw new ArgumentNullException(nameof(messageSender));
        }

        public override MessageType MessageType => MessageType.Text;
        protected override async Task HandleExecute(CallbackQueryEventArgs args, CancellationToken cancellationToken)
        {
            var chatId = new ChatId(args.CallbackQuery.From.Id);
            var callBack = await _callBackButtonProvider.GetCallBackGroup(args.CallbackQuery.Data, cancellationToken);
            if (callBack == null || callBack.IsProcessed)
            {
                await _messageSender.SendMessage(chatId, "Ожидание ответа уже истекло или было обработано ранее.", cancellationToken);
            }
            else
            {
                await _callBackButtonProvider.CallBackProcessed(callBack.ID, cancellationToken);

                var callBacks = callBack.CallBacks;

                //здесь должен быть Reactive с SignalR но пока что пусть будет ответ в чат.
                await _messageSender.SendMessage(chatId, $"На вопрос: {args.CallbackQuery.Message.Text} вы ответили {callBacks.First().Caption} callBack: {callBacks.First().CallBackGroup.MessageCallBack}", cancellationToken);
            }
        }
    }
}
