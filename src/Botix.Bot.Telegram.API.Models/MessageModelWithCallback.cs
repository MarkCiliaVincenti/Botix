using System.Collections.Generic;

namespace Botix.Bot.Telegram.API.Models
{
    public class TextMessageWithCallback
    {
        public string Text { get; set; }
        public string CallBackData { get; set; }
    }

    public class TextMessageModel
    {
        public string Text { get; set; }
    }

    public class ButtonModel
    {
        public string Caption { get; set; }
    }

    public class TextWithButtonModel
    {
        public string Text { get; set; }

        public string CallBackData { get; set; }

        public IList<ButtonModel> Buttons { get; set; }
    }
}
