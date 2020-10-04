using System;

namespace Botix.TelegramBot.Core.Infrastructure.Configurations
{
    public class RetryPolicySettings
    {
        public TimeSpan OnFailDelay { get; set; }
    }
}
