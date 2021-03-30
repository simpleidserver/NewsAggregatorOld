using NewsAggregator.Domain.Sessions.Enums;
using System.Diagnostics;

namespace NewsAggregator.Domain.Sessions.Events
{
    [DebuggerDisplay("An interaction occured")]
    public class SessionInteractionOccuredEvent : DomainEvent
    {
        public SessionInteractionOccuredEvent(string id, string aggregateId, int version, string personId, string sessionId, InteractionTypes eventType, string articleId, string articleLanguage) : base(id, aggregateId, version)
        {
            PersonId = personId;
            SessionId = sessionId;
            EventType = eventType;
            ArticleId = articleId;
            ArticleLanguage = articleLanguage;
        }

        public string PersonId { get; set; }
        public string SessionId { get; set; }
        public InteractionTypes EventType { get; set; }
        public string ArticleId { get; set; }
        public string ArticleLanguage { get; set; }
    }
}