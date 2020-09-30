using System;
using System.Threading;
using System.Threading.Tasks;
using HelpCenter.TelegramBot.Core.Handlers;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;

namespace HelpCenter.TelegramBot.API.Handlers
{
    public class ContactCallBackHandler : CallbackHandlerBase
    {
        public override MessageType MessageType => MessageType.Contact;
        protected override Task HandleExecute(CallbackQueryEventArgs args, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
