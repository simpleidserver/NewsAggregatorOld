namespace NewsAggregator.Core.QueryResults
{
    public class RecommendationQueryResult
    {
        public double ArticleScore { get; set; }
        public string ArticleId { get; set; }
        public string ArticleLanguage { get; set; }
        public string ArticleTitle { get; set; }
        public string ArticleSummary { get; set; }
        public string ArticleExternalId { get; set; }
    }
}
