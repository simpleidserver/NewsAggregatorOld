using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using NewsAggregator.Core.Domains.Articles.Events;
using NewsAggregator.Core.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace NewsAggregator.Api.Articles.Commands.Handlers
{
    public class ReadAndHideArticleCommandHandler : BaseArticleActionCommandHandler, IRequestHandler<ReadAndHideArticleCommand, bool>
    {
        public ReadAndHideArticleCommandHandler(
            ILogger<BaseArticleActionCommandHandler> logger, 
            IArticleCommandRepository articleCommandRepository, 
            IBusControl busControl) : base(logger, articleCommandRepository, busControl)
        {

        }

        public Task<bool> Handle(ReadAndHideArticleCommand request, CancellationToken cancellationToken)
        {
            return Handle<ArticleReadEvent>(request.ArticleId, (article) =>
            {
                article.ReadAndHide(request.UserId, request.SessionId);
            }, cancellationToken);
        }
    }
}
