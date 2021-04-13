using System;

namespace NewsAggregator.Core.QueryResults
{
    public class ArticleQueryResult
    {
        public string Id { get; set; }
        public string ExternalId { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public string Language { get; set; }
        public DateTimeOffset PublishDate { get; set; }
        public DateTime? LikeActionDateTime { get; set; }
        public string Text { get; set; }
    }
}
