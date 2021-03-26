namespace NewsAggregator.ML.Models
{
    public class PopularArticle
    {
        public PopularArticle(string personId, string articleId, string articleLanguage, double score)
        {
            PersonId = personId;
            ArticleId = articleId;
            ArticleLanguage = articleLanguage;
            Score = score;
        }

        public string PersonId { get; set; }
        public string ArticleId { get; set; }
        public string ArticleLanguage { get; set; }
        public double Score { get; set; }
    }
}
