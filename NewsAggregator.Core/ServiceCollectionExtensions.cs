using NewsAggregator.Core.Repositories;
using NewsAggregator.Core.Repositories.InMemory;
using NewsAggregator.Domain.RSSFeeds;
using System.Collections.Concurrent;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddNewsAggregatorCore(this IServiceCollection services)
        {
            var rssFeeds = new ConcurrentBag<RSSFeedAggregate>
            {
                RSSFeedAggregate.Create("BBC", "Top stories", "http://feeds.bbci.co.uk/news/rss.xml")
            };
            services.AddSingleton<IRSSFeedRepository>(new InMemoryRSSFeedRepository(rssFeeds));
            return services;
        }
    }
}
