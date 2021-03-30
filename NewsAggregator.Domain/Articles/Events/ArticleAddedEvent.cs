using System;
using System.Diagnostics;

namespace NewsAggregator.Domain.Articles.Events
{
    [DebuggerDisplay("Add article")]
    public class ArticleAddedEvent : DomainEvent
    {
        public ArticleAddedEvent(string id, string aggregateId, int version, string externalId, string title, string summary, string content, string language, DateTimeOffset publishDate) : base(id, aggregateId, version) 
        {
            ExternalId = externalId;
            Title = title;
            Summary = summary;
            Content = content;
            Language = language;
            PublishDate = publishDate;
        }

        public string ExternalId { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public string Content { get; set; }
        public string Language { get; set; }
        public DateTimeOffset PublishDate { get; set; }
    }
}