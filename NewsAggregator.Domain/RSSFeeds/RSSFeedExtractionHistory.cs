using System;

namespace NewsAggregator.Domain.RSSFeeds
{
    public class RSSFeedExtractionHistory
    {
        private RSSFeedExtractionHistory() { }

        public string Id { get; set; }
        public DateTimeOffset LastArticlePublicationDate { get; set; }
        public int NbExtractedArticle { get; set; }

        public static RSSFeedExtractionHistory Crate(DateTimeOffset lastArticlePublicationDate, int nbExtractedArticles)
        {
            var result = new RSSFeedExtractionHistory { Id = Guid.NewGuid().ToString(), LastArticlePublicationDate = lastArticlePublicationDate, NbExtractedArticle = nbExtractedArticles };
            return result;
        }
    }
}
