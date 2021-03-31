using MediatR;
using NewsAggregator.Api.DataSources.Queries;
using NewsAggregator.Core.QueryResults;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace NewsAggregator.Api.DataSources
{
    public class DataSourceService : IDataSourceService
    {
        private readonly IMediator _mediator;

        public DataSourceService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public Task<IEnumerable<DataSourceQueryResult>> Get(IEnumerable<string> dataSourceIds, CancellationToken cancellationToken)
        {
            return _mediator.Send(new GetDataSourcesQuery(dataSourceIds), cancellationToken);
        }
    }
}
