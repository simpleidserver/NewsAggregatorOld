using NewsAggregator.Domain.Articles;
using System;

namespace NewsAggregator.Api.Articles.Results
{
    public class ArticleResult
    {
        public string Id { get; set; }
        public string ExternalId { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public string Content { get; set; }
        public string Language { get; set; }
        public DateTimeOffset PublishDate { get; set; }

        public static ArticleResult ToDto(ArticleAggregate article)
        {
            return new ArticleResult
            {
                Id = article.Id,
                Content = article.Content,
                Title = article.Title,
                ExternalId = article.ExternalId,
                Language = article.Language,
                PublishDate = article.PublishDate,
                Summary = article.Summary
            };
        }
    }
}
