using MediatR;
using NewsAggregator.Core.QueryResults;

namespace NewsAggregator.Api.Articles.Queries
{
    public class SearchArticlesInDataSourceQuery : IRequest<SearchQueryResult<ArticleQueryResult>>
    {
        public SearchArticlesInDataSourceQuery()
        {
            StartIndex = 0;
            Count = 100;
        }

        public int StartIndex { get; set; }
        public int Count { get; set; }
        public string DataSourceId { get; set; }
        public string Order { get; set; }
        public string Direction { get; set; }
        public string UserId { get; set; }
    }
}
