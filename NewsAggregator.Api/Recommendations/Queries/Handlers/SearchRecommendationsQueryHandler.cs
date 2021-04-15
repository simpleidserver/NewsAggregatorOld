using MediatR;
using NewsAggregator.Core.QueryResults;
using NewsAggregator.Core.Repositories;
using NewsAggregator.Core.Repositories.Parameters;
using System.Threading;
using System.Threading.Tasks;

namespace NewsAggregator.Api.Recommendations.Queries.Handlers
{
    public class SearchRecommendationsQueryHandler : IRequestHandler<SearchRecommendationsQuery, SearchQueryResult<ArticleQueryResult>>
    {
        private readonly IRecommendationQueryRepository _recommendationQueryRepository;

        public SearchRecommendationsQueryHandler(IRecommendationQueryRepository recommendationQueryRepository)
        {
            _recommendationQueryRepository = recommendationQueryRepository;
        }

        public Task<SearchQueryResult<ArticleQueryResult>> Handle(SearchRecommendationsQuery request, CancellationToken cancellationToken)
        {
            return _recommendationQueryRepository.Search(new SearchRecommendationsParameter
            {
                Count = request.Count,
                StartIndex = request.StartIndex,
                UserId = request.UserId
            }, cancellationToken);
        }
    }
}