using NewsAggregator.Core.Parameters;
using NewsAggregator.Core.QueryResults;
using System.Threading;
using System.Threading.Tasks;

namespace NewsAggregator.Core.Repositories
{
    public interface IRecommendationQueryRepository
    {
        Task<SearchQueryResult<RecommendationQueryResult>> Search(SearchRecommendationsParameter parameter, CancellationToken cancellationToken);
    }
}
