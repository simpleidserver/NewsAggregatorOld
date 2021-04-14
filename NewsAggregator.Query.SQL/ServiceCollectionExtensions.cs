
using NewsAggregator.Core.Repositories;
using NewsAggregator.Query.SQL;
using NewsAggregator.Query.SQL.Repositories;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddNewsAggregatorQuerySQL(this IServiceCollection serviceCollection, string connectionString)
        {
            serviceCollection.AddSingleton(typeof(ISqlConnectionFactory), new SqlConnectionFactory(connectionString));
            serviceCollection.AddTransient<IFeedQueryRepository, FeedQueryRepository>();
            serviceCollection.AddTransient<IDataSourceQueryRepository, DataSourceQueryRepository>();
            serviceCollection.AddTransient<IArticleQueryRepository, ArticleQueryRepository>();
            serviceCollection.AddTransient<ISessionQueryRepository, SessionQueryRepository>();
            serviceCollection.AddTransient<IRecommendationQueryRepository, RecommendationQueryRepository>();
            serviceCollection.AddTransient<IHangfireQueryRepository, HangfireQueryRepository>();
            return serviceCollection;
        }
    }
}
