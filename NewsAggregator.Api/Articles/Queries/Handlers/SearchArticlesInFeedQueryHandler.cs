using MediatR;
using NewsAggregator.Core.Parameters;
using NewsAggregator.Core.QueryResults;
using NewsAggregator.Core.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace NewsAggregator.Api.Articles.Queries.Handlers
{
    public class SearchArticlesInFeedQueryHandler : IRequestHandler<SearchArticlesInFeedQuery, SearchQueryResult<ArticleQueryResult>>
    {
        private readonly IArticleQueryRepository _articleQueryRepository;

        public SearchArticlesInFeedQueryHandler(IArticleQueryRepository articleQueryRepository)
        {
            _articleQueryRepository = articleQueryRepository;
        }

        public Task<SearchQueryResult<ArticleQueryResult>> Handle(SearchArticlesInFeedQuery request, CancellationToken cancellationToken)
        {
            return _articleQueryRepository.SearchInFeeds(new SearchArticlesInFeedParameter
            {
                Count = request.Count,
                DataSourceId = request.DataSourceId,
                StartIndex = request.StartIndex
            }, cancellationToken);
        }
    }
}
