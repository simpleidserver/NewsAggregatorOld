using System;
using System.Diagnostics;

namespace NewsAggregator.Core.Domains.Recommendations.Events
{
    [DebuggerDisplay("Create recommendation")]
    public class RecommendationAddedEvent : DomainEvent
    {
        public RecommendationAddedEvent(string id, string aggregateId, int version, string userId, DateTime createDateTime) : base(id, aggregateId, version)
        {
            UserId = userId;
            CreateDateTime = createDateTime;
        }

        public string UserId { get; set; }
        public DateTime CreateDateTime { get; set; }
    }
}
