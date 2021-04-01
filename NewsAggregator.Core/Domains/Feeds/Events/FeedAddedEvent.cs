using System;
using System.Diagnostics;

namespace NewsAggregator.Core.Domains.Feeds.Events
{
    [DebuggerDisplay("add feed")]
    public class FeedAddedEvent : DomainEvent
    {
        public FeedAddedEvent(string id, string aggregateId, int version, string userId, string title, DateTime createDateTime) : base(id, aggregateId, version)
        {
            UserId = userId;
            Title = title;
            CreateDateTime = createDateTime;
        }

        public string UserId { get; set; }
        public string Title { get; set; }
        public DateTime CreateDateTime { get; set; }
    }
}
