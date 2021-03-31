using Dapper;
using NewsAggregator.Core.Parameters;
using NewsAggregator.Core.QueryResults;
using NewsAggregator.Core.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace NewsAggregator.Query.SQL.Repositories
{
    public class ArticleQueryRepository : IArticleQueryRepository
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public ArticleQueryRepository(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<SearchQueryResult<ArticleQueryResult>> SearchInFeeds(SearchArticlesInFeedParameter parameter, CancellationToken cancellationToken)
        {
            const string sql = "SELECT " +
                            "[ExternalId], " +
                            "[Title], " +
                            "[Summary], " +
                            "[Language], " +
                            "[PublishDate] " +
                            "FROM [dbo].[Articles] " +
                            "WHERE [DataSourceId] = @datasourceId " +
                            "ORDER BY [PublishDate] DESC " +
                            "OFFSET @startIndex ROWS " +
                            "FETCH NEXT @count ROWS ONLY";
            var connection = _sqlConnectionFactory.GetOpenConnection();
            var result = await connection.QueryAsync<ArticleQueryResult>(sql, new
            {
                datasourceId =parameter.DataSourceId,
                startIndex = parameter.StartIndex,
                count = parameter.Count
            });
            return new SearchQueryResult<ArticleQueryResult>
            {
                Content = result,
                Count = parameter.Count,
                StartIndex = parameter.StartIndex
            };
        }
    }
}
