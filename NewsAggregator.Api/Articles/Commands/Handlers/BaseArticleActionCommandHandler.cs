using MassTransit;
using Microsoft.Extensions.Logging;
using NewsAggregator.Api.Resources;
using NewsAggregator.Core.Domains;
using NewsAggregator.Core.Domains.Articles;
using NewsAggregator.Core.Exceptions;
using NewsAggregator.Core.Repositories;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NewsAggregator.Api.Articles.Commands.Handlers
{
    public class BaseArticleActionCommandHandler
    {
        private readonly ILogger<BaseArticleActionCommandHandler> _logger;
        private readonly IArticleCommandRepository _articleCommandRepository;
        private readonly IBusControl _busControl;

        public BaseArticleActionCommandHandler(
            ILogger<BaseArticleActionCommandHandler> logger, 
            IArticleCommandRepository articleCommandRepository,
            IBusControl busControl)
        {
            _logger = logger;
            _articleCommandRepository = articleCommandRepository;
            _busControl = busControl;
        }

        protected async Task<bool> Handle<T>(string articleId, Action<ArticleAggregate> callback, CancellationToken cancellationToken) where T : DomainEvent
        {
            var article = await _articleCommandRepository.Get(articleId, cancellationToken);
            if (article == null)
            {
                _logger.LogError($"The article {article} doesn't exist");
                throw new NewsAggregatorResourceNotFoundException(string.Format(Global.ArticleDoesntExist, articleId));
            }

            callback(article);
            await _articleCommandRepository.Update(article, cancellationToken);
            await _articleCommandRepository.SaveChanges(cancellationToken);
            if (article.DomainEvts.Any())
            {
                await _busControl.Publish((T)article.DomainEvts.First(), cancellationToken);
            }

            return true;
        }
    }
}
