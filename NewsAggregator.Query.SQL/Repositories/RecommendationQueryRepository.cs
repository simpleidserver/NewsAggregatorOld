using Dapper;
using NewsAggregator.Core.QueryResults;
using NewsAggregator.Core.Repositories;
using NewsAggregator.Core.Repositories.Parameters;
using System.Threading;
using System.Threading.Tasks;

namespace NewsAggregator.Query.SQL.Repositories
{
    public class RecommendationQueryRepository : IRecommendationQueryRepository
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public RecommendationQueryRepository(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<SearchQueryResult<RecommendationQueryResult>> Search(SearchRecommendationsParameter parameter, CancellationToken cancellationToken)
        {
            const string sql = "SELECT	[dbo].[RecommendationArticle].[Score] as [ArticleScore], "+
                        "[dbo].[Articles].[Id] as [ArticleId], "+
                        "[dbo].[Articles].[Language] as [ArticleLanguage], "+
                        "[dbo].[Articles].[Title] as [ArticleTitle], "+
                        "[dbo].[Articles].[Summary] as [ArticleSummary], "+
                        "[dbo].[Articles].[ExternalId] as [ArticleExternalId] "+
                        "FROM [dbo].[RecommendationArticle] "+
                        "INNER JOIN [dbo].[Articles] ON [dbo].[RecommendationArticle].[ArticleId] = [dbo].[Articles].[Id] "+
                        "WHERE [dbo].[RecommendationArticle].[RecommendationAggregateId] = ( " +
                        "SELECT TOP(1) [Id] "+
                        "FROM[dbo].[Recommendations] "+
                        "WHERE [UserId] = @userId "+
                        "ORDER BY [dbo].[Recommendations].[CreateDateTime] DESC )"+
                        "ORDER BY[dbo].[RecommendationArticle].[Score] DESC "+
                        "OFFSET @startIndex ROWS "+
                        "FETCH NEXT @count ROWS ONLY";
            var connection = _sqlConnectionFactory.GetOpenConnection();
            var result = await connection.QueryAsync<RecommendationQueryResult>(sql, new
            {
                userId = parameter.UserId,
                startIndex = parameter.StartIndex,
                count = parameter.Count
            });
            return new SearchQueryResult<RecommendationQueryResult>
            {
                Content = result,
                Count = parameter.Count,
                StartIndex = parameter.StartIndex
            };
        }
    }
}
