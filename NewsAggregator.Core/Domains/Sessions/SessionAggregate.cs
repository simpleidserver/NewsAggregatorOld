using NewsAggregator.Core.Domains.Sessions.Enums;
using NewsAggregator.Core.Domains.Sessions.Events;
using System;
using System.Collections.Generic;

namespace NewsAggregator.Core.Domains.Sessions
{
    public class SessionAggregate : BaseAggregate
    {
        private SessionAggregate() 
        {
            Actions = new List<SessionAction>();
        }

        public string UserId { get; set; }
        public DateTime CreateDateTime { get; set; }
        public ICollection<SessionAction> Actions { get; set; }

        public static SessionAggregate Create(string sessionId, string userId)
        {
            var result = new SessionAggregate();
            var evt = new SessionCreatedEvent(Guid.NewGuid().ToString(), sessionId, 0, userId, DateTime.UtcNow);
            result.Handle(evt);
            return result;
        }

        public void Read(string articleId, string articleLanguage, DateTime actionDateTime)
        {
            var evt = new SessionInteractionOccuredEvent(Guid.NewGuid().ToString(), Id, Version + 1, InteractionTypes.READ, articleId, articleLanguage, actionDateTime);
            Handle(evt);
        }

        public void Like(string articleId, string articleLanguage, DateTime actionDateTime)
        {
            var evt = new SessionInteractionOccuredEvent(Guid.NewGuid().ToString(), Id, Version + 1, InteractionTypes.LIKE, articleId,articleLanguage, actionDateTime);
            Handle(evt);
        }

        public void Unread(string articleId, string articleLanguage, DateTime actionDateTime)
        {
            var evt = new SessionInteractionOccuredEvent(Guid.NewGuid().ToString(), Id, Version + 1, InteractionTypes.UNREAD, articleId, articleLanguage, actionDateTime);
            Handle(evt);
        }

        protected override void Handle(dynamic evt)
        {
            Handle(evt);
        }

        private void Handle(SessionCreatedEvent evt)
        {
            Id = evt.AggregateId;
            UserId = evt.UserId;
            CreateDateTime = evt.CreateDateTime;
            Version = evt.Version;
        }

        private void Handle(SessionInteractionOccuredEvent evt)
        {
            Actions.Add(SessionAction.Create(evt.ArticleId, evt.ArticleLanguage, evt.InteractionType, evt.ActionDateTime));
            Version = evt.Version;
        }
    }
}