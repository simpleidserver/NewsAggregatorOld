using NewsAggregator.Core.Domains.Sessions.Enums;
using System;
using System.Diagnostics;

namespace NewsAggregator.Core.Domains.Sessions.Events
{
    [DebuggerDisplay("An interaction occured")]
    public class SessionInteractionOccuredEvent : DomainEvent
    {
        public SessionInteractionOccuredEvent(string id, string aggregateId, int version, InteractionTypes interactionType, string articleId, string articleLanguage, DateTime actionDateTime) : base(id, aggregateId, version)
        {
            InteractionType = interactionType;
            ArticleId = articleId;
            ArticleLanguage = articleLanguage;
            ActionDateTime = actionDateTime;
        }

        public InteractionTypes InteractionType { get; set; }
        public string ArticleId { get; set; }
        public string ArticleLanguage { get; set; }
        public DateTime ActionDateTime { get; set; }
    }
}