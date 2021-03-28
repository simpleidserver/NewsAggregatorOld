using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NewsAggregator.Core.Repositories;
using NewsAggregator.Domain.Articles;
using NewsAggregator.Domain.Articles.Events;
using NewsAggregator.Domain.RSSFeeds;
using NewsAggregator.ML.Articles;
using NewsAggregator.ML.Infrastructures.Jobs;
using NewsAggregator.ML.Infrastructures.Locks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using System.Xml;

namespace NewsAggregator.ML.Jobs
{
    public class RSSArticleExtractorJob : BaseScheduledJob
    {
        private readonly IRSSFeedRepository _rssFeedRepository;
        private readonly IArticleManager _articleManager;

        public RSSArticleExtractorJob(
            IRSSFeedRepository rssFeedRepository,
            IArticleManager articleManager,
            IDistributedLock distributedLock, 
            IOptions<NewsAggregatorMLOptions> options, 
            ILogger<BaseScheduledJob> logger) : base(distributedLock, options, logger)
        {
            _rssFeedRepository = rssFeedRepository;
            _articleManager = articleManager;
        }

        protected override string LockName => "rss-article-extractor";
        protected override int IntervalMS => 10 * 1000;

        protected override async Task Execute(CancellationToken cancellationToken)
        {
            var rssFeeds = await _rssFeedRepository.GetAll(cancellationToken);
            foreach(var rssFeed in rssFeeds)
            {
                try
                {
                    ExtractArticlesFromFeed(rssFeed);
                }
                catch(Exception ex)
                {
                    Logger.LogError(ex.ToString());   
                }
            }

            await _rssFeedRepository.Update(rssFeeds, cancellationToken);
            await _rssFeedRepository.SaveChanges(cancellationToken);
        }

        private void ExtractArticlesFromFeed(RSSFeedAggregate rssFeed)
        {
            var reader = XmlReader.Create(rssFeed.Url);
            var feed = SyndicationFeed.Load(reader);
            reader.Close();
            var result = new List<ArticleAddedEvent>();
            foreach(var item in feed.Items.OrderBy(i => i.PublishDate))
            {
                if (!rssFeed.IsArticleExtracted(item.PublishDate))
                {
                    ArticleAggregate.Create(item.Id, item.Title.Text, item.Summary.Text, null, "en", item.PublishDate, out ArticleAddedEvent evt);
                    result.Add(evt);
                }
            }

            if (result.Any())
            {
                using (var transactionScope = new TransactionScope())
                {
                    _articleManager.AddArticles(result);
                    transactionScope.Complete();
                }

                var lastArticle = result.OrderByDescending(a => a.PublishDate).FirstOrDefault();
                if (lastArticle != null)
                {
                    rssFeed.AddHistory(lastArticle.PublishDate, result.Count());
                }
            }
        }
    }
}
