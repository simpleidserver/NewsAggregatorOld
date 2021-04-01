namespace NewsAggregator.Core.Domains.Recommendations
{
    public class RecommendationArticle
    {
        private RecommendationArticle() { }

        public string ArticleId { get; set; }

        public static RecommendationArticle Create(string articleId)
        {
            return new RecommendationArticle
            {
                ArticleId = articleId
            };
        }
    }
}
