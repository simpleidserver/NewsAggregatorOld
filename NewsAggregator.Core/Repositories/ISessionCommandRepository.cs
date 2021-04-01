using NewsAggregator.Core.Domains.Sessions;
using System.Threading;
using System.Threading.Tasks;

namespace NewsAggregator.Core.Repositories
{
    public interface ISessionCommandRepository
    {
        Task<SessionAggregate> Get(string id, CancellationToken cancellationToken);
        Task Add(SessionAggregate session, CancellationToken cancellationToken);
        Task Update(SessionAggregate session, CancellationToken cancellationToken);
        Task<int> SaveChanges(CancellationToken cancellationToken);
    }
}