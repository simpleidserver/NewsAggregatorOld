using System;

namespace NewsAggregator.Core.Domains.Articles
{
    public class ArticleRead
    {
        private ArticleRead() { }

        public string UserId { get; set; }
        public DateTime ActionDateTime { get; set; }
        public bool IsHidden { get; set; }

        public void UpdateIsHidden(bool isHidden)
        {
            IsHidden = isHidden;
        }

        public static ArticleRead Create(string userId, DateTime actionDateTime)
        {
            return new ArticleRead
            {
                UserId = userId,
                ActionDateTime = actionDateTime
            };
        }
    }
}
