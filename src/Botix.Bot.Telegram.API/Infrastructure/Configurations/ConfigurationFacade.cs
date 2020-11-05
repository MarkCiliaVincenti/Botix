using Botix.Bot.Telegram.Infrastructure.Configurations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Botix.Bot.Telegram.API.Infrastructure.Configurations
{
    public static class ConfigurationFacade
    {
        public static void RegisterSettings(this IServiceCollection services, IConfiguration configuration)
        {
            var accessTokenSettings = configuration.GetSection("AccessToken").Get<AccessTokenSettings>();
            var retryPolicySettings = configuration.GetSection("RetryPolicy").Get<RetryPolicySettings>();
            var connectionString = configuration.GetConnectionString("Postgres");

            PostgresConnectionString = connectionString;

            services.AddSingleton(accessTokenSettings);
            services.AddSingleton(retryPolicySettings);
        }

        public static string PostgresConnectionString { get; private set; }
    }
}
