using MediatR;
using Microsoft.Extensions.Logging;
using NewsAggregator.Api.Resources;
using NewsAggregator.Common.Exceptions;
using NewsAggregator.Core.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace NewsAggregator.Api.Feeds.Commands.Handlers
{
    public class SubscribeDatasourceCommandHandler : IRequestHandler<SubscribeDatasourceCommand, bool>
    {
        private readonly IFeedCommandRepository _feedCommandRepository;
        private readonly ILogger<SubscribeDatasourceCommandHandler> _logger;

        public SubscribeDatasourceCommandHandler(
            IFeedCommandRepository feedCommandRepository,
            ILogger<SubscribeDatasourceCommandHandler> logger)
        {
            _feedCommandRepository = feedCommandRepository;
            _logger = logger;
        }

        public async Task<bool> Handle(SubscribeDatasourceCommand request, CancellationToken cancellationToken)
        {
            var feed = await _feedCommandRepository.Get(request.FeedId, cancellationToken);
            if (feed == null)
            {
                _logger.LogError($"Feed {request.FeedId} doesn't exist");
                throw new NewsAggregatorResourceNotFoundException(string.Format(Global.FeedDoesntExist, feed.Id));
            }

            feed.SubscribeDataSource(request.UserId, request.DatasourceId);
            await _feedCommandRepository.Update(feed, cancellationToken);
            await _feedCommandRepository.SaveChanges(cancellationToken);
            _logger.LogInformation($"Subscribe to the datasource {request.DatasourceId} into the feed {request.FeedId}");
            throw new System.NotImplementedException();
        }
    }
}
