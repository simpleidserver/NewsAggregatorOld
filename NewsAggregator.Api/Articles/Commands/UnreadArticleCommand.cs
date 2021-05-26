using MediatR;

namespace NewsAggregator.Api.Articles.Commands
{
    public class UnreadArticleCommand : IRequest<bool>
    {
        public string ArticleId { get; set; }
        public string SessionId { get; set; }
        public string UserId { get; set; }
    }
}
