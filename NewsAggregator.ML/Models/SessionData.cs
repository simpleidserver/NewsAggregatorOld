using Microsoft.ML.Data;

namespace NewsAggregator.ML.Models
{
    public class SessionData
    {
        [LoadColumn(0)]
        public int EventType { get; set; }
        [LoadColumn(1)]
        public string ArticleId { get; set; }
        [LoadColumn(2)]
        public string ArticleLanguage { get; set; }
    }
}
