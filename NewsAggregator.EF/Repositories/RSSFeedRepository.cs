using Microsoft.EntityFrameworkCore;
using NewsAggregator.Core.Repositories;
using NewsAggregator.Domain.RSSFeeds;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace NewsAggregator.EF.Repositories
{
    public class RSSFeedRepository : IRSSFeedRepository
    {
        private readonly NewsAggregatorDBContext _dbContext;

        public RSSFeedRepository(NewsAggregatorDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<RSSFeedAggregate>> GetAll(CancellationToken cancellationToken)
        {
            IEnumerable<RSSFeedAggregate> result = await _dbContext.RSSFeeds.Include(r => r.ExtractionHistories).ToListAsync(cancellationToken);
            return result;
        }

        public Task Update(IEnumerable<RSSFeedAggregate> rssFeeds, CancellationToken cancellationToken)
        {
            _dbContext.RSSFeeds.UpdateRange(rssFeeds);
            return Task.CompletedTask;
        }

        public Task<int> SaveChanges(CancellationToken cancellationToken)
        {
            return _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
