using NewsAggregator.ML;
using NewsAggregator.ML.Articles;
using NewsAggregator.ML.Articles.Operations;
using NewsAggregator.ML.Factories;
using NewsAggregator.ML.Jobs;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddNewsAggregatorML(this IServiceCollection services)
        {
            services.Configure<WordEmbeddingOption>((o) => { });
            services.AddTransient<INextArticleRecommenderJob, NextArticleRecommenderJob>();
            services.AddTransient<IArticleExtractorJob, ArticleExtractorJob>();
            services.AddTransient<IArticleManager, ArticleManager>();
            services.AddTransient<IHttpClientFactory, HttpClientFactory>();
            services.AddTransient<IArticleOperation, TrainWord2VecOperation>();
            services.AddTransient<IArticleOperation, TrainLDAOperation>();
            return services;
        }
    }
}
