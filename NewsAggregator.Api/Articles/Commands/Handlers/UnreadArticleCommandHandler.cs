using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using NewsAggregator.Core.Domains.Articles.Events;
using NewsAggregator.Core.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace NewsAggregator.Api.Articles.Commands.Handlers
{
    public class UnreadArticleCommandHandler : BaseArticleActionCommandHandler, IRequestHandler<UnreadArticleCommand, bool>
    {
        public UnreadArticleCommandHandler(
            ILogger<BaseArticleActionCommandHandler> logger,
            IArticleCommandRepository articleCommandRepository,
            IBusControl busControl) : base(logger, articleCommandRepository, busControl) { }

        public Task<bool> Handle(UnreadArticleCommand request, CancellationToken cancellationToken)
        {
            return Handle<ArticleUnreadEvent>(request.ArticleId, (article) =>
            {
                article.Unread(request.UserId, request.SessionId);
            }, cancellationToken);
        }
    }
}
