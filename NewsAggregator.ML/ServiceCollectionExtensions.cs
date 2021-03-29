using NewsAggregator.Domain;
using NewsAggregator.Domain.Sessions.Events;
using NewsAggregator.ML;
using NewsAggregator.ML.Articles;
using NewsAggregator.ML.EventHandlers;
using NewsAggregator.ML.Factories;
using NewsAggregator.ML.Infrastructures.Bus;
using NewsAggregator.ML.Infrastructures.Locks;
using NewsAggregator.ML.Jobs;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddNewsAggregatorML(this IServiceCollection services)
        {
            services.Configure<WordEmbeddingOption>((o) => { });
            services.AddTransient<INextArticleRecommenderJob, NextArticleRecommenderJob>();
            services.AddTransient<IRSSArticleExtractorJob, RSSArticleExtractorJob>();
            services.AddTransient<IArticleManager, ArticleManager>();
            services.AddTransient<IHttpClientFactory, HttpClientFactory>();
            services.AddTransient(typeof(IEventHandler<SessionInteractionOccuredEvent>), typeof(SessionEventHandler));
            services.AddSingleton<IDistributedLock, InMemoryDistributedLock>();
            services.AddSingleton<IMessageBroker, InMemoryMessageBroker>();
            services.AddNewsAggregatorCore();
            return services;
        }
    }
}
