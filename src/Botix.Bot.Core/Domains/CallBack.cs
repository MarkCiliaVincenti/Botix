namespace Botix.Bot.Core.Domains
{
    public class CallBack
    {
        public CallBack() { }

        public CallBack(string caption, string guid)
        {
            Caption = caption;
            Guid = guid;
        }

        public long Id { get; protected set; }

        public string Caption { get; protected set; }

        public string Guid { get; protected set; }

        public long CallBackGroupId { get; protected set; }

        public CallBackGroup CallBackGroup { get; protected set; }
    }
}
