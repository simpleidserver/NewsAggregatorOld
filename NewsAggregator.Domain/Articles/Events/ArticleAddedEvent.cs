using System;
using System.Diagnostics;

namespace NewsAggregator.Domain.Articles.Events
{
    [DebuggerDisplay("Add article")]
    public class ArticleAddedEvent : DomainEvent
    {
        public ArticleAddedEvent(string id) : base(id) { }

        public string Title { get; set; }
        public string Summary { get; set; }
        public string Content { get; set; }
        public string Language { get; set; }
    }
}