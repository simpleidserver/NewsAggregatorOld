using Dapper;
using NewsAggregator.Core.QueryResults;
using NewsAggregator.Core.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NewsAggregator.Query.SQL.Repositories
{
    public class FeedQueryRepository : IFeedQueryRepository
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public FeedQueryRepository(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public Task<IEnumerable<FeedQueryResult>> GetFeeds(string userId)
        {
            const string sql = "SELECT " +
                            "[feeds].[Title] as [FeedTitle], " +
                            "[datasources].[Title] as [DatasourceTitle], " +
                            "[datasources].[Description] as [DatasourceDescription] " +
                            "FROM [dbo].[Feeds] as [feeds] " +
                            "LEFT JOIN [dbo].[FeedDatasource] as [feedDatasource] ON [feedDatasource].[FeedAggregateId] = [feeds].[Id] " +
                            "LEFT JOIN [dbo].[DataSources] as [datasources] ON [datasources].[Id] = [feedDatasource].[DatasourceId] " +
                            "where [feeds].[UserId] = @userId";
            var connection = _sqlConnectionFactory.GetOpenConnection();
            return connection.QueryAsync<FeedQueryResult>(sql, new
            {
                userId
            });
        }
    }
}
