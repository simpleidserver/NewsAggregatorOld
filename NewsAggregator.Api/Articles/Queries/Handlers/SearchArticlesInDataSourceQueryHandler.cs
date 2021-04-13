using MediatR;
using Microsoft.Extensions.Logging;
using NewsAggregator.Api.Resources;
using NewsAggregator.Core.Exceptions;
using NewsAggregator.Core.QueryResults;
using NewsAggregator.Core.Repositories;
using NewsAggregator.Core.Repositories.Parameters;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace NewsAggregator.Api.Articles.Queries.Handlers
{
    public class SearchArticlesInDataSourceQueryHandler : IRequestHandler<SearchArticlesInDataSourceQuery, SearchQueryResult<ArticleQueryResult>>
    {
        private readonly ILogger<SearchArticlesInDataSourceQueryHandler> _logger;
        private readonly IDataSourceQueryRepository _dataSouceQueryRepository;
        private readonly IArticleQueryRepository _articleQueryRepository;

        public SearchArticlesInDataSourceQueryHandler(
            ILogger<SearchArticlesInDataSourceQueryHandler> logger,
            IDataSourceQueryRepository dataSourceQueryRepository,
            IArticleQueryRepository articleQueryRepository)
        {
            _logger = logger;
            _dataSouceQueryRepository = dataSourceQueryRepository;
            _articleQueryRepository = articleQueryRepository;
        }

        public async Task<SearchQueryResult<ArticleQueryResult>> Handle(SearchArticlesInDataSourceQuery request, CancellationToken cancellationToken)
        {
            var dataSource = await _dataSouceQueryRepository.Get(new List<string> { request.DataSourceId }, cancellationToken);
            if (dataSource == null)
            {
                _logger.LogError($"the datasource {request.DataSourceId} doesn't exist");
                throw new NewsAggregatorResourceNotFoundException(string.Format(Global.DataSourceDoesntExist, request.DataSourceId));
            }

            return await _articleQueryRepository.SearchInDataSource(new SearchArticlesInDataSourceParameter
            {
                Count = request.Count,
                DataSourceId = request.DataSourceId,
                Direction = request.Direction,
                Order = request.Order,
                StartIndex = request.StartIndex,
                UserId = request.UserId
            }, cancellationToken);
        }
    }
}
