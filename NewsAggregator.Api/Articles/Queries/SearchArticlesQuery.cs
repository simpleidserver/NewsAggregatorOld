using MediatR;
using NewsAggregator.Api.Articles.Results;
using NewsAggregator.Api.Common.Results;

namespace NewsAggregator.Api.Articles.Queries
{
    public class SearchArticlesQuery : IRequest<BaseSearchQueryResult<ArticleResult>>
    {
        public SearchArticlesQuery()
        {
            StartIndex = 0;
            Count = 100;
        }

        public int StartIndex { get; set; }
        public int Count { get; set; }
    }
}
