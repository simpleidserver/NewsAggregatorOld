using Hangfire;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NewsAggregator.Domain.RSSFeeds;
using NewsAggregator.EF;
using NewsAggregator.ML.Jobs;
using System;
using System.Reflection;
using System.Threading;

namespace NewsAggregator.ML.Startup
{
    public class JobLauncher
    {
        public static void Launch()
        {
            const string connectionString = "Data Source=DESKTOP-T4INEAM\\SQLEXPRESS;Initial Catalog=NewsAggregator;Integrated Security=True";
            var migrationsAssembly = typeof(JobLauncher).GetTypeInfo().Assembly.GetName().Name;
            var serviceCollection = new ServiceCollection();
            serviceCollection
                .AddNewsAggregatorML()
                .AddNewsAggregatorEF(o => o.UseSqlServer(connectionString, o => o.MigrationsAssembly(migrationsAssembly)));
            serviceCollection.AddLogging(logging =>
            {
                logging.AddConsole();
            });
            var serviceProvider = serviceCollection.BuildServiceProvider();
            Seed(serviceProvider);
            GlobalConfiguration.Configuration
                .UseSqlServerStorage(connectionString)
                .UseActivator(new HangfireActivator(serviceProvider));
            using (var server = new BackgroundJobServer())
            {
                RecurringJob.AddOrUpdate<IRSSArticleExtractorJob>("rssExtractArticles", j => j.Run(CancellationToken.None), Cron.MinuteInterval(5));
                RecurringJob.AddOrUpdate<INextArticleRecommenderJob>("nextArticlesRecommender", j => j.Run(CancellationToken.None), Cron.HourInterval(1));
                Console.WriteLine("Press Enter to quit the application !");
                Console.ReadLine();
            }
        }

        private static void Seed(IServiceProvider serviceProvider)
        {
            using (var dbContext = serviceProvider.GetService<NewsAggregatorDBContext>())
            {
                dbContext.Database.Migrate();
                if (dbContext.RSSFeeds.AnyAsync().Result)
                {
                    return;
                }

                dbContext.RSSFeeds.Add(RSSFeedAggregate.Create("BBC", "Top stories", "http://feeds.bbci.co.uk/news/rss.xml"));
                dbContext.SaveChanges();
            }
        }
    }
}