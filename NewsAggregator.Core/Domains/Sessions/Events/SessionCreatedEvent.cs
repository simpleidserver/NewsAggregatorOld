using System;
using System.Diagnostics;

namespace NewsAggregator.Core.Domains.Sessions.Events
{
    [DebuggerDisplay("Create session")]
    public class SessionCreatedEvent : DomainEvent
    {
        public SessionCreatedEvent(string id, string aggregateId, int version, string userId, DateTime createDateTime) : base(id, aggregateId, version)
        {
            UserId = userId;
            CreateDateTime = createDateTime;
        }

        public string UserId { get; set; }
        public DateTime CreateDateTime { get; set; }
    }
}
