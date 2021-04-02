using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using NewsAggregator.Core.Domains.Articles.Events;
using NewsAggregator.Core.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace NewsAggregator.Api.Articles.Commands.Handlers
{
    public class LikeArticleCommandHandler : BaseArticleActionCommandHandler, IRequestHandler<LikeArticleCommand, bool>
    {
        public LikeArticleCommandHandler(ILogger<BaseArticleActionCommandHandler> logger, IArticleCommandRepository articleCommandRepository, IBusControl busControl) : base(logger, articleCommandRepository, busControl)
        {
        }

        public Task<bool> Handle(LikeArticleCommand request, CancellationToken cancellationToken)
        {
            return Handle<ArticleLikedEvent>(request.ArticleId, (article) =>
            {
                article.Like(request.UserId, request.SessionId);
            }, cancellationToken);
        }
    }
}
