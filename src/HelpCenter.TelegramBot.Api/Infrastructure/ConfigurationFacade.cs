using HelpCenter.TelegramBot.Core.Configurations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HelpCenter.TelegramBot.API.Infrastructure
{
    public static class ConfigurationFacade
    {
        public static void RegisterSettings(this IServiceCollection services, IConfiguration configuration)
        {
            var accessTokenSettings = configuration.GetSection("AccessToken").Get<AccessTokenSettings>();

            services.AddSingleton(accessTokenSettings);
        }
    }
}
