using HelpCenter.API.Common.HostedServices;
using Microsoft.Extensions.DependencyInjection;

namespace HelpCenter.API.Common.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddLifetimeLogging(this IServiceCollection services) => 
            services.AddHostedService<LifeTimeLoggingHostedService>();
    }
}
