using Dapper;
using NewsAggregator.Core.QueryResults;
using NewsAggregator.Core.Repositories;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace NewsAggregator.Query.SQL.Repositories
{
    public class SessionQueryRepository : ISessionQueryRepository
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public SessionQueryRepository(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public string GetSessionActionsSQL(string sessionId)
        {
            return string.Format("SELECT [Id], "+
                "[UserId], "+
                "[InteractionType], "+
                "[ActionDateTime], " +
                "[ArticleId], " +
                "[ArticleLanguage] " +
                "[FROM] [dbo].[SessionAction] " +
                "INNER JOIN [dbo].[SessionAggregate] ON [dbo].[SessionAggregate].[Id] = [dbo].[SessionAction].[SessionAggregateId] " +
                "WHERE [dbo].[SessionAction].[SessionAggregateId] = '{0}'");
        }

        public Task<IEnumerable<SessionQueryResult>> GetAll(CancellationToken cancellationToken)
        {
            const string sql = "SELECT " +
                            "[Id], " +
                            "[UserId], " +
                            "[CreateDateTime] " +
                            "FROM [dbo].[SessionAggregate] " +
                            "ORDER BY [CreateDateTime] DESC";
            var connection = _sqlConnectionFactory.GetOpenConnection();
            return connection.QueryAsync<SessionQueryResult>(sql);
        }
    }
}
