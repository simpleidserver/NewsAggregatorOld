using MediatR;
using NewsAggregator.Core.QueryResults;

namespace NewsAggregator.Api.Recommendations.Queries
{
    public class SearchRecommendationsQuery : IRequest<SearchQueryResult<ArticleQueryResult>>
    {
        public SearchRecommendationsQuery()
        {
            StartIndex = 0;
            Count = 100;
        }

        public int Count { get; set; }
        public int StartIndex { get; set; }
        public string UserId { get; set; }
    }
}