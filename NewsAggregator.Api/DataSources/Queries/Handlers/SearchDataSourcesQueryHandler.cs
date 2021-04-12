using MediatR;
using NewsAggregator.Core.QueryResults;
using NewsAggregator.Core.Repositories;
using NewsAggregator.Core.Repositories.Parameters;
using System.Threading;
using System.Threading.Tasks;

namespace NewsAggregator.Api.DataSources.Queries.Handlers
{
    public class SearchDataSourcesQueryHandler : IRequestHandler<SearchDataSourcesQuery, SearchQueryResult<DataSourceQueryResult>>
    {
        private readonly IDataSourceQueryRepository _datasourceQueryRepository;

        public SearchDataSourcesQueryHandler(IDataSourceQueryRepository dataSourceQueryRepository)
        {
            _datasourceQueryRepository = dataSourceQueryRepository;
        }

        public Task<SearchQueryResult<DataSourceQueryResult>> Handle(SearchDataSourcesQuery request, CancellationToken cancellationToken)
        {
            return _datasourceQueryRepository.Search(new SearchDataSourceParameter
            {
                Count = request.Count,
                IsPaginationEnabled = request.IsPaginationEnabled,
                StartIndex = request.StartIndex,
                Title = request.Title
            }, cancellationToken);
        }
    }
}
