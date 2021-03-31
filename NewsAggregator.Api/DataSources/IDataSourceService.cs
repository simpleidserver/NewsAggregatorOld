using NewsAggregator.Core.QueryResults;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace NewsAggregator.Api.DataSources
{
    public interface IDataSourceService
    {
        Task<IEnumerable<DataSourceQueryResult>> Get(IEnumerable<string> dataSourceIds, CancellationToken cancellationToken);
    }
}