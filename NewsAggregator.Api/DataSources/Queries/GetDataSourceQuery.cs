using MediatR;
using NewsAggregator.Core.QueryResults;

namespace NewsAggregator.Api.DataSources.Queries
{
    public class GetDataSourceQuery: IRequest<DataSourceQueryResult>
    {
        public string DataSourceId { get; set; }
    }
}
