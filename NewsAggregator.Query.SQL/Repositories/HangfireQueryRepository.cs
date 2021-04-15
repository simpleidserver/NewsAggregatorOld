using Dapper;
using NewsAggregator.Core.QueryResults;
using NewsAggregator.Core.Repositories;
using NewsAggregator.Core.Repositories.Parameters;
using System.Threading;
using System.Threading.Tasks;

namespace NewsAggregator.Query.SQL.Repositories
{
    public class HangfireQueryRepository : IHangfireQueryRepository
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public HangfireQueryRepository(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<SearchQueryResult<HangfireJobQueryResult>> SearchJobs(SearchHangfireJobsParameter parameter, CancellationToken cancellationToken)
        {
            const string sql = "SELECT " +
                            "[Id], " +
                            "[InvocationData], " +
                            "[StateName], " +
                            "[CreatedAt] "+
                            "FROM [HangFire].[Job] " +
                            "ORDER BY [CreatedAt] DESC " +
                            "OFFSET @startIndex ROWS " +
                            "FETCH NEXT @count ROWS ONLY";
            const string countSql = "SELECT COUNT(*) " +
                            "FROM [HangFire].[Job] ";
            var connection = _sqlConnectionFactory.GetOpenConnection();
            var count = await connection.QuerySingleAsync<int>(countSql);
            var result = await connection.QueryAsync<HangfireJobQueryResult>(sql, new
            {
                startIndex = parameter.StartIndex,
                count = parameter.Count
            });
            return new SearchQueryResult<HangfireJobQueryResult>
            {
                Content = result,
                Count = count,
                StartIndex = parameter.StartIndex
            };
        }

        public async Task<SearchQueryResult<HangfireJobStateQueryResult>> SearchStates(SearchHangfireJobStatesParameter parameter, CancellationToken cancellationToken)
        {
            const string sql = "SELECT " +
                            "[Name], " +
                            "[Reason], " +
                            "[CreatedAt], " +
                            "[Data] " +
                            "FROM [HangFire].[State] " +
                            "WHERE [JobId] = @jobId " +
                            "ORDER BY [CreatedAt] DESC " +
                            "OFFSET @startIndex ROWS " +
                            "FETCH NEXT @count ROWS ONLY";
            const string countSql = "SELECT COUNT(*) " +
                            "FROM [HangFire].[State] " +
                            "WHERE [JobId] = @jobId ";
            var connection = _sqlConnectionFactory.GetOpenConnection();
            var count = await connection.QuerySingleAsync<int>(countSql, new
            {
                jobId = parameter.JobId
            });
            var result = await connection.QueryAsync<HangfireJobStateQueryResult>(sql, new
            {
                jobId = parameter.JobId,
                startIndex = parameter.StartIndex,
                count = parameter.Count
            });
            return new SearchQueryResult<HangfireJobStateQueryResult>
            {
                Content = result,
                Count = count,
                StartIndex = parameter.StartIndex
            };
        }
    }
}
