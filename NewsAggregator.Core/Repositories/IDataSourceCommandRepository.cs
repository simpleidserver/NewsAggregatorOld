using NewsAggregator.Core.Domains.DataSources;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace NewsAggregator.Core.Repositories
{
    public interface IDataSourceCommandRepository
    {
        Task<DataSourceAggregate> Get(string id, CancellationToken cancellationToken);
        Task<IEnumerable<DataSourceAggregate>> GetAll(CancellationToken cancellationToken);
        Task Update(IEnumerable<DataSourceAggregate> datasources, CancellationToken cancellationToken);
        Task<int> SaveChanges(CancellationToken cancellationToken);
    }
}
