using Dapper;
using NewsAggregator.Core.QueryResults;
using NewsAggregator.Core.Repositories;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace NewsAggregator.Query.SQL
{
    public class DataSourceQueryRepository : IDataSourceQueryRepository
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public DataSourceQueryRepository(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public Task<IEnumerable<DataSourceQueryResult>> Get(IEnumerable<string> datasourceIds, CancellationToken cancellationToken)
        {
            const string sql = "SELECT " +
                            "[Title], " +
                            "[Description], " +
                            "[Url] " +
                            "FROM [dbo].[DataSources] "+
                            "where [Id] IN @ids";
            var connection = _sqlConnectionFactory.GetOpenConnection();
            return connection.QueryAsync<DataSourceQueryResult>(sql, new
            {
                ids = datasourceIds
            });
        }
    }
}
