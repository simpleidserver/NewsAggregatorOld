using NewsAggregator.Core.QueryResults;
using NewsAggregator.Core.Repositories.Parameters;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace NewsAggregator.Core.Repositories
{
    public interface IFeedQueryRepository
    {
        Task<FeedQueryResult> Get(string feedId, CancellationToken cancellationToken);
        Task<IEnumerable<DetailedFeedQueryResult>> GetFeeds(string userId);
        Task<SearchQueryResult<DetailedFeedQueryResult>> SearchFeeds(SearchFeedParameter parameter, CancellationToken cancellationToken);
    }
}