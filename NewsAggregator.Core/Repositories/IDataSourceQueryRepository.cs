using NewsAggregator.Core.QueryResults;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace NewsAggregator.Core.Repositories
{
    public interface IDataSourceQueryRepository
    {
        Task<IEnumerable<DataSourceQueryResult>> Get(IEnumerable<string> datasourceIds, CancellationToken cancellationToken);
    }
}
