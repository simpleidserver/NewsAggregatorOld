using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NewsAggregator.ML.Infrastructures.Jobs;
using NewsAggregator.ML.Infrastructures.Locks;
using System.ServiceModel.Syndication;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;

namespace NewsAggregator.ML.Jobs
{
    public class RSSArticleExtractorJob : BaseScheduledJob
    {
        public RSSArticleExtractorJob(IDistributedLock distributedLock, IOptions<NewsAggregatorMLOptions> options, ILogger<BaseScheduledJob> logger) : base(distributedLock, options, logger)
        {
        }

        protected override string LockName => "rss-article-extractor";
        protected override int IntervalMS => 10 * 1000;

        protected override async Task Execute(CancellationToken cancellationToken)
        {
            var url = "http://feeds.bbci.co.uk/news/rss.xml";
            await ExtractArticlesFromFeed(url, cancellationToken);
        }

        private async Task ExtractArticlesFromFeed(string url, CancellationToken cancellationToken)
        {
            var reader = XmlReader.Create(url);
            var feed = SyndicationFeed.Load(reader);
            reader.Close();
            foreach(var item in feed.Items)
            {
                // TODO : Check if latest article has been fetched etc...
            }
        }
    }
}
