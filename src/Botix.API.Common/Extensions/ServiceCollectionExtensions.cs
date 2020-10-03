using Botix.API.Common.HostedServices;
using Microsoft.Extensions.DependencyInjection;

namespace Botix.API.Common.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddLifetimeLogging(this IServiceCollection services) => 
            services.AddHostedService<LifeTimeLoggingHostedService>();
    }
}
