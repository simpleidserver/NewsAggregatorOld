using NewsAggregator.Core.QueryResults;
using System.Threading;
using System.Threading.Tasks;

namespace NewsAggregator.Api.Feeds
{
    public interface IFeedService
    {
        Task<FeedQueryResult> Get(string id, CancellationToken cancellationToken);
    }
}
