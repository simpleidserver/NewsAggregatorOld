using NewsAggregator.Core.Domains.Feeds;
using System.Threading;
using System.Threading.Tasks;

namespace NewsAggregator.Core.Repositories
{
    public interface IFeedCommandRepository
    {
        Task<FeedAggregate> Get(string id, CancellationToken cancellationToken);
        Task<FeedAggregate> Get(string userId, string title, CancellationToken cancellationToken);
        Task Add(FeedAggregate feed, CancellationToken cancellationToken);
        Task Delete(FeedAggregate feed, CancellationToken cancellation);
        Task Update(FeedAggregate feed, CancellationToken cancellation);
        Task<int> SaveChanges(CancellationToken cancellationToken);
    }
}
