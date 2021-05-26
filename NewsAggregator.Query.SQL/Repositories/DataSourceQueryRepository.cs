using Dapper;
using NewsAggregator.Core.QueryResults;
using NewsAggregator.Core.Repositories;
using NewsAggregator.Core.Repositories.Parameters;
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
                            "[Id], " +
                            "[Title], " +
                            "[Description], " +
                            "[Url], " +
                            "[NbFollowers]," +
                            "[NbStoriesPerMonth] " +
                            "FROM [dbo].[DataSources] "+
                            "where [Id] IN @ids";
            var connection = _sqlConnectionFactory.GetOpenConnection();
            return connection.QueryAsync<DataSourceQueryResult>(sql, new
            {
                ids = datasourceIds
            });
        }

        public async Task<SearchQueryResult<DataSourceQueryResult>> Search(SearchDataSourceParameter parameter, CancellationToken cancellationToken)
        {
            var sql = "SELECT " +
                            "[Id], " +
                            "[Title], " +
                            "[Description], " +
                            "[Url] " +
                            "FROM [dbo].[DataSources] ";
            if (!string.IsNullOrWhiteSpace(parameter.Title)) 
            {
                sql += "where [Title] LIKE @title ";
            }

            if (parameter.IsPaginationEnabled)
            {
                sql += "ORDER BY [Title] ASC " +
                       "OFFSET @startIndex ROWS " +
                       "FETCH NEXT @count ROWS ONLY";
            }

            var connection = _sqlConnectionFactory.GetOpenConnection();
            var result = await connection.QueryAsync<DataSourceQueryResult>(sql, new
            {
                title = $"%{parameter.Title}%",
                startIndex = parameter.StartIndex,
                count = parameter.Count
            });
            return new SearchQueryResult<DataSourceQueryResult>
            {
                Content = result,
                Count = parameter.Count,
                StartIndex = parameter.StartIndex
            };
        }
    }
}
