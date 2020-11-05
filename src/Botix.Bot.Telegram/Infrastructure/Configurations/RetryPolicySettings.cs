using System;

namespace Botix.Bot.Telegram.Infrastructure.Configurations
{
    public class RetryPolicySettings
    {
        public TimeSpan OnFailDelay { get; set; }
    }
}
