using Botix.API.Common.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Botix.TelegramBot.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
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
                        .ConfigureServices(service=>service.AddLifetimeLogging());
                });
    }
}
