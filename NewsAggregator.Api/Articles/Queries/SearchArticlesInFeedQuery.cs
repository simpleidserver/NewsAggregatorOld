using MediatR;
using NewsAggregator.Core.QueryResults;

namespace NewsAggregator.Api.Articles.Queries
{
    public class SearchArticlesInFeedQuery: IRequest<SearchQueryResult<ArticleQueryResult>>
    {
        public SearchArticlesInFeedQuery()
        {
            StartIndex = 0;
            Count = 100;
        }

        public int StartIndex { get; set; }
        public int Count { get; set; }
        public string DataSourceId { get; set; }
    }
}
