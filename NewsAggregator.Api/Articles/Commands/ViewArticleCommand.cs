namespace NewsAggregator.Api.Articles.Commands
{
    public class ViewArticleCommand
    {
        public string SessionId { get; set; }
        public string UserId { get; set; }
        public string ArticleId { get; set; }
    }
}
