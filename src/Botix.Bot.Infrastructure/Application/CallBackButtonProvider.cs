using System;
using System.Threading;
using System.Threading.Tasks;
using Botix.Bot.Core.Domains;
using Botix.Bot.Infrastructure.DataBase;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Botix.Bot.Infrastructure.Application
{
    public class CallBackButtonProvider : ICallBackButtonProvider
    {
        private readonly IServiceScopeFactory _serviceScope;

        public CallBackButtonProvider(IServiceScopeFactory serviceScope)
        {
            _serviceScope = serviceScope ?? throw new ArgumentNullException(nameof(serviceScope));
        }

        public async Task<CallBackGroup> GetCallBackGroup(string callBackData, CancellationToken cancellationToken = default)
        {
            using var scope = _serviceScope.CreateScope();
            await using var context = scope.ServiceProvider.GetRequiredService<BotDbContext>();
            var callBackButton = await context.CallBacks.Include(x => x.CallBackGroup).AsNoTracking().FirstOrDefaultAsync(x => x.Guid == callBackData, cancellationToken);

            return callBackButton.CallBackGroup;
        }

        public async Task AddCallBackGroup(CallBackGroup callBackGroup, CancellationToken cancellationToken = default)
        {
            using var scope = _serviceScope.CreateScope();
            await using var context = scope.ServiceProvider.GetRequiredService<BotDbContext>();
            await context.CallBackGroups.AddAsync(callBackGroup, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
        }

        public async Task CallBackProcessed(long id, CancellationToken cancellationToken = default)
        {
            using var scope = _serviceScope.CreateScope();
            await using var context = scope.ServiceProvider.GetRequiredService<BotDbContext>();
            var callBack = await context.CallBackGroups.FirstAsync(x => x.ID == id, cancellationToken);
            callBack.Processed();
            await context.SaveChangesAsync(cancellationToken);
        }
    }
}
