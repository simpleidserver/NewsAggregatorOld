using System;

namespace NewsAggregator.Core.Domains.Articles
{
    public class ArticleLike
    {
        private ArticleLike() { }

        public string UserId { get; set; }
        public DateTime ActionDateTime { get; set; } 

        public static ArticleLike Create(string userId, DateTime actionDateTime)
        {
            return new ArticleLike
            {
                UserId = userId,
                ActionDateTime = actionDateTime
            };
        }
    }
}
