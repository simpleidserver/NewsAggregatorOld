using System;

namespace NewsAggregator.ML.Models
{
    public class RecommendedArticle : IEquatable<RecommendedArticle>
    {
        public string ArticleId { get; set; }
        public string UserId { get; set; }
        public double Score { get; set; }

        public bool Equals(RecommendedArticle other)
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
