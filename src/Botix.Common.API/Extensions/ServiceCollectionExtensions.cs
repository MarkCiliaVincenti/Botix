using Botix.Common.API.HostedServices;
using Microsoft.Extensions.DependencyInjection;

namespace Botix.Common.API.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddLifetimeLogging(this IServiceCollection services) =>
            services.AddHostedService<LifeTimeLoggingHostedService>();
    }
}
