using Microsoft.Extensions.DependencyInjection;
using NewsAggregator.ML.Infrastructures.Bus;
using NewsAggregator.ML.Infrastructures.Jobs;
using NewsAggregator.ML.Infrastructures.Locks;
using NewsAggregator.ML.Jobs;

namespace NewsAggregator.ML
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddNewsAggregatorML(this IServiceCollection services)
        {
            services.AddTransient<IMLJobServer, MLJobServer>();
            services.AddTransient<IJob, NextArticleRecommenderJob>();
            services.AddSingleton<IDistributedLock, InMemoryDistributedLock>();
            services.AddSingleton<IMessageBroker, InMemoryMessageBroker>();
            return services;
        }
    }
}
