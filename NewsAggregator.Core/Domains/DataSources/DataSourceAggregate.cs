using System;
using System.Collections.Generic;
using System.Linq;

namespace NewsAggregator.Core.Domains.DataSources
{
    public class DataSourceAggregate : BaseAggregate
    {
        private DataSourceAggregate()
        {

        }

        public string Title { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public ICollection<DataSourceExtractionHistory> ExtractionHistories { get; set; }

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
            ExtractionHistories.Add(DataSourceExtractionHistory.Create(lastArticlePublicationDate, nbExtractedArticles));
        }

        public static DataSourceAggregate Create(string title, string description, string url)
        {
            var result = new DataSourceAggregate
            {
                Id = Guid.NewGuid().ToString(),
                Title = title,
                Description = description,
                Url = url
            };
            return result;
        }

        protected override void Handle(dynamic evt)
        {
            throw new System.NotImplementedException();
        }
    }
}