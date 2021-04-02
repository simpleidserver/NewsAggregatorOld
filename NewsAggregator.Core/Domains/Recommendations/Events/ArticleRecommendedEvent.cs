using System.Diagnostics;

namespace NewsAggregator.Core.Domains.Recommendations.Events
{
    [DebuggerDisplay("Recommend article")]
    public class ArticleRecommendedEvent : DomainEvent
    {
        public ArticleRecommendedEvent(string id, string aggregateId, int version, string articleId, double score) : base(id, aggregateId, version)
        {
            ArticleId = articleId;
            Score = score;
        }

        public string ArticleId { get; set; }
        public double Score { get; set; }
    }
}