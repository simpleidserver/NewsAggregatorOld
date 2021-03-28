using System;

namespace NewsAggregator.Domain.RSSFeeds
{
    public class RSSFeedExtractionHistory
    {
        private RSSFeedExtractionHistory() { }

        public DateTimeOffset LastArticlePublicationDate { get; set; }
        public int NbExtractedArticle { get; set; }

        public static RSSFeedExtractionHistory Crate(DateTimeOffset lastArticlePublicationDate, int nbExtractedArticles)
        {
            var result = new RSSFeedExtractionHistory { LastArticlePublicationDate = lastArticlePublicationDate, NbExtractedArticle = nbExtractedArticles };
            return result;
        }
    }
}
