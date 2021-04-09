using Microsoft.EntityFrameworkCore;
using NewsAggregator.Core.Domains.DataSources;
using NewsAggregator.Core.Repositories;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace NewsAggregator.EF.Repositories
{
    public class DataSourceCommandRepository : IDataSourceCommandRepository
    {
        private readonly NewsAggregatorDBContext _dbContext;

        public DataSourceCommandRepository(NewsAggregatorDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<DataSourceAggregate> Get(string id, CancellationToken cancellationToken)
        {
            return _dbContext.DataSources
                .Include(d => d.Articles)
                .Include(d => d.ExtractionHistories)
                .FirstOrDefaultAsync(d => d.Id == id, cancellationToken);
        }

        public async Task<IEnumerable<DataSourceAggregate>> GetAll(CancellationToken cancellationToken)
        {
            IEnumerable<DataSourceAggregate> result = await _dbContext.DataSources.Include(d => d.ExtractionHistories).ToListAsync(cancellationToken);
            return result;
        }

        public Task Update(IEnumerable<DataSourceAggregate> datasources, CancellationToken cancellationToken)
        {
            _dbContext.DataSources.UpdateRange(datasources);
            return Task.CompletedTask;
        }
        public Task<int> SaveChanges(CancellationToken cancellationToken)
        {
            return _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
