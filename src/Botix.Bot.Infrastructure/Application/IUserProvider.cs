using System.Threading;
using System.Threading.Tasks;
using Botix.Bot.Core.Domains;

namespace Botix.Bot.Infrastructure.Application
{
    public interface IUserProvider
    {
        public Task<User> GetUser(long id, CancellationToken cancellationToken = default);

        public Task<User> GetUser(string userName, CancellationToken cancellationToken = default);

        public Task SetOrUpdateUser(User user, CancellationToken cancellationToken = default);
    }
}
