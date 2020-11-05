using System.Collections.Generic;

namespace Botix.Bot.Core
{
    public class ButtonMessage
    {
        public string Text { get; }
        public IList<Button> Buttons { get; }
        public string MessageCallBack { get; }

        public ButtonMessage(string text, IList<Button> buttons, string messageCallBack)
        {
            Text = text;
            Buttons = buttons;
            MessageCallBack = messageCallBack;
        }

        public override string ToString()
        {
            return $"{nameof(Text)}: {Text}, {nameof(Buttons)}: {string.Join(",", Buttons)}, {nameof(MessageCallBack)}: {MessageCallBack}";
        }
    }
}
