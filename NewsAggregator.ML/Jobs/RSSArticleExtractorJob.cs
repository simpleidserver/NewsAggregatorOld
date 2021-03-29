using Microsoft.Extensions.Logging;
using NewsAggregator.Core.Repositories;
using NewsAggregator.Domain.Articles;
using NewsAggregator.Domain.RSSFeeds;
using NewsAggregator.ML.Articles;
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
    public class RSSArticleExtractorJob : IRSSArticleExtractorJob
    {
        private const string LockName = "extract-rss-articles";
        private readonly IRSSFeedRepository _rssFeedRepository;
        private readonly IArticleRepository _articleRepository;
        private readonly IArticleManager _articleManager;
        private readonly IDistributedLock _distributedLock;
        private readonly ILogger<RSSArticleExtractorJob> _logger;
        private static object _obj = new object();

        public RSSArticleExtractorJob(
            IRSSFeedRepository rssFeedRepository,
            IArticleRepository articleRepository,
            IArticleManager articleManager,
            IDistributedLock distributedLock,
            ILogger<RSSArticleExtractorJob> logger)
        {
            _rssFeedRepository = rssFeedRepository;
            _articleRepository = articleRepository;
            _articleManager = articleManager;
            _distributedLock = distributedLock;
            _logger = logger;
        }

        public async Task Run(CancellationToken cancellationToken)
        {
            if (!await _distributedLock.TryAcquireLock(LockName, cancellationToken))
            {
                return;
            }

            try
            {
                var rssFeeds = await _rssFeedRepository.GetAll(cancellationToken);
                foreach (var rssFeed in rssFeeds)
                {
                    try
                    {
                        await ExtractArticlesFromFeed(rssFeed, cancellationToken);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex.ToString());
                    }
                }

                await _rssFeedRepository.Update(rssFeeds, cancellationToken);
                await _rssFeedRepository.SaveChanges(cancellationToken);
            }
            finally
            {
                await _distributedLock.ReleaseLock(LockName, cancellationToken);
            }
        }

        private async Task ExtractArticlesFromFeed(RSSFeedAggregate rssFeed, CancellationToken cancellationToken)
        {
            var reader = XmlReader.Create(rssFeed.Url);
            var feed = SyndicationFeed.Load(reader);
            reader.Close();
            var result = new List<ArticleAggregate>();
            foreach (var item in feed.Items.OrderBy(i => i.PublishDate))
            {
                if (!rssFeed.IsArticleExtracted(item.PublishDate))
                {
                    var article = ArticleAggregate.Create(item.Id, item.Title.Text, item.Summary.Text, null, "en", item.PublishDate);
                    result.Add(article);
                }
            }

            if (result.Any())
            {
                using (var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    _articleManager.AddArticles(result);
                    foreach(var article in result)
                    {
                        await _articleRepository.Add(article, cancellationToken);
                    }

                    await _articleRepository.SaveChanges(cancellationToken);
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
