using Dapper;
using NewsAggregator.Core.QueryResults;
using NewsAggregator.Core.Repositories;
using NewsAggregator.Core.Repositories.Parameters;
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
            var nbPages = Math.Ceiling((double)nbArticles / (double)count);
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

        public async Task<SearchQueryResult<ArticleQueryResult>> SearchInDataSource(SearchArticlesInDataSourceParameter parameter, CancellationToken cancellationToken)
        {
            const string sql = "SELECT " +
                            "[dbo].[Articles].[Id], " +
                            "[ExternalId], " +
                            "[dbo].[Articles].[Title], " +
                            "[Summary], " +
                            "[Language], " +
                            "[PublishDate], " +
                            "CONCAT([dbo].[Articles].[Title], ' ', [Summary]) as [Text], " +
                            "[articleLike].[ActionDateTime] as [LikeActionDateTime], " +
                            "[articleR].[ActionDateTime] as [ReadActionDateTime], " + 
                            "[dbo].[DataSources].[Id] as [DatasourceId], " +
                            "[dbo].[DataSources].[Title] as [DatasourceTitle], "+
                            "[NbRead], " +
                            "[NbLikes] " +
                            "FROM [dbo].[Articles] " +
                            "OUTER APPLY ( " +
                                "SELECT TOP 1 [ActionDateTime] " +
                                "FROM [dbo].[ArticleLike] " +
                                "where [dbo].[ArticleLike].[ArticleAggregateId] = [dbo].[Articles].[Id] " +
                            ") [articleLike] " +
                            "OUTER APPLY ( " +
                                "SELECT TOP 1 [UserId], [IsHidden], [ActionDateTime] " +
                                "FROM [dbo].[ArticleRead] " +
                                "WHERE [dbo].[ArticleRead].[ArticleAggregateId] = [dbo].[Articles].[Id]" +
                            ") [articleR] " +
                            "INNER JOIN [dbo].[DataSources] ON [dbo].[DataSources].[Id] = [dbo].[Articles].[DataSourceId] " + 
                            "WHERE [DataSourceId] = @datasourceId " +
                            "AND ([articleR].[UserId] IS NOT NULL AND [articleR].[UserId] = @userId AND [articleR].IsHidden = 0) OR [articleR].[UserId] IS NULL " +
                            "ORDER BY [PublishDate] DESC " +
                            "OFFSET @startIndex ROWS " +
                            "FETCH NEXT @count ROWS ONLY";
            var connection = _sqlConnectionFactory.GetOpenConnection();
            var result = await connection.QueryAsync<ArticleQueryResult>(sql, new
            {
                datasourceId = parameter.DataSourceId,
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

        public async Task<SearchQueryResult<ArticleQueryResult>> SearchInFeed(SearchArticlesInFeedParameter parameter, CancellationToken cancellationToken)
        {
            const string sql = "SELECT " +
                    "[dbo].[Articles].[Id], " +
                    "[ExternalId], " +
                    "[dbo].[Articles].[Title], " +
                    "[Summary], " +
                    "[Language], " +
                    "[PublishDate], " +
                    "CONCAT([dbo].[Articles].[Title], ' ', [Summary]) as [Text], " +
                    "[articleLike].[ActionDateTime] as [LikeActionDateTime], " +
                    "[articleR].[ActionDateTime] as [ReadActionDateTime], " +
                    "[dbo].[DataSources].[Id] as [DatasourceId], " +
                    "[dbo].[DataSources].[Title] as [DatasourceTitle], " +
                    "[NbRead], " +
                    "[NbLikes] " +
                    "FROM [dbo].[Articles] " +
                    "INNER JOIN [dbo].[FeedDatasource] ON[dbo].[FeedDatasource].[DatasourceId] = [dbo].[Articles].[DataSourceId] " +
                    "OUTER APPLY ( "+
                        "SELECT TOP 1 [ActionDateTime] " +
                        "FROM [dbo].[ArticleLike] " +
                        "where [dbo].[ArticleLike].[ArticleAggregateId] = [dbo].[Articles].[Id] "+
                    ") [articleLike] "+
                    "OUTER APPLY ( "+
                        "SELECT TOP 1 [UserId], [IsHidden], [ActionDateTime] "+
                        "FROM [dbo].[ArticleRead] " +
                        "WHERE [dbo].[ArticleRead].[ArticleAggregateId] = [dbo].[Articles].[Id]" +
                    ") [articleR] " +
                    "INNER JOIN [dbo].[DataSources] ON [dbo].[DataSources].[Id] = [dbo].[Articles].[DataSourceId] " +
                    "WHERE [dbo].[FeedDatasource].FeedAggregateId = @feedId " +
                    "AND ([articleR].[UserId] IS NOT NULL AND [articleR].[UserId] = @userId AND [articleR].IsHidden = 0) OR [articleR].[UserId] IS NULL " +
                    "ORDER BY [PublishDate] DESC " +
                    "OFFSET @startIndex ROWS " +
                    "FETCH NEXT @count ROWS ONLY";
            var connection = _sqlConnectionFactory.GetOpenConnection();
            var result = await connection.QueryAsync<ArticleQueryResult>(sql, new
            {
                feedId = parameter.FeedId,
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
