namespace NewsAggregator.Core.Domains.Recommendations
{
    public class RecommendationArticle
    {
        private RecommendationArticle() { }

        public string ArticleId { get; set; }
        public double Score { get; set; }

        public static RecommendationArticle Create(string articleId, double score)
        {
            return new RecommendationArticle
            {
                ArticleId = articleId,
                Score = score
            };
        }
    }
}
