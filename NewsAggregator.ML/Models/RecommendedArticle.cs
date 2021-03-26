namespace NewsAggregator.ML.Models
{
    public class RecommendedArticle
    {
        public string ArticleId { get; set; }
        public string PersonId { get; set; }
        public double Score { get; set; }
    }
}
