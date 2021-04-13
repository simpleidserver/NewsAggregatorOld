using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using NewsAggregator.Core.Domains.Articles.Events;
using NewsAggregator.Core.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace NewsAggregator.Api.Articles.Commands.Handlers
{
    public class UnlikeArticleCommandHandler : BaseArticleActionCommandHandler, IRequestHandler<UnlikeArticleCommand, bool>
    {
        public UnlikeArticleCommandHandler(ILogger<BaseArticleActionCommandHandler> logger, IArticleCommandRepository articleCommandRepository, IBusControl busControl) : base(logger, articleCommandRepository, busControl)
        {
        }

        public Task<bool> Handle(UnlikeArticleCommand request, CancellationToken cancellationToken)
        {
            return Handle<ArticleUnlikedEvent>(request.ArticleId, (article) =>
            {
                article.Unlike(request.UserId, request.SessionId);
            }, cancellationToken);
        }
    }
}
