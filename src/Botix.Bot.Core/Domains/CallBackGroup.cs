using System;
using System.Collections.Generic;
using System.Linq;

namespace Botix.Bot.Core.Domains
{
    public class CallBackGroup
    {
        public CallBackGroup(string messageCallBack)
        {
            MessageCallBack = messageCallBack;
            CreatedAt = DateTime.UtcNow;
        }

        public long ID { get; protected set; }

        public bool IsProcessed { get; protected set; }

        public string MessageCallBack { get; set; }

        public virtual IList<CallBack> CallBacks { get; protected set; }

        public DateTime CreatedAt { get; set; }

        public void AddButtons(params (string caption, string callback)[] callBackButtons)
        {
            CallBacks ??= new List<CallBack>();
            foreach (var callBack in callBackButtons.Select(x => new CallBack(x.caption, x.callback)))
            {
                CallBacks.Add(callBack);
            }
        }

        public void RemoveRangeButtons(params CallBack[] callBackButtons)
        {
            foreach (var callBackButton in callBackButtons)
            {
                CallBacks.Remove(callBackButton);
            }
        }

        public void Processed()
        {
            if (IsProcessed == false)
                IsProcessed = true;
        }
    }
}
