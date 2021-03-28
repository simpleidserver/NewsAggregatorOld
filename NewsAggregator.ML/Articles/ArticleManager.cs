using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NewsAggregator.Domain.Articles.Events;
using NewsAggregator.ML.Articles.Operations;
using NewsAggregator.ML.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;

namespace NewsAggregator.ML.Articles
{
    public class ArticleManager : IArticleManager
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly NewsAggregatorMLOptions _options;
        private readonly ILogger<ArticleManager> _logger;

        public ArticleManager(
            IHttpClientFactory httpClientFactory,
            IOptions<NewsAggregatorMLOptions> options,
            ILogger<ArticleManager> logger)
        {
            _httpClientFactory = httpClientFactory;
            _options = options.Value;
            _logger = logger;
        }

        public void AddArticles(IEnumerable<ArticleAddedEvent> evts)
        {
            EnlisteOperation(_logger, new AppendArticlesOperation(evts));
            EnlisteOperation(_logger, new TrainWord2VecOperation(evts.First(), _options, _httpClientFactory, _logger));
            EnlisteOperation(_logger, new TrainLDAOperation(evts.First(), _options));
        }

        private static readonly object _enlistmentsLock = new object();
        [ThreadStatic]
        private static Dictionary<string, ArticleEnlistment> _enlistments;

        private static void EnlisteOperation(ILogger<ArticleManager> logger, IOperation operation)
        {
            var tx = Transaction.Current;
            ArticleEnlistment enlistment;
            lock (_enlistmentsLock)
            {
                if (_enlistments == null)
                {
                    _enlistments = new Dictionary<string, ArticleEnlistment>();
                }

                if (!_enlistments.TryGetValue(tx.TransactionInformation.LocalIdentifier, out enlistment))
                {
                    enlistment = new ArticleEnlistment(logger, tx);
                    _enlistments.Add(tx.TransactionInformation.LocalIdentifier, enlistment);
                }

                enlistment.Enlist(operation);
            }
        }
    }
}
