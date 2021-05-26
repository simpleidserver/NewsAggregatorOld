using Hangfire;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NewsAggregator.Core.Domains.DataSources;
using NewsAggregator.EF;
using NewsAggregator.ML.EventHandlers;
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
            var serviceCollection = new ServiceCollection();
            RegisterNewsAggregatorML(serviceCollection, connectionString);
            RegisterLogging(serviceCollection);
            RegisterMassTransit(serviceCollection);
            var serviceProvider = serviceCollection.BuildServiceProvider();
            Seed(serviceProvider);
            StartBus(serviceProvider);
            GlobalConfiguration.Configuration
                .UseSqlServerStorage(connectionString)
                .UseActivator(new HangfireActivator(serviceProvider));
            using (var server = new BackgroundJobServer(new BackgroundJobServerOptions
            {
                WorkerCount = 1
            }))
            {
                var interval = Cron.MinuteInterval(5);
                RecurringJob.AddOrUpdate<IArticleExtractorJob>("rssExtractArticles", j => j.Run(CancellationToken.None), Cron.MinuteInterval(5));
                // RecurringJob.AddOrUpdate<INextArticleRecommenderJob>("nextArticlesRecommender", j => j.Run(CancellationToken.None), Cron.HourInterval(1));
                Console.WriteLine("Press Enter to quit the application !");
                Console.ReadLine();
            }
        }

        private static void RegisterNewsAggregatorML(IServiceCollection serviceCollection, string connectionString)
        {
            var migrationsAssembly = typeof(JobLauncher).GetTypeInfo().Assembly.GetName().Name;
            serviceCollection
                .AddNewsAggregatorML()
                .AddNewsAggregatorEF(o => o.UseSqlServer(connectionString, o => o.MigrationsAssembly(migrationsAssembly)))
                .AddNewsAggregatorQuerySQL(connectionString);
        }

        private static void RegisterLogging(IServiceCollection serviceCollection)
        {
            serviceCollection.AddLogging(logging =>
            {
                logging.AddConsole();
            });
        }

        private static void RegisterMassTransit(IServiceCollection services)
        {
            services.AddMassTransit(x =>
            {
                x.AddConsumer<ArticleLikedEventConsumer>();
                x.AddConsumer<ArticleReadEventConsumer>();
                x.AddConsumer<ArticleReadAndHideEventConsumer>();
                x.AddConsumer<ArticleAddedEventConsumer>();
                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.ReceiveEndpoint("article-like-listener", e =>
                    {
                        e.ConfigureConsumer<ArticleLikedEventConsumer>(context);
                    });
                    cfg.ReceiveEndpoint("article-read-listener", e =>
                    {
                        e.ConfigureConsumer<ArticleReadEventConsumer>(context);
                    });
                    cfg.ReceiveEndpoint("article-readandhide-listener", e =>
                    {
                        e.ConfigureConsumer<ArticleReadAndHideEventConsumer>(context);
                    });
                    cfg.ReceiveEndpoint("article-add-listener", e =>
                    {
                        e.ConfigureConsumer<ArticleAddedEventConsumer>(context);
                    });
                });
            });
        }

        private static void Seed(IServiceProvider serviceProvider)
        {
            using (var dbContext = serviceProvider.GetService<NewsAggregatorDBContext>())
            {
                dbContext.Database.Migrate();
                if (dbContext.DataSources.AnyAsync().Result)
                {
                    return;
                }

                dbContext.DataSources.Add(DataSourceAggregate.Create("BBC", "Top stories", "http://feeds.bbci.co.uk/news/rss.xml"));
                dbContext.SaveChanges();
            }
        }

        private static void StartBus(IServiceProvider serviceProvider)
        {
            var busControl = serviceProvider.GetService<IBusControl>();
            busControl.Start();
        }
    }
}