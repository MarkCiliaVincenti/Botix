using System.Threading;
using System.Threading.Tasks;
using Botix.Bot.Core.Domains;

namespace Botix.Bot.Infrastructure.Application
{
    public interface ICallBackButtonProvider
    {
        public Task<CallBackGroup> GetCallBackGroup(string callBackData, CancellationToken cancellationToken = default);

        public Task AddCallBackGroup(CallBackGroup callBackGroup, CancellationToken cancellationToken = default);

        public Task CallBackProcessed(long callBack, CancellationToken cancellationToken = default);
    }
}
