using NewsAggregator.Core.Domains.Sessions;
using NewsAggregator.Core.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace NewsAggregator.EF.Repositories
{
    public class SessionCommandRepository : ISessionCommandRepository
    {
        private readonly NewsAggregatorDBContext _dbContext;

        public SessionCommandRepository(NewsAggregatorDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task Add(SessionAggregate session, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task<SessionAggregate> Get(string id, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task<int> SaveChanges(CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task Update(SessionAggregate session, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
