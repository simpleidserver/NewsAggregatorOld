using MediatR;
using NewsAggregator.Api.Articles.Results;
using NewsAggregator.Api.Common.Results;
using NewsAggregator.Core.Repositories;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NewsAggregator.Api.Articles.Queries.Handlers
{
    public class SearchArticlesQueryHandler : IRequestHandler<SearchArticlesQuery, BaseSearchQueryResult<ArticleResult>>
    {
        private readonly IArticleRepository _articleRepository;

        public SearchArticlesQueryHandler(IArticleRepository articleRepository)
        {
            _articleRepository = articleRepository;
        }

        public Task<BaseSearchQueryResult<ArticleResult>> Handle(SearchArticlesQuery request, CancellationToken cancellationToken)
        {
            var query = _articleRepository.Query();
            var result = query.OrderByDescending(a => a.PublishDate).Skip(request.StartIndex).Take(request.Count).ToList();
            return Task.FromResult(new BaseSearchQueryResult<ArticleResult>
            {
                Count = request.Count,
                StartIndex = request.StartIndex,
                Content = result.Select(a => ArticleResult.ToDto(a)).ToList()
            });
        }
    }
}
