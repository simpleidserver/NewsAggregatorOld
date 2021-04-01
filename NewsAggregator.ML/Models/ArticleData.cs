using Microsoft.ML.Data;
using NewsAggregator.Core.QueryResults;

namespace NewsAggregator.ML.Models
{
    public class ArticleData
    {
        [LoadColumn(0)]
        public string Id { get; set; }
        [LoadColumn(1)]
        public string ExternalId { get; set; }
        [LoadColumn(2)]
        public string Title { get; set; }
        [LoadColumn(3)]
        public string Summary { get; set; }
        [LoadColumn(4)]
        public string Text { get; set; }

        public static ArticleData Transform(ArticleQueryResult article)
        {
            return new ArticleData
            {
                ExternalId = article.ExternalId,
                Id = article.Id,
                Summary = article.Summary,
                Text = article.Text,
                Title = article.Title
            };
        }
    }
}
