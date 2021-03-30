using System;

namespace NewsAggregator.Domain.DataSources
{
    public class DataSourceExtractionHistory
    {
        private DataSourceExtractionHistory() { }

        public string Id { get; set; }
        public DateTimeOffset LastArticlePublicationDate { get; set; }
        public int NbExtractedArticle { get; set; }

        public static DataSourceExtractionHistory Create(DateTimeOffset lastArticlePublicationDate, int nbExtractedArticles)
        {
            var result = new DataSourceExtractionHistory { Id = Guid.NewGuid().ToString(), LastArticlePublicationDate = lastArticlePublicationDate, NbExtractedArticle = nbExtractedArticles };
            return result;
        }
    }
}
