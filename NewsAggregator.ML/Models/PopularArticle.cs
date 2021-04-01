using System;

namespace NewsAggregator.ML.Models
{
    public class PopularArticle : IEquatable<PopularArticle>
    {
        public PopularArticle(string userId, string articleId, string articleLanguage, double score)
        {
            UserId = userId;
            ArticleId = articleId;
            ArticleLanguage = articleLanguage;
            Score = score;
        }

        public string UserId { get; set; }
        public string ArticleId { get; set; }
        public string ArticleLanguage { get; set; }
        public double Score { get; set; }

        public bool Equals(PopularArticle other)
        {
            if (other == null)
            {
                return false;
            }

            return other.GetHashCode() == GetHashCode();
        }

        public override int GetHashCode()
        {
            return ArticleId.GetHashCode();
        }
    }
}
