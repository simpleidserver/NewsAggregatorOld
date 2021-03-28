using NewsAggregator.ML;
using NewsAggregator.ML.Articles;
using NewsAggregator.ML.Factories;
using NewsAggregator.ML.Infrastructures.Bus;
using NewsAggregator.ML.Infrastructures.Jobs;
using NewsAggregator.ML.Infrastructures.Locks;
using NewsAggregator.ML.Jobs;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddNewsAggregatorML(this IServiceCollection services)
        {
            services.Configure<WordEmbeddingOption>((o) => { });
            services.AddTransient<IMLJobServer, MLJobServer>();
            // services.AddTransient<IJob, NextArticleRecommenderJob>();
            // services.AddTransient<IJob, ProcessDomainEventsJob>();
            services.AddTransient<IJob, RSSArticleExtractorJob>();
            services.AddTransient<IArticleManager, ArticleManager>();
            services.AddTransient<IHttpClientFactory, HttpClientFactory>();
            // services.AddTransient(typeof(IEventHandler<>), typeof(SessionEventHandler));
            services.AddSingleton<IDistributedLock, InMemoryDistributedLock>();
            services.AddSingleton<IMessageBroker, InMemoryMessageBroker>();
            services.AddNewsAggregatorCore();
            return services;
        }
    }
}
