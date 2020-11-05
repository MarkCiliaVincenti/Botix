using System;

namespace Botix.Bot.Core
{
    public class Button
    {
        public Button(string text)
        {
            Text = text;
            PressCallBack = Guid.NewGuid().ToString();
        }

        public string Text { get; }

        public string PressCallBack { get; }

        public override string ToString()
        {
            return $"{nameof(Text)}: {Text}, {nameof(PressCallBack)}: {PressCallBack}";
        }
    }
}
