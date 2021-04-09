using MediatR;
using Microsoft.Extensions.Logging;
using NewsAggregator.Api.Resources;
using NewsAggregator.Core.Exceptions;
using NewsAggregator.Core.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace NewsAggregator.Api.Feeds.Commands.Handlers
{
    public class DeleteFeedCommandHandler : IRequestHandler<DeleteFeedCommand, bool>
    {
        private readonly IFeedCommandRepository _feedCommandRepository;
        private readonly ILogger<DeleteFeedCommandHandler> _logger;

        public DeleteFeedCommandHandler(
            IFeedCommandRepository feedCommandRepository,
            ILogger<DeleteFeedCommandHandler> logger)
        {
            _feedCommandRepository = feedCommandRepository;
            _logger = logger;
        }

        public async Task<bool> Handle(DeleteFeedCommand request, CancellationToken cancellationToken)
        {
            var feed = await _feedCommandRepository.Get(request.FeedId, cancellationToken);
            if (feed == null)
            {
                _logger.LogError($"Feed {request.FeedId} doesn't exist");
                throw new NewsAggregatorResourceNotFoundException(string.Format(Global.FeedDoesntExist, feed.Id));
            }

            feed.Remove(request.UserId);
            await _feedCommandRepository.Delete(feed, cancellationToken);
            await _feedCommandRepository.SaveChanges(cancellationToken);
            _logger.LogInformation($"the feed {request.FeedId} is removed");
            return true;
        }
    }
}
