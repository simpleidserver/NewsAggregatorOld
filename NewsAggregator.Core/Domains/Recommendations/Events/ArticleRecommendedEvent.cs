using System.Diagnostics;

namespace NewsAggregator.Core.Domains.Recommendations.Events
{
    [DebuggerDisplay("Recommend article")]
    public class ArticleRecommendedEvent : DomainEvent
    {
        public ArticleRecommendedEvent(string id, string aggregateId, int version, string articleId) : base(id, aggregateId, version)
        {
            ArticleId = articleId;
        }

        public string ArticleId { get; set; }
    }
}
