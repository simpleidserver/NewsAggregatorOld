using Microsoft.EntityFrameworkCore;
using NewsAggregator.Core.Repositories;
using NewsAggregator.EF;
using NewsAggregator.EF.Repositories;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddNewsAggregatorEF(this IServiceCollection services, Action<DbContextOptionsBuilder> optionsAction = null)
        {
            services.AddDbContext<NewsAggregatorDBContext>(optionsAction, ServiceLifetime.Transient);
            services.AddTransient<IRSSFeedRepository, RSSFeedRepository>();
            services.AddTransient<IArticleRepository, ArticleRepository>();
            return services;
        }
    }
}