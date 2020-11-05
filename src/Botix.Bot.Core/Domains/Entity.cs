using System;

namespace Botix.Bot.Core.Domains
{
    public abstract class Entity
    {
        public DateTime CreatedAt { get; protected set; }

        public DateTime UpdateAt { get; protected set; }
    }
}
