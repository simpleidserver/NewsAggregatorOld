using System.Diagnostics;

namespace NewsAggregator.Core.Domains.Feeds.Events
{
    [DebuggerDisplay("Remove the feed")]
    public class FeedRemovedEvent : DomainEvent
    {
        public FeedRemovedEvent(string id, string aggregateId, int version, string userId) : base(id, aggregateId, version)
        {
            UserId = userId;
        }

        public string UserId { get; set; }
    }
}
