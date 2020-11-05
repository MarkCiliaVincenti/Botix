using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Botix.Bot.Infrastructure.DataBase
{
    public static class BotDbContextSeed
    {
        public static IHost SeedAppDbContext(this IHost host)
        {
            using var scope = host.Services.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<BotDbContext>();
            try
            {
                db.Database.Migrate();
            }
            catch (Exception ex)
            {
                var logger = scope.ServiceProvider.GetRequiredService<ILogger<BotDbContext>>();

                logger.LogError(ex, "An error occurred while seeding the database.");

                throw;
            }

            return host;
        }
    }
}
