using Botix.Bot.Infrastructure.DataBase;
using Botix.Common.API.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Botix.Bot.Telegram.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args)
                .Build()
                .SeedAppDbContext()
                .Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>()
                        .ConfigureLogging((context, builder) =>
                        {
                            builder.AddSeq(context.Configuration.GetSection("Seq"));
                            builder.AddConfiguration(context.Configuration.GetSection("Logging"));
                        })
                        .ConfigureServices(service => service.AddLifetimeLogging());
                });
    }
}
