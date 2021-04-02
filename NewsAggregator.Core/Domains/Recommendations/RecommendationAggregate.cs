using NewsAggregator.Core.Domains.Recommendations.Events;
using System;
using System.Collections.Generic;

namespace NewsAggregator.Core.Domains.Recommendations
{
    public class RecommendationAggregate : BaseAggregate
    {
        private RecommendationAggregate() 
        {
            Articles = new List<RecommendationArticle>();
        }

        public string UserId { get; set; }
        public ICollection<RecommendationArticle> Articles { get; set; }
        public DateTime CreateDateTime { get; set; }

        public static RecommendationAggregate Create(string userId)
        {
            var result = new RecommendationAggregate();
            var evt = new RecommendationAddedEvent(Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), 0, userId, DateTime.UtcNow);
            result.Handle(evt);
            result.DomainEvts.Add(evt);
            return result;
        }

        public void Recommend(string articleId, double score)
        {
            var evt = new ArticleRecommendedEvent(Guid.NewGuid().ToString(), Id, Version + 1, articleId, score);
            Handle(evt);
            DomainEvts.Add(evt);
        }

        protected override void Handle(dynamic evt)
        {
            Handle(evt);
        }

        private void Handle(RecommendationAddedEvent evt)
        {
            Id = evt.AggregateId;
            UserId = evt.UserId;
            CreateDateTime = evt.CreateDateTime;
            Version = evt.Version;
        }

        private void Handle(ArticleRecommendedEvent evt)
        {
            Articles.Add(RecommendationArticle.Create(evt.ArticleId, evt.Score));
            Version = evt.Version;
        }
    }
}
