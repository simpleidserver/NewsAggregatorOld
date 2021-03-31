using MediatR;
using NewsAggregator.Core.QueryResults;
using System.Collections.Generic;

namespace NewsAggregator.Api.DataSources.Queries
{
    public class GetDataSourcesQuery : IRequest<IEnumerable<DataSourceQueryResult>>
    {
        public GetDataSourcesQuery(IEnumerable<string> datasourceIds)
        {
            DataSourceIds = datasourceIds;
        }

        public IEnumerable<string> DataSourceIds { get; private set; }
    }
}
