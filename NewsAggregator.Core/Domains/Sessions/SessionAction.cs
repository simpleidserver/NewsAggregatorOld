using NewsAggregator.Core.Domains.Sessions.Enums;
using System;

namespace NewsAggregator.Core.Domains.Sessions
{
    public class SessionAction
    {
        private SessionAction() { }

        public string ArticleId { get; set; }
        public string ArticleLanguage { get; set; }
        public InteractionTypes InteractionType { get; set; }
        public DateTime ActionDateTime { get; set; }

        public static SessionAction Create(string articleId, string articleLanguage, InteractionTypes interactionType, DateTime actionDateTime)
        {
            return new SessionAction { ArticleId = articleId, ArticleLanguage = articleLanguage, ActionDateTime = actionDateTime, InteractionType = interactionType };
        }
    }
}
