using System;
using System.Threading;
using System.Threading.Tasks;
using Botix.Bot.Core.Domains;
using Botix.Bot.Infrastructure.DataBase;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Botix.Bot.Infrastructure.Application
{
    public class UserProvider : IUserProvider
    {
        private readonly IServiceScopeFactory _serviceScope;

        public UserProvider(IServiceScopeFactory serviceScope)
        {
            _serviceScope = serviceScope ?? throw new ArgumentNullException(nameof(serviceScope));
        }

        public async Task<User> GetUser(long id, CancellationToken cancellationToken)
        {
            using var scope = _serviceScope.CreateScope();
            await using var context = scope.ServiceProvider.GetRequiredService<BotDbContext>();
            return await context.Users.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public async Task<User> GetUser(string userName, CancellationToken cancellationToken)
        {
            using var scope = _serviceScope.CreateScope();
            await using var context = scope.ServiceProvider.GetRequiredService<BotDbContext>();
            return await context.Users.FirstOrDefaultAsync(x => x.Username == userName, cancellationToken);
        }

        public async Task SetOrUpdateUser(User user, CancellationToken cancellationToken)
        {
            using var scope = _serviceScope.CreateScope();
            await using var context = scope.ServiceProvider.GetRequiredService<BotDbContext>();
            var result = await context.Users.FirstOrDefaultAsync(x => x.Id == user.Id && x.Identifier == user.Identifier, cancellationToken);

            if (result == null)
                await context.Users.AddAsync(user, cancellationToken);
            else
            {
                result.UpdateUserName(user.Username);
            }

            await context.SaveChangesAsync(cancellationToken);
        }
    }
}
