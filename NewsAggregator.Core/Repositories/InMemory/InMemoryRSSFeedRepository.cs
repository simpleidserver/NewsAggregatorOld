using NewsAggregator.Domain.RSSFeeds;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NewsAggregator.Core.Repositories.InMemory
{
    public class InMemoryRSSFeedRepository : IRSSFeedRepository
    {
        private readonly ConcurrentBag<RSSFeedAggregate> _rssFeeds;

        public InMemoryRSSFeedRepository(ConcurrentBag<RSSFeedAggregate> rssFeeds)
        {
            _rssFeeds = rssFeeds;
        }

        public Task<IEnumerable<RSSFeedAggregate>> GetAll(CancellationToken cancellationToken)
        {
            IEnumerable<RSSFeedAggregate> result = _rssFeeds.ToList();
            return Task.FromResult(result);
        }

        public Task Update(IEnumerable<RSSFeedAggregate> rssFeeds, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task<int> SaveChanges(CancellationToken cancellationToken)
        {
            return Task.FromResult(1);
        }
    }
}