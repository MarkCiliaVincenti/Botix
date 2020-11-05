using System;
using System.Reflection;
using Botix.Bot.Core;
using Botix.Bot.Infrastructure.Application;
using Botix.Bot.Infrastructure.DataBase;
using Botix.Bot.Telegram.API.Application;
using Botix.Bot.Telegram.API.Handlers;
using Botix.Bot.Telegram.API.Infrastructure.Configurations;
using Botix.Bot.Telegram.Application;
using Botix.Bot.Telegram.Extensions;
using Botix.Common.API;
using Botix.Common.API.Filters;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace Botix.Bot.Telegram.API
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

            services.AddDbContext<BotDbContext>(options => options.UseNpgsql(ConfigurationFacade.PostgresConnectionString, builder => builder.MigrationsAssembly("Botix.Bot.Infrastructure")));

            services.TelegramBotConfiguration(option =>
            {
                option.AddMessageHandler<TextMessageHandler>();
                option.AddMessageHandler<PhotoMessageHandler>();
                option.AddMessageHandler<ContactMessageHandler>();
                option.AddCallbackHandler<TextCallBackHandler>();
            });

            services.AddScoped<LoggingExceptionFilter>();

            services.AddTransient<TextMessageAdapter>();
            services.AddTransient<IMessageSender, MessageSender>();
            services.AddTransient<IUserProvider, UserProvider>();
            services.AddTransient<ICallBackButtonProvider, CallBackButtonProvider>();

            services.AddControllers()
                .AddJsonOptions(SerializationSettings.ConfigureJsonOptions);

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = Assembly.GetAssembly(typeof(Startup))?.GetName().Name,
                    Description = "API для коммуникации с клиентом через Telegram бот.",
                    Contact = new OpenApiContact
                    {
                        Email = "marat2785@gmail.com",
                        Name = "Тапканов Марат",
                        Url = new Uri("https://www.facebook.com/m.tapkanov/")
                    }
                });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.StartTelegramBot();
        }
    }
}
