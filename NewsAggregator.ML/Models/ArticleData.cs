using Microsoft.ML.Data;

namespace NewsAggregator.ML.Models
{
    public class ArticleData
    {
        [LoadColumn(0)]
        public string Id { get; set; }
        [LoadColumn(1)]
        public string Text { get; set; }
    }
}
