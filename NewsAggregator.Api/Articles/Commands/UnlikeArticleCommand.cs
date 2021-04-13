using MediatR;

namespace NewsAggregator.Api.Articles.Commands
{
    public class UnlikeArticleCommand : IRequest<bool>
    {
        public string SessionId { get; set; }
        public string UserId { get; set; }
        public string ArticleId { get; set; }
    }
}
