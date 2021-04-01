using Dapper;
using NewsAggregator.Core.Parameters;
using NewsAggregator.Core.QueryResults;
using NewsAggregator.Core.Repositories;
using System;
using System.Collections.Generic;
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

        public string GetArticlesByLanguageSQL(string language)
        {
            const string sql = "SELECT [Id], "
                + " [ExternalId], "
                + " [Title], "
                + " [Summary], "
                + " CONCAT([Title], ' ', [Summary]) as [Text] "
                + " FROM [dbo].[Articles] "
                + " WHERE [Language] = '{0}'";
            return string.Format(sql, language);
        }

        public IEnumerable<IEnumerable<ArticleQueryResult>> GetAll(string language, int count = 500)
        {
            const string countSql = "SELECT COUNT(*) FROM [dbo].[Articles] where language = @language";
            const string selectArticles = "SELECT " +
                            "[Id], "+
                            "[ExternalId], " +
                            "[Title], " +
                            "[Summary], " +
                            "[Language], " +
                            "[PublishDate], " +
                            " CONCAT([Title], ' ', [Summary]) as [Text] " +
                            "FROM [dbo].[Articles] " +
                            "WHERE [Language] = @language " +
                            "ORDER BY [PublishDate] DESC "+
                            "OFFSET @startIndex ROWS "+
                            "FETCH NEXT @count ROWS ONLY";
            var connection = _sqlConnectionFactory.GetOpenConnection();
            var nbArticles = connection.QuerySingle<int>(countSql, new
            {
                language = language
            });
            var nbPages = Math.Ceiling((decimal)((nbArticles / count) * 100) / 100);
            for(int currentPage = 0; currentPage < nbPages; currentPage++)
            {
                int startIndex = count * currentPage;
                var result = connection.Query<ArticleQueryResult>(selectArticles, new
                {
                    language = language,
                    startIndex = startIndex,
                    count = count
                });
                yield return result;
            }

            yield return new ArticleQueryResult[0];
        }

        public async Task<SearchQueryResult<ArticleQueryResult>> SearchInFeeds(SearchArticlesInFeedParameter parameter, CancellationToken cancellationToken)
        {
            const string sql = "SELECT " +
                            "[Id], "+
                            "[ExternalId], " +
                            "[Title], " +
                            "[Summary], " +
                            "[Language], " +
                            "[PublishDate], " +
                            " CONCAT([Title], ' ', [Summary]) as [Text] " +
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
