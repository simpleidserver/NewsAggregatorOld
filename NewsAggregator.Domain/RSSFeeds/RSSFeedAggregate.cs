using System;
using System.Collections.Generic;
using System.Linq;

namespace NewsAggregator.Domain.RSSFeeds
{
    public class RSSFeedAggregate : BaseAggregate
    {
        private RSSFeedAggregate()
        {
            ExtractionHistories = new List<RSSFeedExtractionHistory>();
        }

        public string Title { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public ICollection<RSSFeedExtractionHistory> ExtractionHistories { get; set; }

        public bool IsArticleExtracted(DateTimeOffset publicationDate)
        {
            var extractionHistory = ExtractionHistories.OrderBy(h => h.LastArticlePublicationDate).FirstOrDefault();
            if (extractionHistory == null)
            {
                return false;
            }

            return extractionHistory.LastArticlePublicationDate >= publicationDate;
        }

        public void AddHistory(DateTimeOffset lastArticlePublicationDate, int nbExtractedArticles)
        {
            ExtractionHistories.Add(RSSFeedExtractionHistory.Crate(lastArticlePublicationDate, nbExtractedArticles));
        }

        public static RSSFeedAggregate Create(string title, string description, string url)
        {
            var result = new RSSFeedAggregate { Id = Guid.NewGuid().ToString(), Title = title, Description = description, Url = url };
            return result;
        }

        protected override void Handle(dynamic evt)
        {

        }
    }
}