using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using NewsAggregator.Core.Domains.Articles.Events;
using NewsAggregator.Core.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace NewsAggregator.Api.Articles.Commands.Handlers
{
    public class ViewArticleCommandHandler : BaseArticleActionCommandHandler, IRequestHandler<ViewArticleCommand, bool>
    {
        public ViewArticleCommandHandler(
            ILogger<BaseArticleActionCommandHandler> logger,
            IArticleCommandRepository articleCommandRepository,
            IBusControl busControl) : base(logger, articleCommandRepository, busControl) { }

        public Task<bool> Handle(ViewArticleCommand request, CancellationToken cancellationToken)
        {
            return Handle<ArticleViewedEvent>(request.ArticleId, (article) =>
            {
                article.View(request.UserId, request.SessionId);
            }, cancellationToken);
        }
    }
}