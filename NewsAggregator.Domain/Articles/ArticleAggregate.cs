using NewsAggregator.Domain.Articles.Events;
using System;

namespace NewsAggregator.Domain.Articles
{
    public class ArticleAggregate : BaseAggregate
    {
        private ArticleAggregate() { }

        public string ExternalId { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public string Content { get; set; }
        public string Language { get; set; }
        public DateTimeOffset PublishDate { get; set; }

        public static ArticleAggregate Create(string externalId, string title, string summary, string content, string language, DateTimeOffset publishDate)
        {
            var result = new ArticleAggregate();
            var evt = new ArticleAddedEvent(Guid.NewGuid().ToString(), externalId, title, summary, content, language, publishDate);
            result.Handle(evt);
            result.DomainEvts.Add(evt);
            return result;
        }

        protected override void Handle(dynamic evt)
        {
            Handle(evt);
        }

        private void Handle(ArticleAddedEvent evt)
        {
            Id = evt.Id;
            ExternalId = evt.ExternalId;
            Title = evt.Title;
            Summary = evt.Summary;
            Content = evt.Content;
            Language = evt.Language;
            PublishDate = evt.PublishDate;
        }
    }
}
