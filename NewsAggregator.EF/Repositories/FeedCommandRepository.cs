using Microsoft.EntityFrameworkCore;
using NewsAggregator.Core.Domains.Feeds;
using NewsAggregator.Core.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace NewsAggregator.EF.Repositories
{
    public class FeedCommandRepository : IFeedCommandRepository
    {
        private readonly NewsAggregatorDBContext _dbContext;

        public FeedCommandRepository(NewsAggregatorDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<FeedAggregate> Get(string id, CancellationToken cancellationToken)
        {
            return _dbContext.Feeds.Include(f => f.DataSources).FirstOrDefaultAsync(f => f.Id == id, cancellationToken);
        }

        public Task<FeedAggregate> Get(string userId, string title, CancellationToken cancellationToken)
        {
            return _dbContext.Feeds.Include(f => f.DataSources).FirstOrDefaultAsync(f => f.UserId == userId && f.Title == title, cancellationToken);
        }

        public Task Add(FeedAggregate feed, CancellationToken cancellationToken)
        {
            _dbContext.Add(feed);
            return Task.CompletedTask;
        }

        public Task Delete(FeedAggregate feed, CancellationToken cancellation)
        {
            _dbContext.Feeds.Remove(feed);
            return Task.CompletedTask;
        }

        public Task Update(FeedAggregate feed, CancellationToken cancellation)
        {
            _dbContext.Feeds.Update(feed);
            return Task.CompletedTask;
        }

        public Task<int> SaveChanges(CancellationToken cancellationToken)
        {
            return _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
