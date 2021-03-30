using Microsoft.Extensions.Logging;
using NewsAggregator.Core.Repositories;
using NewsAggregator.Domain.Articles;
using NewsAggregator.Domain.DataSources;
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
    public class ArticleExtractorJob : IArticleExtractorJob
    {
        private const string LockName = "extract-articles";
        private readonly IDataSourceCommandRepository _datasourceCommandRepository;
        private readonly IArticleCommandRepository _articleRepository;
        private readonly IArticleManager _articleManager;
        private readonly IDistributedLock _distributedLock;
        private readonly ILogger<ArticleExtractorJob> _logger;
        private static object _obj = new object();

        public ArticleExtractorJob(
            IDataSourceCommandRepository datasourceCommandRepository,
            IArticleCommandRepository articleRepository,
            IArticleManager articleManager,
            IDistributedLock distributedLock,
            ILogger<ArticleExtractorJob> logger)
        {
            _datasourceCommandRepository = datasourceCommandRepository;
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
                var datasources = await _datasourceCommandRepository.GetAll(cancellationToken);
                foreach (var datasource in datasources)
                {
                    try
                    {
                        await ExtractArticlesFromFeed(datasource, cancellationToken);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex.ToString());
                    }
                }

                await _datasourceCommandRepository.Update(datasources, cancellationToken);
                await _datasourceCommandRepository.SaveChanges(cancellationToken);
            }
            finally
            {
                await _distributedLock.ReleaseLock(LockName, cancellationToken);
            }
        }

        private async Task ExtractArticlesFromFeed(DataSourceAggregate datasource, CancellationToken cancellationToken)
        {
            var reader = XmlReader.Create(datasource.Url);
            var feed = SyndicationFeed.Load(reader);
            reader.Close();
            var result = new List<ArticleAggregate>();
            foreach (var item in feed.Items.OrderBy(i => i.PublishDate))
            {
                if (!datasource.IsArticleExtracted(item.PublishDate))
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
                    datasource.AddHistory(lastArticle.PublishDate, result.Count());
                }
            }
        }
    }
}
