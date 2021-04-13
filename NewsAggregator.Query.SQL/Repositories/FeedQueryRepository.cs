using Dapper;
using NewsAggregator.Core.QueryResults;
using NewsAggregator.Core.Repositories;
using NewsAggregator.Core.Repositories.Parameters;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading;
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

        public Task<FeedQueryResult> Get(string feedId, CancellationToken cancellationToken)
        {
            const string sql = "SELECT [feeds].[Id] as [FeedId], " +
                            "[feeds].[Title] as [FeedTitle], " +
                            "[feeds].[UserId] as [UserId] " +
                            "FROM [dbo].[Feeds] as [feeds] " +
                            "where [feeds].[Id] = @feedId";
            var connection = _sqlConnectionFactory.GetOpenConnection();
            return connection.QuerySingleOrDefaultAsync<FeedQueryResult>(sql, new
            {
                feedId
            });
        }

        public Task<IEnumerable<DetailedFeedQueryResult>> GetFeeds(string userId)
        {
            const string sql = "SELECT [feeds].[Id] as [FeedId], " +
                            "[feeds].[Title] as [FeedTitle], " +
                            "[datasources].[Id] as [DatasourceId], " +
                            "[datasources].[Title] as [DatasourceTitle], " +
                            "[datasources].[Description] as [DatasourceDescription], " +
                            "[datasources].[NbFollowers] as [NbFollowers], " +
                            "[datasources].[NbStoriesPerMonth] as [NbStoriesPerMonth] " +
                            "FROM [dbo].[Feeds] as [feeds] " +
                            "LEFT JOIN [dbo].[FeedDatasource] as [feedDatasource] ON [feedDatasource].[FeedAggregateId] = [feeds].[Id] " +
                            "LEFT JOIN [dbo].[DataSources] as [datasources] ON [datasources].[Id] = [feedDatasource].[DatasourceId] " +
                            "where [feeds].[UserId] = @userId";
            var connection = _sqlConnectionFactory.GetOpenConnection();
            return connection.QueryAsync<DetailedFeedQueryResult>(sql, new
            {
                userId
            });
        }

        public async Task<SearchQueryResult<DetailedFeedQueryResult>> SearchFeeds(SearchFeedParameter parameter, CancellationToken cancellationToken)
        {
            string sql = "SELECT [feeds].[Id] as [FeedId], " +
                        "[feeds].[Title] as [FeedTitle], " +
                        "[NewsAggregator].[dbo].[DataSources].Id as [DatasourceId], " +
                        "[NewsAggregator].[dbo].[DataSources].Title as [DatasourceTitle], " +
                        "[NewsAggregator].[dbo].[DataSources].Description as [DatasourceDescription], " +
                        "[NewsAggregator].[dbo].[DataSources].NbFollowers as [NbFollowers], " +
                        "[NewsAggregator].[dbo].[DataSources].NbStoriesPerMonth as [NbStoriesPerMonth] " +
                        "FROM [NewsAggregator].[dbo].[Feeds] as [feeds] " +
                        "INNER JOIN [NewsAggregator].[dbo].[FeedDatasource] ON[NewsAggregator].[dbo].[FeedDatasource].[FeedAggregateId] = [feeds].[Id] " +
                        "INNER JOIN [NewsAggregator].[dbo].[DataSources] ON[NewsAggregator].[dbo].[FeedDatasource].[DatasourceId] = [NewsAggregator].[dbo].[DataSources].[Id] " +
                        "where [feeds].UserId = @userId ";
            if (!string.IsNullOrWhiteSpace(parameter.FeedTitle))
            {
                sql += "AND [feeds].[Title] LIKE @feedTitle ";
            }

            if (parameter.DatasourceIds != null && parameter.DatasourceIds.Any())
            {
                sql += "AND [DataSources].[Id] IN @datasourceIds ";
            }

            if (parameter.FollowersFilter != null)
            {
                switch(parameter.FollowersFilter.Value)
                {
                    case FollowerFilterTypes.LessThen100Followers:
                        sql += "AND [DataSources].[NbFollowers] < 100 ";
                        break;
                    case FollowerFilterTypes.MoreThan100AndLessThen1000Followers:
                        sql += "AND [DataSources].[NbFollowers] >= 100 ";
                        sql += "AND [DataSources].[NbFollowers] <= 1000 ";
                        break;
                    case FollowerFilterTypes.MoreThan1000Followers:
                        sql += "AND [DataSources].[NbFollowers] > 1000 ";
                        break;
                }
            }

            if (parameter.StoriesFilter != null)
            {
                switch (parameter.StoriesFilter.Value)
                {
                    case NumberStoriesFilterTypes.LessThen100Stories:
                        sql += "AND [DataSources].[NbStoriesPerMonth] < 100 ";
                        break;
                    case NumberStoriesFilterTypes.LessThan1000AndMoreThan100Stories:
                        sql += "AND [DataSources].[NbStoriesPerMonth] >= 100 ";
                        sql += "AND [DataSources].[NbStoriesPerMonth] <= 1000 ";
                        break;
                    case NumberStoriesFilterTypes.MoreThan1000Stories:
                        sql += "AND [DataSources].[NbStoriesPerMonth] >= 1000 ";
                        break;
                }
            }

            if (parameter.IsPaginationEnabled)
            {
                sql += "ORDER BY [FeedTitle] ASC " +
                       "OFFSET @startIndex ROWS " +
                       "FETCH NEXT @count ROWS ONLY";
            }

            var connection = _sqlConnectionFactory.GetOpenConnection();
            dynamic dynObject = new ExpandoObject();
            dynObject.userId = parameter.UserId;
            dynObject.startIndex = parameter.StartIndex;
            dynObject.count = parameter.Count;
            if (!string.IsNullOrWhiteSpace(parameter.FeedTitle))
            {
                dynObject.feedTitle = $"%{parameter.FeedTitle}%";
            }

            if (parameter.DatasourceIds != null && parameter.DatasourceIds.Any())
            {
                dynObject.datasourceIds = parameter.DatasourceIds;
            }

            var result = await connection.QueryAsync<DetailedFeedQueryResult>(sql, (object)dynObject);
            return new SearchQueryResult<DetailedFeedQueryResult>
            {
                Content = result,
                Count = parameter.Count,
                StartIndex = parameter.StartIndex
            };
        }
    }
}
