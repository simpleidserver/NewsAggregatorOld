using Microsoft.ML.Data;

namespace NewsAggregator.ML.Models
{
    public class InteractionSessionData
    {
        [LoadColumn(0)]
        public int EventType { get; set; }
        [LoadColumn(1)]
        public string ArticleId { get; set; }
    }
}
