using System;
using System.Diagnostics;

namespace NewsAggregator.Core.Domains.Feeds.Events
{
    [DebuggerDisplay("Unsubscribe datasource from the feed")]
    public class FeedDataSourceUnsubscribedEvent : DomainEvent
    {
        public FeedDataSourceUnsubscribedEvent(string id, string aggregateId, int version, string userId, string datasourceId, DateTime deletionDateTime) : base(id, aggregateId, version)
        {
            UserId = userId;
            DataSourceId = datasourceId;
            DeletionDateTime = deletionDateTime;
        }

        public string UserId { get; set; }
        public string DataSourceId { get; set; }
        public DateTime DeletionDateTime { get; set; }
    }
}
