using NewsAggregator.Core.Parameters;
using NewsAggregator.Core.QueryResults;
using System.Threading;
using System.Threading.Tasks;

namespace NewsAggregator.Core.Repositories
{
    public interface IArticleQueryRepository
    {
        Task<SearchQueryResult<ArticleQueryResult>> SearchInFeeds(SearchArticlesInFeedParameter parameter, CancellationToken cancellationToken);
    }
}
