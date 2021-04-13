using System;
using System.Diagnostics;

namespace NewsAggregator.Core.Domains.Articles.Events
{
    [DebuggerDisplay("Unlike article")]
    public class ArticleUnlikedEvent : DomainEvent
    {
        public ArticleUnlikedEvent(string id, string aggregateId, int version, string language, string userId, string sessionId, DateTime actionDateTime) : base(id, aggregateId, version)
        {
            Language = language;
            UserId = userId;
            SessionId = sessionId;
            ActionDateTime = actionDateTime;
        }

        public string Language { get; set; }
        public string UserId { get; set; }
        public string SessionId { get; set; }
        public DateTime ActionDateTime { get; set; }
    }
}
