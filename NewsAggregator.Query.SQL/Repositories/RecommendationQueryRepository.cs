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

        public async Task<SearchQueryResult<ArticleQueryResult>> Search(SearchRecommendationsParameter parameter, CancellationToken cancellationToken)
        {
            const string sql = "SELECT [dbo].[RecommendationArticle].[Score] as [ArticleScore], "+
                       "[dbo].[Articles].[Id] as [Id], " +
                       "[dbo].[Articles].[ExternalId] as [ExternalId], " +
                       "[dbo].[Articles].[Title] as [Title], " +
                       "[dbo].[Articles].[Summary] as [Summary], " +
                       "[dbo].[Articles].[Language] as [Language], " +
                       "[dbo].[Articles].[PublishDate] as [PublishDate], " +
                       "CONCAT([dbo].[Articles].[Title], ' ', [dbo].[Articles].[Summary]) as [Text], " +
                       "[dbo].[ArticleLike].[ActionDateTime] as [LikeActionDateTime], " +
                       "[dbo].[DataSources].[Id] as [DatasourceId], " +
                       "[dbo].[DataSources].[Title] as [DatasourceTitle], " +
                       "[NbRead], " +
                       "[NbLikes] " +
                       " FROM[dbo].[RecommendationArticle] " +
                       "INNER JOIN[dbo].[Articles] ON[dbo].[RecommendationArticle].[ArticleId] = [dbo].[Articles].[Id] " +
                       "LEFT OUTER JOIN[dbo].[ArticleLike] ON[dbo].[ArticleLike].[ArticleAggregateId] = [dbo].[Articles].[Id] " +
                       "INNER JOIN[dbo].[DataSources] ON[dbo].[DataSources].[Id] = [dbo].[Articles].[DataSourceId] " +
                       "WHERE[dbo].[RecommendationArticle].[RecommendationAggregateId] = ( " +
                       "SELECT TOP(1)[Id] " +
                       "FROM[dbo].[Recommendations] " +
                       "WHERE[UserId] = 'administrator' " +
                       "ORDER BY[dbo].[Recommendations].[CreateDateTime] DESC ) " +
                       "ORDER BY[dbo].[RecommendationArticle].[Score] DESC " +
                       "OFFSET 0 ROWS " +
                       "FETCH NEXT 10 ROWS ONLY";
            var connection = _sqlConnectionFactory.GetOpenConnection();
            var result = await connection.QueryAsync<ArticleQueryResult>(sql, new
            {
                userId = parameter.UserId,
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
