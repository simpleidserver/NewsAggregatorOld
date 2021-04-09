using NewsAggregator.Core.QueryResults;
using NewsAggregator.Core.Repositories.Parameters;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace NewsAggregator.Core.Repositories
{
    public interface IFeedQueryRepository
    {
        Task<IEnumerable<FeedQueryResult>> GetFeeds(string userId);
        Task<SearchQueryResult<FeedQueryResult>> SearchFeeds(SearchFeedParameter parameter, CancellationToken cancellationToken);
    }
}