using NewsAggregator.Core.QueryResults;
using NewsAggregator.Core.Repositories.Parameters;
using System.Threading;
using System.Threading.Tasks;

namespace NewsAggregator.Core.Repositories
{
    public interface IRecommendationQueryRepository
    {
        Task<SearchQueryResult<ArticleQueryResult>> Search(SearchRecommendationsParameter parameter, CancellationToken cancellationToken);
    }
}
