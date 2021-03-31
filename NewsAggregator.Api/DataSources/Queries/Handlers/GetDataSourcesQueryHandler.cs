using MediatR;
using NewsAggregator.Core.QueryResults;
using NewsAggregator.Core.Repositories;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace NewsAggregator.Api.DataSources.Queries.Handlers
{
    public class GetDataSourcesQueryHandler : IRequestHandler<GetDataSourcesQuery, IEnumerable<DataSourceQueryResult>>
    {
        private readonly IDataSourceQueryRepository _dataSourceQueryRepository;

        public GetDataSourcesQueryHandler(IDataSourceQueryRepository dataSourceQueryRepository)
        {
            _dataSourceQueryRepository = dataSourceQueryRepository;
        }

        public Task<IEnumerable<DataSourceQueryResult>> Handle(GetDataSourcesQuery request, CancellationToken cancellationToken)
        {
            return _dataSourceQueryRepository.Get(request.DataSourceIds, cancellationToken);
        }
    }
}
