using Botix.TelegramBot.API.Handlers;
using Botix.TelegramBot.API.Infrastructure;
using Botix.TelegramBot.Core.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Botix.TelegramBot.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.RegisterSettings(Configuration);

            services.TelegramBotConfiguration(option =>
            {
                option.AddMessageHandler<TextMessageHandler>();
                option.AddMessageHandler<PhotoMessageHandler>();
                option.AddMessageHandler<ContactMessageHandler>();
                option.AddCallbackHandler<ContactCallBackHandler>();
            });

            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.StartTelegramBot();
        }
    }
}
