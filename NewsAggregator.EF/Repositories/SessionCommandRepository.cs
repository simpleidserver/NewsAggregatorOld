using Microsoft.EntityFrameworkCore;
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

        public Task<SessionAggregate> Get(string id, CancellationToken cancellationToken)
        {
            return _dbContext.Sessions.FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
        }

        public Task Add(SessionAggregate session, CancellationToken cancellationToken)
        {
            _dbContext.Sessions.Add(session);
            return Task.CompletedTask;
        }

        public Task Update(SessionAggregate session, CancellationToken cancellationToken)
        {
            _dbContext.Sessions.Update(session);
            return Task.CompletedTask;
        }

        public Task<int> SaveChanges(CancellationToken cancellationToken)
        {
            return _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
