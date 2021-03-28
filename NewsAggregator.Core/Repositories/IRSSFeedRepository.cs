using NewsAggregator.Domain.RSSFeeds;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace NewsAggregator.Core.Repositories
{
    public interface IRSSFeedRepository
    {
        Task<IEnumerable<RSSFeedAggregate>> GetAll(CancellationToken cancellationToken);
        Task Update(IEnumerable<RSSFeedAggregate> rssFeeds, CancellationToken cancellationToken);
        Task SaveChanges(CancellationToken cancellationToken);
    }
}
